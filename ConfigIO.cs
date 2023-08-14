// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WinUAELoader
{
    class ConfigIO
    {
        public static void ReadConfig(string fileName)
        {
            try
            {
                IniFile iniFile = new IniFile(fileName);

                if (!File.Exists(fileName))
                    Settings.General.FirstRun = true;

                Settings.General.LoaderMode = (ROMType)Convert.StrToInt(iniFile.ReadValueDefault("General", "LoaderMode", "0"));

                Settings.File.WinUAEExe = iniFile.ReadValueDefault("Paths", "WinUAEExe", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"WinUAE\winuae.exe"));
                Settings.Folder.GameBaseROMs = iniFile.ReadValueDefault("Paths", "GameBaseROMs", iniFile.ReadValueDefault("Paths", "GameBaseGames", String.Empty));
                Settings.Folder.WHDLoadROMs = iniFile.ReadValueDefault("Paths", "WHDLoadROMs", iniFile.ReadValueDefault("Paths", "WHDLoadGames", String.Empty));
                Settings.Folder.SPSROMs = iniFile.ReadValueDefault("Paths", "SPSROMs", iniFile.ReadValueDefault("Paths", "SPSGames", String.Empty));
                Settings.Folder.DemoBaseROMs = iniFile.ReadValueDefault("Paths", "DemoBaseROMs", String.Empty);
                Settings.File.GameBaseDatabase = iniFile.ReadValueDefault("Paths", "GameBaseDatabase", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"GameBase\GameBase Amiga\GameBase Amiga.mdb"));
                Settings.File.DemoBaseDatabase = iniFile.ReadValueDefault("Paths", "DemoBaseDatabase", Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), @"GameBase\Amiga demobase\Amiga Demobase.mdb"));

                Settings.Input.Enabled = Convert.StrToBool(iniFile.ReadValueDefault("Input", "Enabled", "True"));
                Settings.Input.DiskSwapperKey = (uint) Convert.StrToInt(iniFile.ReadValueDefault("Input", "DiskSwapperKey", ((int)Keys.P).ToString()));
                Settings.Input.Port0 = (InputType) Convert.StrToInt(iniFile.ReadValueDefault("Input", "Port0", "7"));
                Settings.Input.Port1 = (InputType)Convert.StrToInt(iniFile.ReadValueDefault("Input", "Port1", "6"));
                Settings.Input.ExitKey = (uint) Convert.StrToInt(iniFile.ReadValueDefault("Input", "ExitKey", ((int)Keys.Escape).ToString()));
                Settings.Input.MouseSpeed = Convert.StrToInt(iniFile.ReadValueDefault("Input", "MouseSpeed", "2"));
                Settings.Input.EnableMouseMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableMouseMap", "False"));
                Settings.Input.EnableKeyboardAMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableKeyboardAMap", "False"));
                Settings.Input.EnableKeyboardBMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableKeyboardBMap", "False"));
                Settings.Input.EnableKeyboardCMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableKeyboardCMap", "False"));
                Settings.Input.EnableXArcadeLeftMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableXArcadeLeftMap", "False"));
                Settings.Input.EnableXArcadeRightMap = Convert.StrToBool(iniFile.ReadValueDefault("Input", "EnableXArcadeRightMap", "False"));

                Settings.Config.OverwriteConfigs = Convert.StrToBool(iniFile.ReadValueDefault("Config", "OverwriteConfigs", "True"));
                Settings.Config.AmigaConfigFile = iniFile.ReadValueDefault("Config", "AmigaConfigFile", Path.Combine(Settings.Folder.Configs, "A500.UAE"));
                Settings.Config.ROMFolder = iniFile.ReadValueDefault("Config", "ROMFolder", String.Empty);
                Settings.Config.OutputFolder = iniFile.ReadValueDefault("Config", "OutputFolder", String.Empty);
                Settings.Config.OnlyApply = Convert.StrToBool(iniFile.ReadValueDefault("Config", "OnlyApply", "False"));
                Settings.Config.OnlyApplyWord = iniFile.ReadValueDefault("Config", "OnlyApplyWord", "AGA");

                Settings.WHDRun.EnableForceClose = Convert.StrToBool(iniFile.ReadValueDefault("WHDRun", "EnableForceClose", "True"));
                Settings.WHDRun.ForceCloseTimeout = Convert.StrToInt(iniFile.ReadValueDefault("WHDRun", "ForceCloseTimeout", "5"));
                Settings.WHDRun.CLIHide = (CLIHideType)Convert.StrToInt(iniFile.ReadValueDefault("WHDRun", "CLIHide", "0"));
                Settings.WHDRun.DeleteTempFiles = Convert.StrToBool(iniFile.ReadValueDefault("WHDRun", "DeleteTempFiles", "False"));

                Global.WinUAE.SetKeyValues(ref Global.WinUAE.Mouse, iniFile.ReadValueDefault("InputMappings", "Mouse", Global.WinUAE.GetKeyValues(Global.WinUAE.Mouse)));
                Global.WinUAE.SetKeyValues(ref Global.WinUAE.KeyboardA, iniFile.ReadValueDefault("InputMappings", "KeyboardA", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardA)));
                Global.WinUAE.SetKeyValues(ref Global.WinUAE.KeyboardB, iniFile.ReadValueDefault("InputMappings", "KeyboardB", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardB)));
                Global.WinUAE.SetKeyValues(ref Global.WinUAE.KeyboardC, iniFile.ReadValueDefault("InputMappings", "KeyboardC", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardC)));
                Global.WinUAE.SetKeyValues(ref Global.WinUAE.XArcadeLeft, iniFile.ReadValueDefault("InputMappings", "XArcadeLeft", Global.WinUAE.GetKeyValues(Global.WinUAE.XArcadeLeft)));
                Global.WinUAE.SetKeyValues(ref Global.WinUAE.XArcadeRight, iniFile.ReadValueDefault("InputMappings", "XArcadeRight", Global.WinUAE.GetKeyValues(Global.WinUAE.XArcadeRight)));

                Settings.Display.Enabled = Convert.StrToBool(iniFile.ReadValueDefault("Display", "Enabled", "True"));
                Settings.Display.FullScreen = Convert.StrToInt(iniFile.ReadValueDefault("Display", "FullScreen", "0"));
                Settings.Display.VSync = Convert.StrToInt(iniFile.ReadValueDefault("Display", "VSync", "0"));
                Settings.Display.Screen = Convert.StrToInt(iniFile.ReadValueDefault("Display", "Screen", "0"));
                Settings.Display.Depth = Convert.StrToInt(iniFile.ReadValueDefault("Display", "Depth", "0"));
                Settings.Display.Refresh = Convert.StrToInt(iniFile.ReadValueDefault("Display", "Refresh", "0"));
                Settings.Display.Resolution = Convert.StrToInt(iniFile.ReadValueDefault("Display", "Resolution", "1"));
                Settings.Display.LineMode = Convert.StrToInt(iniFile.ReadValueDefault("Display", "LineMode", "1"));
                Settings.Display.CenterHorizontal = Convert.StrToBool(iniFile.ReadValueDefault("Display", "CenterHorizontal", "False"));
                Settings.Display.CenterVertical = Convert.StrToBool(iniFile.ReadValueDefault("Display", "CenterVertical", "False"));
                Settings.Display.BlackerThanBlack = Convert.StrToBool(iniFile.ReadValueDefault("Display", "BlackerThanBlack", "False"));
                Settings.Display.RemoveInterlaceArtifacts = Convert.StrToBool(iniFile.ReadValueDefault("Display", "RemoveInterlaceArtifacts", "False"));
                Settings.Display.FilteredLowResolution = Convert.StrToBool(iniFile.ReadValueDefault("Display", "FilteredLowResolution", "False"));
                Settings.Display.ResolutionAutoswitch = Convert.StrToBool(iniFile.ReadValueDefault("Display", "ResolutionAutoswitch", "False"));

                Settings.Tools.ArtworkSource = iniFile.ReadValueDefault("Tools", "ArtworkSource", String.Empty);
                Settings.Tools.ArtworkDest = iniFile.ReadValueDefault("Tools", "ArtworkDest", String.Empty);
                Settings.Tools.ArtworkType = (ArtworkType) Convert.StrToInt(iniFile.ReadValueDefault("Tools", "ArtworkType", "1"));
                Settings.Tools.ArtworkROMType = (ROMType) Convert.StrToInt(iniFile.ReadValueDefault("Tools", "ArtworkROMType", "0"));

                Settings.GameEx.RemoveMissingGames = Convert.StrToBool(iniFile.ReadValueDefault("GameEx", "RemoveMissingGames", "True"));
                Settings.GameEx.FilterGames = Convert.StrToBool(iniFile.ReadValueDefault("GameEx", "FilterGames", "False"));
                Settings.GameEx.FilterString = iniFile.ReadValueDefault("GameEx", "FilterString", "(It),(De),(Fr),(Es),_It,_De,_Fr,_Es");
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("ReadConfig", "ConfigIO", ex.Message, ex.StackTrace);
            }
        }

        public static void WriteConfig(string fileName)
        {
            try
            {
                IniFile iniFile = new IniFile(fileName);

                iniFile.WriteValue("General", "LoaderMode", ((int)Settings.General.LoaderMode).ToString());

                iniFile.WriteValue("Paths", "WinUAEExe", Settings.File.WinUAEExe);
                iniFile.WriteValue("Paths", "GameBaseROMs", Settings.Folder.GameBaseROMs);
                iniFile.WriteValue("Paths", "WHDLoadROMs", Settings.Folder.WHDLoadROMs);
                iniFile.WriteValue("Paths", "SPSROMs", Settings.Folder.SPSROMs);
                iniFile.WriteValue("Paths", "DemoBaseROMs", Settings.Folder.DemoBaseROMs);
                iniFile.WriteValue("Paths", "GameBaseDatabase", Settings.File.GameBaseDatabase);
                iniFile.WriteValue("Paths", "DemoBaseDatabase", Settings.File.DemoBaseDatabase);

                iniFile.WriteValue("Input", "Enabled", Settings.Input.Enabled.ToString());
                iniFile.WriteValue("Input", "DiskSwapperKey", Settings.Input.DiskSwapperKey.ToString());
                iniFile.WriteValue("Input", "Port0", ((int)Settings.Input.Port0).ToString());
                iniFile.WriteValue("Input", "Port1", ((int)Settings.Input.Port1).ToString());
                iniFile.WriteValue("Input", "ExitKey", Settings.Input.ExitKey.ToString());
                iniFile.WriteValue("Input", "MouseSpeed", Settings.Input.MouseSpeed.ToString());
                iniFile.WriteValue("Input", "EnableMouseMap", Settings.Input.EnableMouseMap.ToString());
                iniFile.WriteValue("Input", "EnableKeyboardAMap", Settings.Input.EnableKeyboardAMap.ToString());
                iniFile.WriteValue("Input", "EnableKeyboardBMap", Settings.Input.EnableKeyboardBMap.ToString());
                iniFile.WriteValue("Input", "EnableKeyboardCMap", Settings.Input.EnableKeyboardCMap.ToString());
                iniFile.WriteValue("Input", "EnableXArcadeLeftMap", Settings.Input.EnableXArcadeLeftMap.ToString());
                iniFile.WriteValue("Input", "EnableXArcadeRightMap", Settings.Input.EnableXArcadeRightMap.ToString());

                iniFile.WriteValue("Config", "OverwriteConfigs", Settings.Config.OverwriteConfigs.ToString());
                iniFile.WriteValue("Config", "AmigaConfigFile", Settings.Config.AmigaConfigFile);
                iniFile.WriteValue("Config", "ROMFolder", Settings.Config.ROMFolder);
                iniFile.WriteValue("Config", "OutputFolder", Settings.Config.OutputFolder);
                iniFile.WriteValue("Config", "OnlyApply", Settings.Config.OnlyApply.ToString());
                iniFile.WriteValue("Config", "OnlyApplyWord", Settings.Config.OnlyApplyWord);

                iniFile.WriteValue("WHDRun", "EnableForceClose", Settings.WHDRun.EnableForceClose.ToString());
                iniFile.WriteValue("WHDRun", "ForceCloseTimeout", Settings.WHDRun.ForceCloseTimeout.ToString());
                iniFile.WriteValue("WHDRun", "CLIHide", ((int)Settings.WHDRun.CLIHide).ToString());
                iniFile.WriteValue("WHDRun", "DeleteTempFiles", Settings.WHDRun.DeleteTempFiles.ToString());

                iniFile.WriteValue("InputMappings", "Mouse", Global.WinUAE.GetKeyValues(Global.WinUAE.Mouse));
                iniFile.WriteValue("InputMappings", "KeyboardA", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardA));
                iniFile.WriteValue("InputMappings", "KeyboardB", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardB));
                iniFile.WriteValue("InputMappings", "KeyboardC", Global.WinUAE.GetKeyValues(Global.WinUAE.KeyboardC));
                iniFile.WriteValue("InputMappings", "XArcadeLeft", Global.WinUAE.GetKeyValues(Global.WinUAE.XArcadeLeft));
                iniFile.WriteValue("InputMappings", "XArcadeRight", Global.WinUAE.GetKeyValues(Global.WinUAE.XArcadeRight));

                iniFile.WriteValue("Display", "Enabled", Settings.Display.Enabled.ToString());
                iniFile.WriteValue("Display", "FullScreen", Settings.Display.FullScreen.ToString());
                iniFile.WriteValue("Display", "VSync", Settings.Display.VSync.ToString());
                iniFile.WriteValue("Display", "Screen", Settings.Display.Screen.ToString());
                iniFile.WriteValue("Display", "Depth", Settings.Display.Depth.ToString());
                iniFile.WriteValue("Display", "Refresh", Settings.Display.Refresh.ToString());
                iniFile.WriteValue("Display", "Resolution", Settings.Display.Resolution.ToString());
                iniFile.WriteValue("Display", "LineMode", Settings.Display.LineMode.ToString());
                iniFile.WriteValue("Display", "CenterHorizontal", Settings.Display.CenterHorizontal.ToString());
                iniFile.WriteValue("Display", "CenterVertical", Settings.Display.CenterVertical.ToString());
                iniFile.WriteValue("Display", "BlackerThanBlack", Settings.Display.BlackerThanBlack.ToString());
                iniFile.WriteValue("Display", "RemoveInterlaceArtifacts", Settings.Display.RemoveInterlaceArtifacts.ToString());
                iniFile.WriteValue("Display", "FilteredLowResolution", Settings.Display.FilteredLowResolution.ToString());
                iniFile.WriteValue("Display", "ResolutionAutoswitch", Settings.Display.ResolutionAutoswitch.ToString());

                iniFile.WriteValue("Tools", "ArtworkSource", Settings.Tools.ArtworkSource.ToString());
                iniFile.WriteValue("Tools", "ArtworkDest", Settings.Tools.ArtworkDest.ToString());
                iniFile.WriteValue("Tools", "ArtworkType", ((int)Settings.Tools.ArtworkType).ToString());
                iniFile.WriteValue("Tools", "ArtworkROMType", ((int)Settings.Tools.ArtworkROMType).ToString());

                iniFile.WriteValue("GameEx", "RemoveMissingGames", Settings.GameEx.RemoveMissingGames.ToString());
                iniFile.WriteValue("GameEx", "FilterGames", Settings.GameEx.FilterGames.ToString());
                iniFile.WriteValue("GameEx", "FilterString", Settings.GameEx.FilterString);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WriteConfig", "ConfigIO", ex.Message, ex.StackTrace);
            }
        }
    }
}
