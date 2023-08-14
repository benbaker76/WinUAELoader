using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace WinUAELoader
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Settings.Folder.StartupPath = Application.StartupPath;

            Settings.Folder.Data = Path.Combine(Settings.Folder.StartupPath, "Data");
            Settings.Folder.Configs = Path.Combine(Settings.Folder.Data, "Configs");
            Settings.Folder.GameEx = Path.Combine(Settings.Folder.Data, "GameEx");
            Settings.Folder.Media = Path.Combine(Settings.Folder.StartupPath, "Media");
            Settings.Folder.Temp = Path.Combine(Settings.Folder.StartupPath, "Temp");
            Settings.Folder.WHD = Path.Combine(Settings.Folder.StartupPath, "WHD");
            Settings.Folder.WHDRun = Path.Combine(Settings.Folder.StartupPath, "WHDRun");

            Settings.Folder.GameEx_MapFiles = Path.Combine(Settings.Folder.GameEx, "MAP FILES");
            Settings.Folder.GameEx_Database = Path.Combine(Settings.Folder.GameEx, "DATA\\EMULATORS");

            Settings.File.Log = Path.Combine(Settings.Folder.Data, "WinUAELoader.log");
            Settings.File.Config = Path.Combine(Settings.Folder.Data, "WinUAELoader.ini");

            Settings.File.GameEx_GameBaseMap = Path.Combine(Settings.Folder.GameEx_MapFiles, "[PC] Commodore Amiga (GameBase).map");
            Settings.File.GameEx_WHDLoadMap = Path.Combine(Settings.Folder.GameEx_MapFiles, "[PC] Commodore Amiga (WHDLoad).map");
            Settings.File.GameEx_SPSMap = Path.Combine(Settings.Folder.GameEx_MapFiles, "[PC] Commodore Amiga (SPS).map");
            Settings.File.GameEx_DemoBaseMap = Path.Combine(Settings.Folder.GameEx_MapFiles, "[PC] Commodore Amiga (DemoBase).map");

            Settings.File.GameBaseXml = Path.Combine(Settings.Folder.Data, "GameBase.xml");
            Settings.File.WHDLoadXml = Path.Combine(Settings.Folder.Data, "WHDLoad.xml");
            Settings.File.SPSXml = Path.Combine(Settings.Folder.Data, "SPS.xml");
            Settings.File.DemoBaseXml = Path.Combine(Settings.Folder.Data, "DemoBase.xml");

            Settings.File.UAEConfig_GameBase = Path.Combine(Settings.Folder.Data, "GameBase.uae");
            Settings.File.UAEConfig_WHDLoad = Path.Combine(Settings.Folder.Data, "WHDLoad.uae");

            Settings.File.Biography = Path.Combine(Settings.Folder.Data, "Biography.txt");

            Settings.File.GameEx_GameBaseDatabase = Path.Combine(Settings.Folder.GameEx_Database, "[PC] Commodore Amiga (GameBase).mdb");
            Settings.File.GameEx_WHDLoadDatabase = Path.Combine(Settings.Folder.GameEx_Database, "[PC] Commodore Amiga (WHDLoad).mdb");
            Settings.File.GameEx_SPSDatabase = Path.Combine(Settings.Folder.GameEx_Database, "[PC] Commodore Amiga (SPS).mdb");
            Settings.File.GameEx_DemoBaseDatabase = Path.Combine(Settings.Folder.GameEx_Database, "[PC] Commodore Amiga (DemoBase).mdb");

            Settings.File.SystemConfigurationDefault = Path.Combine(Settings.Folder.Data, "system-configuration-default");
            Settings.File.SystemConfigurationBackground = Path.Combine(Settings.Folder.Data, "system-configuration-background");
            Settings.File.SystemConfigurationAll = Path.Combine(Settings.Folder.Data, "system-configuration-all");

            Settings.Media.XArcadeWinUAE = Path.Combine(Settings.Folder.Media, "X-Arcade WinUAE.png");
            Settings.Media.XArcadeKeys = Path.Combine(Settings.Folder.Media, "X-Arcade Keys.png");

            Settings.WHD.Folder.C = Path.Combine(Settings.Folder.WHD, "C");
            Settings.WHD.Folder.Devs = Path.Combine(Settings.Folder.WHD, "Devs");
            Settings.WHD.Folder.Libs = Path.Combine(Settings.Folder.WHD, "Libs");
            Settings.WHD.Folder.S = Path.Combine(Settings.Folder.WHD, "S");
            Settings.WHD.FileName.UserStartup = Path.Combine(Settings.WHD.Folder.S, "user-startup");
            Settings.WHD.FileName.WHDLoadPrefs = Path.Combine(Settings.WHD.Folder.S, "WHDLoad.prefs");

            Settings.WHDRun.Exe = Path.Combine(Settings.Folder.WHDRun, "whdrun.exe");
            Settings.WHDRun.IniFile = Path.Combine(Settings.Folder.WHDRun, @"whdrun-data\" + "__WHDRun__Games.ini");

            LogFile.FileName = Settings.File.Log;
            LogFile.ClearLog();
            LogFile.WriteEntry("WinUAELoader " + Global.Version);

            try
            {
                Global.KeyboardHook = new KeyboardHook();
                Global.SendKeys = new SendKeys();
                Global.WinUAE = new WinUAE();
                Global.GBDatabase = new GBDatabase();
                //Global.DisplayMode = Monitor.EnumDisplaySettings();

                GetResolutionData();

                ConfigIO.ReadConfig(Settings.File.Config);

                Dictionary<string, string> cmdHash = CmdLineToHash(args);
                string cmdValue = String.Empty;
                string fileName = String.Empty;
                bool loadGame = false;

                if (cmdHash.TryGetValue("-game", out cmdValue))
                {
                    loadGame = true;
                    fileName = cmdValue;
                }
                
                if (cmdHash.TryGetValue("-rom", out cmdValue))
                {
                    loadGame = true;
                    fileName = cmdValue;
                }

                if (loadGame)
                {
                    if (cmdHash.TryGetValue("-mode", out cmdValue))
                    {
                        switch (cmdValue.ToLower())
                        {
                            case "gamebase":
                                Settings.General.LoaderMode = ROMType.GameBase;
                                break;
                            case "whdload":
                                Settings.General.LoaderMode = ROMType.WHDLoad;
                                break;
                            case "sps":
                                Settings.General.LoaderMode = ROMType.SPS;
                                break;
                            case "demobase":
                                Settings.General.LoaderMode = ROMType.DemoBase;
                                break;
                            case "auto":
                                if (!Global.WinUAE.TryGetLoaderMode(fileName, ref Settings.General.LoaderMode))
                                    LogFile.WriteEntry(String.Format("Error: \"{0}\" Not Found.", fileName));
                                break;
                        }
                    }
                    else
                        if (!Global.WinUAE.TryGetLoaderMode(fileName, ref Settings.General.LoaderMode))
                            LogFile.WriteEntry(String.Format("Error: \"{0}\" Not Found.", fileName));

                    RunGame(fileName);

                    return;
                }

                using(Global.MainForm = new frmMain())
                    Application.Run(Global.MainForm);
            }
            catch(Exception ex)
            {
                LogFile.WriteEntry("Main", "Program", ex.Message, ex.StackTrace);
            }

            LogFile.WriteEntry("Exiting.");

            Global.KeyboardHook.Dispose();
            Global.SendKeys.Dispose();

            ConfigIO.WriteConfig(Settings.File.Config);
        }

        public static bool RunGame(string cmdLine)
        {
            try
            {
                if (String.IsNullOrEmpty(cmdLine))
                    return false;

                string fileName = Path.GetFileName(cmdLine);

                if (!Global.WinUAE.RunROM(fileName))
                {
                    LogFile.WriteEntry(String.Format("Error Launching: \"{0}\"", fileName));
                    LogFile.WriteEntry("Exiting.");

                    return false;
                }

                if (Global.WinUAE.WinUAEProcess != null)
                    Global.WinUAE.WinUAEProcess.WaitForExit();

                //Global.WinUAE.CloseWHDRunProcess();

                if (Global.WinUAE.WHDRunProcess != null)
                    Global.WinUAE.WHDRunProcess.WaitForExit();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry(ex.Message);
            }

            LogFile.WriteEntry("Exiting.");

            return true;
        }

        public static void GetResolutionData()
        {
            DisplayMode[] DisplayArray = Monitor.EnumDisplaySettings();
            Dictionary<string, string> ScreenHash = new Dictionary<string, string>();
            List<Screen> ScreenArray = new List<Screen>();
            List<int> DepthArray = new List<int>();
            List<int> RefreshArray = new List<int>();

            RefreshArray.Add(0);
            RefreshArray.Add(50);
            RefreshArray.Add(60);

            foreach(DisplayMode displayMode in DisplayArray)
            {
                string ScreenStr = String.Format("{0}x{1}", displayMode.Width, displayMode.Height);

                if (!ScreenHash.ContainsKey(ScreenStr))
                {
                    ScreenHash.Add(ScreenStr, null);
                    ScreenArray.Add(new Screen(displayMode.Width, displayMode.Height));
                }

                if(!DepthArray.Contains(displayMode.BitDepth))
                {
                    switch(displayMode.BitDepth)
                    {
                        case 8:
                        case 16:
                        case 32:
                            DepthArray.Add(displayMode.BitDepth);
                            break;
                    }
                }

                if(!RefreshArray.Contains(displayMode.Frequency))
                {
                    if (displayMode.Frequency > 1)
                        RefreshArray.Add(displayMode.Frequency);
                }
            }

            if (!RefreshArray.Contains(100))
                RefreshArray.Add(100);
            if (!RefreshArray.Contains(120))
                RefreshArray.Add(120);

            DepthArray.Sort();
            RefreshArray.Sort();

            Global.Screen = ScreenArray.ToArray();
            Global.Depth = DepthArray.ToArray();
            Global.Refresh = RefreshArray.ToArray();
        }

        public static Dictionary<string, string> CmdLineToHash(string[] args)
        {
            try
            {
                if (args == null)
                    return null;

                Dictionary<string, string> argHash = new Dictionary<string, string>();

                for (int i = 0; i < args.Length; i++)
                {
                    if (args[i].StartsWith("-"))
                    {
                        string Value = null;

                        if (i + 1 < args.Length)
                            Value = args[i + 1];

                        argHash.Add(args[i].ToLower(), Value);
                    }
                }

                return argHash;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CmdLineToHash", "Program", ex.Message, ex.StackTrace);
            }

            return null;
        }
    }
}