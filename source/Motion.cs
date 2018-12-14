using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Threading;
using lhdevice;

namespace MotionControl
{
    public enum Error
    {
        NoError = 0,
        FileNotExist = -1000,
        TimeOut = -1001,
        NEL = -1002,
        PEL = -1003
    }
    public enum HomeMode
    {
        ORG = 1,
        INDEX = 2,
        PROBE = 3,
        ORG_INDEX = 4
    }
    public struct HomePara
    {
        /// <summary>
        /// 回零模式
        /// </summary>
        public HomeMode mode;
        /// <summary>
        /// 加速度m/s^2
        /// </summary>
        public double acc;
        /// <summary>
        /// 速度m/s
        /// </summary>
        public double vel;
        /// <summary>
        /// 偏移量mm
        /// </summary>
        public double offset;
        public double maxSeconds;
    }

    public struct MotionPara
    {
        public double acc;
        public double maxVel;
        public double maxSeconds;
    }

    public struct AxisStatus
    {
        public bool PEL;
        public bool NEL;
        public bool Enabled;
        public bool Moving;
        public bool Alarm;

        public double CmdPos;
        public double FbkPos;
    }
    public class Axis
    {
        /// <summary>
        /// 轴号0-based
        /// </summary>
        public short id;
        public double plsPerMm;
        public HomePara homePara = new HomePara();
        public MotionPara motionPara = new MotionPara();
        internal AxisStatus status_ = new AxisStatus();
        public AxisStatus Stauts { get { return status_; } }

        /// <summary>
        /// 励磁使能
        /// </summary>
        /// <param name="enabled">true表示上电使能，false表示取消使能</param>
        /// <returns>0表示成功，负值表示错误码</returns>
        public int SetEnabled(bool enabled)
        {
            if (enabled)
            {
                return lhmtc.LH_AxisOn(id, false);
            }
            else
            {
                return lhmtc.LH_AxisOff(id, false);
            }
        }
        


        /// <summary>
        /// 回零动作，阻塞等待完成
        /// </summary>
        /// <returns></returns>
        public int Home()
        {
            int err_ = 0;
            int t_ = Environment.TickCount;
            if ((err_ = lhmtc.LH_Home(id, Int32.MaxValue, _vel2pls(homePara.vel), _acc2pls(homePara.acc), _mm2pls(homePara.offset), false))<0)
            {
                return err_;
            }            
            do
            {
                Thread.Sleep(20);
                lhmtc.LH_GetSts(id, out status_, 1, false);
                if(Environment.TickCount-t_>homePara.maxSeconds*1000)
                {
                    return (int)Error.TimeOut;
                }
            } while (status_.);


            if ((status_ & 0x20) == 0)
            {
                return 0;
            }
            //如果触发了正限位，则执行第二次反向回零
            if ((err_ = this.Reset()) < 0)
            {
                return err_;
            }
            t_ = Environment.TickCount;
            if ((err_ = lhmtc.LH_Home(id, Int32.MinValue, _vel2pls(homePara.vel), _acc2pls(homePara.acc), _mm2pls(homePara.offset), false)) < 0)
            {
                return err_;
            }
            do
            {
                Thread.Sleep(100);
                lhmtc.LH_GetSts(id, out status_, 1, false);
                if (Environment.TickCount - t_ > homePara.maxSeconds * 1000)
                {
                    return (int)Error.TimeOut;
                }
            } while ((status_ & 0x400) != 0);
            
            if ((status_ & 0x40) == 0)
            {
                return (int)Error.NEL;
            }
            return err_;
        }
        
        public int Move(double pos, double spd_ratio = 1.0)
        {
            var mp_ = new MotionPara
            {
                acc = motionPara.acc * spd_ratio * spd_ratio,
                maxVel = motionPara.maxVel * spd_ratio,
            };
            return Move(pos, ref mp_);
        }

        /// <summary>
        /// 单轴点到点运动，不检测完成
        /// </summary>
        /// <param name="pos">目标位置</param>
        /// <param name="mp">运动参数，包含加速度mm/ms^2、速度mm/ms(m/s)</param>
        /// <returns>0表示指令发送成功，负值表示错误码</returns>
        public int Move(double pos, ref MotionPara mp)
        {
            int err_ = 0;
            var p_ = new lhmtc.TrapPrfPrm
            {
                acc = _acc2pls(mp.acc),
                dec = _acc2pls(mp.acc),
                velStart = 0,
                smoothTime = 10
            };
            if ((err_ = lhmtc.LH_PrfTrap(id, false)) < 0)
            {
                return err_;
            }

            if ((err_=lhmtc.LH_SetTrapPrm(id, ref p_, false))<0)
            {
                return err_;
            }
            if((err_=lhmtc.LH_SetPos(id, _mm2pls(pos), false))<0)
            {
                return err_;
            }
            if((err_=lhmtc.LH_SetVel(id, _vel2pls(mp.maxVel), false))<0)
            {
                return err_;
            }
            if ((err_ = lhmtc.LH_Update(1<<(id-1), false)) < 0)
            {
                return err_;
            }
            return 0;
        }


