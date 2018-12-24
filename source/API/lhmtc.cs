using System.Runtime.InteropServices;
using System;

namespace lhdevice
{

    public class lhmtc
    {
        #region lhmtc接口中用到的结构体
        /*运动模式*/
        public enum MotionMode
        {
            enum_prfmode_trap = 0,
            enum_prfmode_jog,
            enum_prfmode_s,
            enum_prfmode_pt,
            enum_prfmode_pvs,
            enum_prfmode_interpolation,
            enum_prfmode_gear,
            enum_prfmode_follow
        }
        /*点位模式运动参数*/
        public struct TrapPrfPrm
        {
            public double acc;
            public double dec;
            public double velStart;
            public short smoothTime;
        }
        /*JOG模式运动参数*/
        public struct JogPrfPrm
        {
            public double acc;
            public double dec;
            public double smooth;
        }
        /*PID参数*/
        public struct PidParam
        {
            public double kp;
            public double ki;
            public double kd;
            public double kvff;
            public double kaff;
        }

        /*插补运动坐标系参数*/
        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct CrdCfg
        {
            /// short
            public short dimension;
            /// short[8]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I2)]
            public short[] profile;
            /// short
            public short setOriginFlag;
            /// int[8]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 8, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I4)]
            public int[] orignPos;
            /// short
            public short evenTime;
            /// double
            public double synVelMax;
            /// double
            public double synAccMax;
            /// double
            public double synDecSmooth;
            /// double
            public double synDecAbrupt;
        }

        //二维位置比较
        public struct T2DComparePrm
        {
            public short encx;
            public short ency;
            public short source;
            public short outputType;
            public short startLevel;
            public short time;
            public short maxerr;
            public short threshold;
            public short pluseCount;
            public short spacetime;
        }
        public struct T2DCompareData
        {
            public int px;
            public int py;
        }

