using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace BLL.CommonClass
{

    /// <summary>
    /// RunExe 的摘要说明。
    /// </summary>
    public class RunExe
    {
        /// <summary>
        /// 运行外部程序
        /// </summary>
        /// <param name="exeName">程序路径</param>
        /// <returns>0:失败，1:成功</returns>
        public bool RunIt(string exeName)
        {
            //声明一个程序信息类
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //设置外部程序名
            Info.FileName = exeName.ToLower();
            //声明一个程序类
            try
            {
                System.Diagnostics.Process Proc;
                Proc = System.Diagnostics.Process.Start(Info);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 判断是否运行
        /// </summary>
        /// <param name="exeName">程序名</param>
        /// <returns>0:没运行，1:运行中</returns>
        public bool IsRun(string exeName)
        {
            string isrunning = "0";
            Process[] myProcesses = Process.GetProcesses();

            foreach (Process myProcess in myProcesses)
            {

                if (myProcess.ProcessName.ToLower() == exeName.ToLower())
                {
                    isrunning = "1";
                    break;
                }
            }
            if (isrunning == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 结束进程
        /// </summary>
        /// <param name="exeName">进程名</param>
        /// <returns>0:失败，1:成功</returns>
        public bool Kill(string exeName)
        {
            string isrunning = "0";
            Process[] myProcesses = Process.GetProcesses();
            foreach (Process myProcess in myProcesses)
            {
                if (myProcess.ProcessName.ToLower() == exeName.ToLower())
                {
                    try
                    {
                        myProcess.Kill();
                        isrunning = "1";
                    }
                    catch
                    {
                        isrunning = "0";
                    }
                    break;
                }
            }
            if (isrunning == "1")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
