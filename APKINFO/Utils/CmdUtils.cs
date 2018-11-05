
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using APKINFO.Interface;

namespace APKINFO
{
    /// <summary>
    /// CMD命令工具类
    /// </summary>
    public class CmdUtils
    {
        /// <summary>
        /// 执行CMD命令,并等待执行结果
        /// </summary>
        /// <param name="args"></param>
        /// <param name="logView"></param>
        /// <returns></returns>
        public static string[] ExecCmdAndWaitForExit(string args, ILog logView)
        {
            logView.Log("CMD -> " + args);
            Process proc;
            try
            {
                proc = new Process();
                proc.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/C " + args;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;//不创建窗口  
                proc.Start();
                proc.WaitForExit();
                
                string msg = proc.StandardOutput.ReadToEnd();
                string errorMsg = proc.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    logView.Log("cmd -> error result: " + errorMsg);
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    logView.Log("cmd -> result: " + msg);
                }

                return new string[] { msg, errorMsg };
            }
            catch (Exception ex)
            {
                logView.Log("Exception Occurred :" + ex.Message + ", " + ex.StackTrace.ToString());
                LogUtils.WriteLine("CMD :" + args);
                LogUtils.WriteLine("CMD Exception Occurred :" + ex.Message + ", " + ex.StackTrace.ToString());
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            return null;
        }


        /// <summary>
        /// 执行CMD命令,并等待执行结果
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string[] ExecCmdAndWaitForExit(string args)
        {
            Process proc;
            try
            {
                proc = new Process();
                proc.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/C " + args;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;//不创建窗口  
                proc.Start();
                proc.WaitForExit();

                string msg = proc.StandardOutput.ReadToEnd();
                string errorMsg = proc.StandardError.ReadToEnd();
                if (!string.IsNullOrEmpty(errorMsg))
                {
                    Console.WriteLine("cmd -> error result: {0}", errorMsg);
                }
                if (!string.IsNullOrEmpty(msg))
                {
                    Console.WriteLine("cmd -> result: {0}", msg);
                }

                return new string[] { msg, errorMsg };
            }
            catch (Exception ex)
            {
                LogUtils.WriteLine("CMD :" + args);
                LogUtils.WriteLine("CMD Exception Occurred :" + ex.Message + ", " + ex.StackTrace.ToString());
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }
            return null;
        }


        /// <summary>
        /// 执行CMD命令,但不等待执行结果
        /// </summary>
        /// <param name="args"></param>
        public static void ExecCmd(string args)
        {
            try
            {
                Process proc = new Process();
                proc.StartInfo.WorkingDirectory = System.Windows.Forms.Application.StartupPath;
                proc.StartInfo.FileName = "cmd.exe";
                proc.StartInfo.Arguments = "/C " + args;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.RedirectStandardOutput = true;
                proc.StartInfo.RedirectStandardError = true;
                proc.StartInfo.CreateNoWindow = true;//不创建窗口  
                proc.Start();
               
            }
            catch (Exception ex)
            {
                LogUtils.WriteLine("CMD :" + args);
                LogUtils.WriteLine("CMD Exception Occurred :" + ex.Message + ", " + ex.StackTrace.ToString());
                Console.WriteLine("Exception Occurred :{0},{1}", ex.Message, ex.StackTrace.ToString());
            }

        }
    }
}