        [System.Runtime.InteropServices.StructLayoutAttribute(System.Runtime.InteropServices.LayoutKind.Sequential)]
        public struct CrdBufOperation
        {
            public ushort delay;                         // 延时时间
            public short doType;                        // 缓存区IO的类型,0:不输出IO
            public ushort doAddress;					 // IO模块地址
            public ushort doMask;                        // 缓存区IO的输出控制掩码
            public ushort doValue;                       // 缓存区IO的输出值
            public short dacChannel;					 // DAC输出通道
            public short dacValue;					     // DAC输出值
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = System.Runtime.InteropServices.UnmanagedType.U2)]
            public ushort[] dataExt;               // 辅助操作扩展数据
        }


        //前瞻缓冲区；与前瞻相关的数据结构
        public struct CrdBlockData
        {
            public short iMotionType;                             // 运动类型,0为直线插补,1为2D圆弧插补,2为3D圆弧插补,6为IO,7为延时，8位DAC
            public short iCirclePlane;                            // 圆弧插补的平面;XY—1，YZ-2，ZX-3
            public short arcPrmType;							   // 1-圆心表示法；2-半径表示法
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 4, ArraySubType = System.Runtime.InteropServices.UnmanagedType.I4)]
            public int[] lPos;            // 当前段各轴终点位置

            public double dRadius;                                // 圆弧插补的半径
            public short iCircleDir;                             // 圆弧旋转方向,0:顺时针;1:逆时针
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] dCenter;                             // 2维圆弧插补的圆心相对坐标值，即圆心相对于起点位置的偏移量
            // 3维圆弧插补的圆心在用户坐标系下的坐标值
            public int height;								   // 螺旋线的高度
            public double pitch;	// 螺旋线的螺距
            //double[3]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] beginPos;
            //double[3]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] midPos;
            //double[3]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] endPos;
            //double[3][3]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] R_inv;
            //double[3][3]
            [System.Runtime.InteropServices.MarshalAsAttribute(System.Runtime.InteropServices.UnmanagedType.ByValArray, SizeConst = 9, ArraySubType = System.Runtime.InteropServices.UnmanagedType.R8)]
            public double[] R;

            public double dVel;                                   // 当前段合成目标速度
            public double dAcc;                                   // 当前段合成加速度
            public short loop;
            public short iVelEndZero;                             // 标志当前段的终点速度是否强制为0,值0——不强制为0;值1——强制为0
            public CrdBufOperation operation;
            public double dVelEnd;                                // 当前段合成终点速度
            public double dVelStart;                              // 当前段合成的起始速度
            public double dResPos;                                // 当前段合成位移量

        }

        #endregion

        /*初始化部分***********************************************************************************************************/
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_EnumDevice(out short pDeviceNum);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Open(short channel, short param);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Close();
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Reset();
        [DllImport("lhmtc.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetVersion(out string pVersion, int size);
        /*轴基本操作*************************************************************************************************************/
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_AxisOn(short axis, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_AxisOff(short axis, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPrfPos(short profile, int prfPos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ZeroPos(short axis, short count, bool resend);

        //系统参数配置
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPidIndex(short control, short index, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPidIndex(short control, out short pIndex, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPid(short control, short index, ref PidParam pPid, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPid(short control, short index, out PidParam pPid, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPidForVelMode(short control, short index, ref PidParam pPid, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPidForVelMode(short control, short index, out PidParam pPid, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPosErr(short control, int error, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPosErr(short control, out int pError, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetCtrlMode(short axis, short mode, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCtrlMode(short axis, out short mode, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPosLmtSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPosLmtSense(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetNegLmtSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetNegLmtSense(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetAlarmSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetAlarmSense(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetEncoderSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetEncoderSense(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetEncoderSrc(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetEncoderSrc(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetHomeEdge(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetHomeEdge(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetEncIndexEdge(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetEncIndexEdge(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetDacSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetDacSense(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPulseMode(short axis, short sense, bool resend);  //0:脉冲+方向  1:正负脉冲
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPulseMode(short axis, out short sense, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_AxisReset(short axis, short reset, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetStepSense(short axis, short sense, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetStepSense(short axis, out short sense, bool resend);

        //软限位

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SoftLimitEnable(short axis, short sEnable);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetSoftLimit(short axis, int positive, int negative, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetSoftLimit(short axis, out int pPositive, out int pNegative, bool resend);
        [DllImport("lhmtc.dll", CharSet = CharSet.Unicode, CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_LoadConfig(string pFile);
        //io停止加速度
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetStopDec(short axis, double dSmoothDec, double dEmergencyDec);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetStopDec(short axis, out double dSmoothDec, out double dEmergencyDec);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        //停止IO

        public static extern short LH_SetStopIo(short axis, short stopType, short inputType, short inputIndex, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetStopIo(short axis, short stopType, out short pInputType, out short pInputIndex, bool resend);

        /*运动状态检测指令************************************************************************************************************/
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetSts(short axis, out int pSts, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ClrSts(short axis, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPrfMode(short profile, out int pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPrfPos(short profile, out double pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPrfVel(short profile, out double pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPrfAcc(short profile, out double pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Stop(short mask, short option, bool resend);
        //点位运动指令
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PrfTrap(short profile, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetTrapPrm(short profile, ref TrapPrfPrm pPrm, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetTrapPrm(short profile, out TrapPrfPrm pPrm, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPos(short profile, int pos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPos(short profile, out int pPos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetVel(short profile, double vel, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetVel(short profile, out double pVel, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Update(int mask, bool resend);

        //Jog指令
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PrfJog(short profile, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetJogPrm(short profile, ref JogPrfPrm pPrm, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetJogPrm(short profile, out JogPrfPrm pPrm, bool resend);

        //访问数字IO
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetDi(short diType, out int pValue, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetDo(int value, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetDoBit(short doIndex, short value, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetDo(out int pValue, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        //访问扩展数字IO
        public static extern short LH_GetExtendDi(short address, out int pValue, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetExtendDiBit(short address, short diIndex, ref short value, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetExtendDo(short address, int value, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetExtendDoBit(short address, short doIndex, short value, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetExtendDo(short address, out int pValue, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetExtendCardCount(short count, bool resend); //设置IO扩展板的个数
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetDoBitReverse(short doType, short doIndex, short value, short reverseTime, bool resend);


        //访问编码器
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetEncPos(short encoder, out double pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetEncVel(short encoder, out double pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetEncPos(short encoder, int encPos, bool resend);
        //访问DAC
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetDac(short dac, out short pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetDac(short dac, out short pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetAdc(short channel, out short pValue, short count, bool resend);
        //Home/Index硬件捕获
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetCaptureMode(short encoder, short mode, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCaptureMode(short encoder, out short pMode, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCaptureStatus(short encoder, out short pStatus, out int pValue, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetCaptureSense(short encoder, short mode, short sense, bool resend);

        //位置比较功能
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetAxisBand(short axis, int band, int time, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetAxisBand(short axis, out int pBand, out int pTime, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ComparePulse(short level, short outputType, short time, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CompareStop();
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CompareStatus(out short pStatus, out int pCount, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CompareData(short encoder, short source, short pulseType, short startLevel, short time, ref int pBuf1, short count1, ref int pBuf2, short count2, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CompareLinear(short encoder, short channel, int startPos, int repeatTimes, int interval, short time, short source, bool resend);
        //自动回零
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_HomeInit(bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Home(short axis, int pos, double vel, double acc, int offset, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_Index(short axis, int pos, int offset, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_HomeStop(short axis, int pos, double vel, double acc, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_HomeSts(short axis, out ushort pStatus, bool resend);

        //PT模式
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PrfPt(short profile, short count, short mode, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PtSpace(short profile, out short pSpace, short count, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PtData(short profile, int pos, int time, short type, short fifo, short count, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PtClear(short profile, short count, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PtStart(int mask, int option, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPtLoop(short profile, int loop, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPtLoop(short profile, out int loop, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetPtMemory(short profile, short memory, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetPtMemory(short profile, out short memory, bool resend);

        //Gear 运动
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PrfGear(short profile, short dir, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetGearMaster(short profile, short masterindex, short masterType, short masterItem, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetGearMaster(short profile, out short masterindex, out short masterType, out short masterItem, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetGearRatio(short profile, int masterEven, int slaveEven, int masterSlope, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetGearRatio(short profile, out int masterEven, out int slaveEven, out int masterSlope, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GearStart(int mask, bool resend);

        //Follow模式
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_PrfFollow(short profile, short dir, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetFollowMaster(short profile, short masterIndex, short masterType, short masterItem, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetFollowMaster(short profile, out short MasterIndex, out short MasterType, out short MasterItem, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetFollowLoop(short profile, short loop, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetFollowLoop(short profile, out int pLoop, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetFollowEvent(short profile, short even, short masterDir, int pos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetFollowEvent(short profile, out short pEvent, out short pMasterDir, out int pPos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_FollowSpace(short profile, out short pSpace, short count, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_FollowData(short profile, int masterSegment, int slaveSegment, short type, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_FollowClear(short profile, short count, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_FollowStart(int mask, int option, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_FollowSwitch(int mask, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetFollowMemory(short profile, short memory, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetFollowMemory(short profile, out short pMemory, bool resend);

        //插补
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetCrdPrm(short crd, ref CrdCfg pCrdPrm, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCrdPrm(short crd, out CrdCfg pCrdPrm, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CrdSpace(short crd, out int pSpace, short count, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CrdClear(short crd, short count, short fifo, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_LnXY(short crd, int x, int y, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_LnXYZ(short crd, int x, int y, int z, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_LnXYZA(short crd, int x, int y, int z, int a, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcXYR(short crd, int x, int y, double radius, short circleDir, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcYZR(short crd, int y, int z, double radius, short circleDir, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcZXR(short crd, int z, int x, double radius, short circleDir, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcXYC(short crd, int x, int y, double xCenter, double yCenter, short circleDir, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcYZC(short crd, int y, int z, double yCenter, double zCenter, short circleDir, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcZXC(short crd, int z, int x, double zCenter, double xCenter, short circleDir, double synVel, double synAcc, short fifo);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetLastCrdPos(short crd, out int position);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufIO(short crd, ushort address, ushort doMask, ushort doValue, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufDelay(short crd, uint delayTime, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufDac(short crd, short chn, short daValue, bool bGear, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufGear(short crd, short axis, int pos, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufMove(short crd, short moveAxis, int pos, double vel, double acc, short modal, short fifo, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufLmtsOn(short crd, short axis, short limitType, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufLmtsOff(short crd, short axis, short limitType, short fifo, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_BufSetStopIo(short crd, short axis, short stoptype, short inputtype, short inputindex, short fifo, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CrdStart(short mask, short option, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CrdStop(short mask, short option, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_CrdStatus(short crd, out short pSts, out short pCmdNum, out int pSpace, short fifo, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCrdPos(short crd, out double pPos, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCrdVel(short crd, out double pSynVel, bool resend);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetCrdStopDec(short crd, double decSmoothStop, double decAbruptStop, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_GetCrdStopDec(short crd, out double decSmoothStop, out double decAbruptStop, bool resend);

        //前瞻部分

        // x y z是终点坐标， interX, interY, interZ是中间点坐标
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_ArcXYZ(short crd, double x, double y, double z, double interX, double interY, double interZ, double synVel, double synAcc, double velEnd , short fifo );
 

        //基于2维圆弧半径加终点的输入方式的螺旋线插补  xyz是终点坐标 终点坐标要跟螺距信息匹配
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short  LH_HelicalLineXYR(short crd, int x, int y, int z, double radius, short circleDir, double pitch, double synVel, double synAcc, short fifo );
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short  LH_HelicalLineYZR(short crd, int y, int z, int x, double radius, short circleDir, double pitch, double synVel, double synAcc, short fifo );
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_HelicalLineZXR(short crd, int z, int x, int y, double radius, short circleDir, double pitch, double synVel, double synAcc, short fifo );

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        //基于2维圆弧圆心和终点的输入方式的螺旋线插补	xyz是终点坐标 终点坐标要跟螺距信息匹配
        public static extern short  LH_HelicalLineXYC(short crd, int x, int y, int z, double xCenter, double yCenter, short circleDir, double pitch, double synVel, double synAcc, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short  LH_HelicalLineYZC(short crd, int y, int z, int x, double yCenter, double zCenter, short circleDir, double pitch, double synVel, double synAcc, short fifo );
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short  LH_HelicalLineZXC(short crd, int z, int x, int y, double zCenter, double xCenter, short circleDir, double pitch, double synVel, double synAcc, short fifo );

        //基于空间圆弧的螺旋线插补，主要输入参数为定义螺旋线圆柱底面的圆弧的两个点（加上当前起点构成三点圆弧），螺旋线的高度（有正负号，根据右手定则判断螺旋线虚拟z轴的正向），螺旋线的螺距（正数），函数会自动计算螺旋线的终点，用户需要有终点停在哪里的意识
        // x y z是终点坐标， interX, interY, interZ是中间点坐标        
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_HelicalLineXYZ(short crd, double x, double y, double z, double interX, double interY, double interZ, int height, double pitch, double synVel, double synAcc, double velEnd, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_InitLookAhead(short crd, short fifo, double T, double accMax, short n, ref CrdBlockData pLookAheadBuf, bool resend);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short  LH_CrdData(short crd, ref CrdBlockData pCrdData, short fifo , bool resend );


        //二维位置比较
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareMode(short chn, short mode);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DComparePulse(short chn, short level, short outputType, short time, int lPluseCount, short spacetime);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareBindOn(short chn, short time, int lPluseCount, short spacetime, short inputIo, short senselevel);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareGetBindPrm(short chn, ref short bind,ref short time,ref int lPluseCount,ref short spacetime,ref short inputIo,ref short senselevel);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareBindOff(short chn);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareData(short chn, short count, ref T2DCompareData pBuf, short fifo);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareSetPrm(short chn, ref T2DComparePrm pPrm);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareClear(short chn);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareStatus(short chn, ref short pStatus, ref int pCount, ref short pFifo, ref short pFifoCount, ref short pBufCount);

        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareStart(short chn);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_2DCompareStop(short chn);
        [DllImport("lhmtc.dll", CallingConvention = CallingConvention.Cdecl)]
        public static extern short LH_SetComparePort(short chn, short hsio0, short hsio1);

    }
    
}
