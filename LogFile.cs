using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Management;
using Microsoft.Win32;
using System.Windows.Forms;

namespace WinUAELoader
{
    class LogFile
    {
        public static string FileName = null;

        static LogFile()
        {
        }

        public static void ClearLog()
        {
            try
            {
                using (System.IO.StreamWriter sw = File.CreateText(FileName))
                    sw.Flush();
            }
            catch //(Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void WriteEntry(string Method, string Class, string Error)
        {
            WriteEntry(String.Format("ERROR @ {0} ({1})", Method, Class));
            WriteEntry(Error);
        }

        public static void WriteEntry(string Method, string Class, string Error, string StackTrace)
        {
            WriteEntry(String.Format("ERROR @ {0} ({1})", Method, Class));
            WriteEntry(Error);
            WriteEntry(StackTrace);
        }

        public static void WriteEntry(string Error, string StackTrace)
        {
            WriteEntry(Error);
            WriteEntry(StackTrace);
        }

        public static void WriteEntry(string Entry)
        {
            try
            {
                using (System.IO.StreamWriter sw = File.AppendText(FileName))
                {
                    sw.WriteLine(DateTime.Now + ": " + Entry);
                    sw.Flush();
                }
            }
            catch // (Exception ex)
            {
                //System.Windows.Forms.MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }

        public static void OutputSystemConfiguration()
        {
            try
            {
                ManagementObjectSearcher query = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");

                foreach (ManagementObject mo in query.Get())
                {
                    Double totalRAM = Double.Parse(mo["TotalVisibleMemorySize"].ToString()) / 1024;
                    Double freeRAM = Double.Parse(mo["FreePhysicalMemory"].ToString()) / 1024;

                    WriteEntry("OS: " + mo["Caption"].ToString());
                    WriteEntry("Version: " + mo["Version"].ToString());
                    WriteEntry("Build: " + mo["BuildNumber"].ToString());
                    WriteEntry("RAM Total: " + Math.Ceiling(totalRAM) + " MB");
                    WriteEntry("RAM Used: " + Math.Ceiling(totalRAM - freeRAM) + " MB");
                }

                query = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");

                foreach (ManagementObject obj in query.Get())
                    WriteEntry("CPU: " + obj["Name"].ToString().TrimStart());

                query = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");

                foreach (ManagementObject obj in query.Get())
                {
                    WriteEntry("Video Card: " + obj["Name"].ToString());
                    WriteEntry("Video Driver: " + obj["DriverVersion"].ToString());
                    if (obj["AdapterRAM"] != null)
                    {
                        double videoRAM = double.Parse(obj["AdapterRAM"].ToString()) / 1024 / 1024;
                        LogFile.WriteEntry("Video RAM: " + videoRAM + " MB");
                    }
                }

                query = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");

                foreach (ManagementObject obj in query.Get())
                {
                    WriteEntry("Sound Card: " + obj["Name"].ToString());
                }

                if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v1.0") != null)
                    WriteEntry(".NET: .NET Framework 1.0 Installed");
                if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v1.1") != null)
                    WriteEntry(".NET: .NET Framework 1.1 Installed");
                if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v2.0") != null)
                    WriteEntry(".NET: .NET Framework 2.0 Installed");
                if (Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\.NETFramework\\policy\\v3.0") != null)
                    WriteEntry(".NET: .NET Framework 3.0 Installed");

                query.Dispose();
                query = null;
            }
            catch // (Exception ex)
            {
                //MessageBox.Show(ex.Message + "\r\n" + ex.StackTrace);
            }
        }
    }
}
