using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MotionControl;

namespace MotionExample
{
    static class Program
    {        
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            int err_ = 0;
            string folder_ = Application.StartupPath;
            bool online_ = false;
            if((err_ = Motion.InitSystem(folder_, online_)) != 0)
            {
                MessageBox.Show("Motion 初始化失败! code: " + err_.ToString());
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainUi());


            Motion.DiscardSystem();
        }
    }
}