        /// <summary>
        /// 阻塞检测运动完成
        /// </summary>
        /// <returns>0表示成功，负值表示错误码</returns>
        public int MotionDone()
        {
            int err_ = 0;
            int sts_ = 0;
            while(true)
            {
                if((err_=lhmtc.LH_GetSts(id, out st)))
            }
        }
        /// <summary>
        /// 轴复位，包括清除状态、报警。
        /// </summary>
        /// <returns>0表示成功，负值表示错误码</returns>
        public int Reset()
        {
            int ret_ = 0;
            if((ret_=lhmtc.LH_ClrSts(id, 1, false))<0)
            {
                return ret_;
            }
            if ((ret_ = lhmtc.LH_AxisReset(id, 0, false)) < 0)
            {
                return ret_;
            }
            return ret_;
        }

        
        // m/s^2 -> pulse/ms^2
        private double _acc2pls(double mpss)
        {
            return mpss * plsPerMm / 1000.0;
        }
        // mm -> pulse
        private int _mm2pls(double mm)
        {
            return (int)(mm * this.plsPerMm);
        }
        // m/s -> pulse/ms
        private double _vel2pls(double mps)
        {
            return mps * this.plsPerMm;
        }
        
    }



    public class Motion
    {
        private List<Axis> axisList_ = new List<Axis>();
        private bool _running = false;
        
        private void _updateAxesStatus()
        {
            short num_ = (short)AxisNum;
            if (num_ == 0)
            {
                return;
            }
            int[] sts_ = new int[num_];
            double[] cmd_ = new double[num_];
            double[] fbk_ = new double[num_];

            while(_running)
            {
                lhmtc.LH_GetSts(1, out sts_, num_, false);

                lhmtc.LH_GetPrfPos(1, out cmd_, num_, false);
                lhmtc.lhget
            }
        }
        public Axis Axis(int i)
        {
            return axisList_[i];
        }
        public int AxisNum { get { return axisList_.Count; } }
        public int InitSystem(string configFileFolder)
        {
            short err_ = 0;
            string file_ = string.Empty;
            //0. 加载各轴的默认基础参数
            file_ = System.IO.Path.Combine(configFileFolder, "axis.xml");
            if (!System.IO.File.Exists(file_))
            {
                return -1000;
            }
            axisList_.Clear();
            axisList_ = _xmlDeserialFromFile<List<Axis>>(file_);
            //1. 扫描板卡数量
            short card_num_ = 0;
            lhmtc.LH_EnumDevice(out card_num_);

            //2. 打开板卡
            for (short i = 0; i < card_num_; i++)
            {
                lhmtc.LH_Open(i, 1);            
            }
            lhmtc.LH_Reset();

            //3. 设置各个板卡所连轴的系统参数
            for(short i=0; i<card_num_; i++)
            {
                file_ = System.IO.Path.Combine(configFileFolder, i.ToString()+".ini");
                if(!System.IO.File.Exists(file_))
                {
                    return -1;
                }
                if((err_ = lhmtc.LH_LoadConfig(file_))<0)
                {
                    return err_;
                }
            }
            //4. 复位各个轴
            foreach(var a in axisList_)
            {
                a.Reset();
            }
            return 0;
        }

        public int DiscardSystem()
        {
            int err_ = 0;
            foreach(var a in axisList_)
            {
                a.SetEnabled(false);
            }
            if((err_=lhmtc.LH_Close())<0)
            {
                return err_;
            }
            return err_;
        }
        /// <summary>
        /// 保存所有轴轴默认参数到文件
        /// </summary>
        /// <param name="paraFile">文件全名(xml格式)</param>
        public void SaveAxesPara(string paraFile)
        {
            _xmlSerialToFile(this.axisList_, paraFile);
        }
        #region 参数序列化读写操作
        private void _xmlSerialToFile<T>(T obj, string filePath)
        {
            XmlWriter writer = null;    //声明一个xml编写器
            XmlWriterSettings writerSetting = new XmlWriterSettings //声明编写器设置
            {
                Indent = true,//定义xml格式，自动创建新的行
                Encoding = UTF8Encoding.UTF8,//编码格式
            };

            //创建一个保存数据到xml文档的流
            writer = XmlWriter.Create(filePath, writerSetting);            
            XmlSerializer xser = new XmlSerializer(typeof(T));  //实例化序列化对象

            xser.Serialize(writer, obj);  //序列化对象到xml文档
            writer.Close(); 
        }

        private T _xmlDeserialFromFile<T>(string filePath)
        {
            string xmlString = File.ReadAllText(filePath);
            using (MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(xmlString)))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(T));
                return (T)serializer.Deserialize(stream);                
            }
        }
        #endregion
    }
}
