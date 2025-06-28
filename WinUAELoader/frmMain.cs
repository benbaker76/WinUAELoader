// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace WinUAELoader
{
    public partial class frmMain : Form
    {
        private class Link
        {
            public string Name = null;
            public string Description = null;

            public Link(string name, string description)
            {
                Name = name;
                Description = description;
            }
        }

        private Link[] WebLinks =
        {
            new Link("www.gameex.net", "GameEx Front End"),
            new Link("kgwhd.whdownload.com", "KG's WHDLoad Game Packs"),
            new Link("www.whdload.de", "WHDLoad Support Page"),
            new Link("classicwb.abime.net", "ClassicWB Packages"),
            new Link("gbamiga.elowar.com", "GameBase Amiga"),
            new Link("www.whdownload.com", "Great WHDLoad Download Site"),
            new Link("eab.abime.net", "English Amiga Board"),
            new Link("www.softpres.org", "Software Preservation Society"),
            new Link("www.amigagames.com", "AmigaGames Forum"),
            new Link("www.lemonamiga.com", "Lemon Amiga"),
            new Link("www.exotica.org.uk", "Exotica")
        };

        public frmMain()
        {
            InitializeComponent();
            CreateWebLinks(grpLinks);
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version;

                this.Text = this.Text.Replace("[VERSION]", version.ToString(3));
                this.lblAbout.Text = this.lblAbout.Text.Replace("[VERSION]", version.ToString(3));

                GetDatabaseVersion();

                txtWinUAEExe.Text = Settings.File.WinUAEExe;

                txtGameBaseROMs.Text = Settings.Folder.GameBaseROMs;
                txtWHDLoadROMs.Text = Settings.Folder.WHDLoadROMs;
                txtSPSROMs.Text = Settings.Folder.SPSROMs;
                txtDemoBaseROMs.Text = Settings.Folder.DemoBaseROMs;

                txtGameBaseDatabase.Text = Settings.File.GameBaseDatabase;
                txtDemoBaseDatabase.Text = Settings.File.DemoBaseDatabase;

                txtDiskSwapperKey.Text = Settings.Input.DiskSwapperKey.ToString();

                chkEnableInputOptions.Checked = Settings.Input.Enabled;

                cboPort0.Items.AddRange(Global.WinUAE.Input);
                cboPort0.SelectedIndex = (int)Settings.Input.Port0;

                cboPort1.Items.AddRange(Global.WinUAE.Input);
                cboPort1.SelectedIndex = (int)Settings.Input.Port1;

                txtExitKey.Text = Settings.Input.ExitKey.ToString();
                chkEnableForceClose.Checked = Settings.WHDRun.EnableForceClose;
                txtForceCloseTimeout.Text = Settings.WHDRun.ForceCloseTimeout.ToString();

                chkOverwriteConfigs.Checked = Settings.Config.OverwriteConfigs;
                txtAmigaConfigFile.Text = Settings.Config.AmigaConfigFile;
                txtROMFolder.Text = Settings.Config.ROMFolder;
                txtOutputFolder.Text = Settings.Config.OutputFolder;
                chkOnlyApply.Checked = Settings.Config.OnlyApply;
                txtOnlyApplyWord.Text = Settings.Config.OnlyApplyWord;

                this.cboScreen.SelectedIndexChanged -= new System.EventHandler(this.cboScreen_SelectedIndexChanged);
                this.cboDepth.SelectedIndexChanged -= new System.EventHandler(this.cboDepth_SelectedIndexChanged);
                this.cboRefresh.SelectedIndexChanged -= new System.EventHandler(this.cboRefresh_SelectedIndexChanged);
                this.cboResolution.SelectedIndexChanged -= new System.EventHandler(this.cboResolution_SelectedIndexChanged);
                this.cboLineMode.SelectedIndexChanged -= new System.EventHandler(this.cboLineMode_SelectedIndexChanged);

                chkEnableDisplayOptions.Checked = Settings.Display.Enabled;

                cboFullScreen.Items.Clear();
                cboFullScreen.Items.AddRange(Global.WinUAE.FullScreen);

                cboVSync.Items.Clear();
                cboVSync.Items.AddRange(Global.WinUAE.VSync);

                cboResolution.Items.Clear();
                cboResolution.Items.AddRange(Global.WinUAE.Resolution);

                cboLineMode.Items.Clear();
                cboLineMode.Items.AddRange(Global.WinUAE.LineMode);

                if (cboScreen.Items.Count > 0)
                {
                    if (Settings.Display.Screen < cboScreen.Items.Count)
                        cboScreen.SelectedIndex = Settings.Display.Screen;
                    else
                        cboScreen.SelectedIndex = 0;
                }

                if (cboDepth.Items.Count > 0)
                {
                    if (Settings.Display.Depth < cboDepth.Items.Count)
                        cboDepth.SelectedIndex = Settings.Display.Depth;
                    else
                        cboDepth.SelectedIndex = 0;
                }

                if (cboRefresh.Items.Count > 0)
                {
                    if (Settings.Display.Refresh < cboRefresh.Items.Count)
                        cboRefresh.SelectedIndex = Settings.Display.Refresh;
                    else
                        cboRefresh.SelectedIndex = 0;
                }

                cboFullScreen.SelectedIndex = Settings.Display.FullScreen;
                cboVSync.SelectedIndex = Settings.Display.VSync;
                cboResolution.SelectedIndex = Settings.Display.Resolution;
                cboLineMode.SelectedIndex = Settings.Display.LineMode;
                chkCenterHorizontal.Checked = Settings.Display.CenterHorizontal;
                chkCenterVertical.Checked = Settings.Display.CenterVertical;
                chkBlackerThanBlack.Checked = Settings.Display.BlackerThanBlack;
                chkRemoveInterlaceArtifacts.Checked = Settings.Display.RemoveInterlaceArtifacts;
                chkBlackerThanBlack.Checked = Settings.Display.BlackerThanBlack;
                chkRemoveInterlaceArtifacts.Checked = Settings.Display.RemoveInterlaceArtifacts;
                chkFilteredLowResolution.Checked = Settings.Display.FilteredLowResolution;
                chkResolutionAutoswitch.Checked = Settings.Display.ResolutionAutoswitch;

                this.cboScreen.SelectedIndexChanged += new System.EventHandler(this.cboScreen_SelectedIndexChanged);
                this.cboDepth.SelectedIndexChanged += new System.EventHandler(this.cboDepth_SelectedIndexChanged);
                this.cboRefresh.SelectedIndexChanged += new System.EventHandler(this.cboRefresh_SelectedIndexChanged);
                this.cboResolution.SelectedIndexChanged += new System.EventHandler(this.cboResolution_SelectedIndexChanged);
                this.cboLineMode.SelectedIndexChanged += new System.EventHandler(this.cboLineMode_SelectedIndexChanged);

                switch (Settings.WHDRun.CLIHide)
                {
                    case CLIHideType.Default:
                        rdoCLIHideDefault.Checked = true;
                        break;
                    case CLIHideType.Background:
                        rdoCLIHideBackground.Checked = true;
                        break;
                    case CLIHideType.All:
                        rdoCLIHideAll.Checked = true;
                        break;
                }

                cboRunROMType.Items.AddRange(Global.RomTypeString);
                cboRunROMType.SelectedIndex = (int) Settings.General.LoaderMode;

                tabControlInputMap.SelectedIndex = 0;
                PopulateInputMaps();
                trkMouseSpeed.Value = Settings.Input.MouseSpeed;

                txtSourceFolder.Text = Settings.Tools.ArtworkSource;
                txtDestinationFolder.Text = Settings.Tools.ArtworkDest;

                cboArtworkROMType.Items.AddRange(Global.RomTypeString);
                cboArtworkROMType.SelectedIndex = (int) Settings.Tools.ArtworkROMType;

                cboArtworkType.Items.AddRange(Global.ArtworkTypeString);
                cboArtworkType.SelectedIndex = (int) Settings.Tools.ArtworkType;

                chkDeleteTempFiles.Checked = Settings.WHDRun.DeleteTempFiles;

                chkRemoveMissingGames.Checked = Settings.GameEx.RemoveMissingGames;
                chkFilterGames.Checked = Settings.GameEx.FilterGames;
                txtFilterString.Text = Settings.GameEx.FilterString;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("MainForm_Load", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void txtWinUAEExe_TextChanged(object sender, EventArgs e)
        {
            Settings.File.WinUAEExe = txtWinUAEExe.Text;
        }

        private void txtGameBaseROMs_TextChanged(object sender, EventArgs e)
        {
            Settings.Folder.GameBaseROMs = txtGameBaseROMs.Text;
        }

        private void txtWHDLoadROMs_TextChanged(object sender, EventArgs e)
        {
            Settings.Folder.WHDLoadROMs = txtWHDLoadROMs.Text;
        }

        private void txtSPSROMs_TextChanged(object sender, EventArgs e)
        {
            Settings.Folder.SPSROMs = txtSPSROMs.Text;
        }

        private void txtDemoBaseROMs_TextChanged(object sender, EventArgs e)
        {
            Settings.Folder.DemoBaseROMs = txtDemoBaseROMs.Text;
        }

        private void txtGameBaseDatabase_TextChanged(object sender, EventArgs e)
        {
            Settings.File.GameBaseDatabase = txtGameBaseDatabase.Text;
        }

        private void txtDemoBaseDatabase_TextChanged(object sender, EventArgs e)
        {
            Settings.File.DemoBaseDatabase = txtDemoBaseDatabase.Text;
        }

        private void butWinUAEExe_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                if (FileIO.TryOpenFile(this, null, null, ".exe", out fileName))
                    txtWinUAEExe.Text = fileName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butWinUAEExe_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butGameBaseROMs_Click(object sender, EventArgs e)
        {
            try
            {
                string folderName = null;

                if (FileIO.TryOpenFolder(this, null, out folderName))
                    txtGameBaseROMs.Text = folderName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butGameBaseROMs_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butWHDLoadROMs_Click(object sender, EventArgs e)
        {
            try
            {
                string folderName = null;

                if (FileIO.TryOpenFolder(this, null, out folderName))
                    txtWHDLoadROMs.Text = folderName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butGameBaseROMs_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butSPSROMs_Click(object sender, EventArgs e)
        {
            try
            {
                string folderName = null;

                if (FileIO.TryOpenFolder(this, null, out folderName))
                    txtSPSROMs.Text = folderName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butSPSROMs_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butDemoBaseROMs_Click(object sender, EventArgs e)
        {
            try
            {
                string folderName = null;

                if (FileIO.TryOpenFolder(this, null, out folderName))
                    txtDemoBaseROMs.Text = folderName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butDemoBaseROMs_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butGameBaseDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                if (FileIO.TryOpenFile(this, null, null, ".mdb", out fileName))
                    txtGameBaseDatabase.Text = fileName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butGameBaseDatabase_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butDemoBaseDatabase_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                if (FileIO.TryOpenFile(this, null, null, ".mdb", out fileName))
                    txtDemoBaseDatabase.Text = fileName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butDemoBaseDatabase_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butConfigureJoysticks_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start("rundll32.exe", "shell32.dll,Control_RunDLL joy.cpl");
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butConfigureJoysticks_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void txtDiskSwapperKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Input.DiskSwapperKey = (uint) Convert.StrToInt(txtDiskSwapperKey.Text);
        }

        private void butDiskSwapperKey_Click(object sender, EventArgs e)
        {
            try
            {
                int KeyCode = 0;

                if (frmInput.TryGetKey(ref KeyCode))
                    txtDiskSwapperKey.Text = KeyCode.ToString();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butDiskSwapperKey_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void chkEnableInputOptions_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Input.Enabled = chkEnableInputOptions.Checked;
        }

        private void cboPort0_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Input.Port0 = (InputType) cboPort0.SelectedIndex;
        }

        private void cboPort1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Input.Port1 = (InputType)cboPort1.SelectedIndex;
        }

        private void txtExitKey_TextChanged(object sender, EventArgs e)
        {
            Settings.Input.ExitKey = (uint) Convert.StrToInt(txtExitKey.Text);
        }

        private void chkEnableForceClose_CheckedChanged(object sender, EventArgs e)
        {
            Settings.WHDRun.EnableForceClose = chkEnableForceClose.Checked;
        }

        private void txtForceCloseTimeout_TextChanged(object sender, EventArgs e)
        {
            Settings.WHDRun.ForceCloseTimeout = Convert.StrToInt(txtForceCloseTimeout.Text);
        }

        private void chkOverwriteConfigs_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Config.OverwriteConfigs = chkOverwriteConfigs.Checked;
        }

        private void butExitKey_Click(object sender, EventArgs e)
        {
            try
            {
                int KeyCode = 0;

                if (frmInput.TryGetKey(ref KeyCode))
                    txtExitKey.Text = KeyCode.ToString();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butExitKey_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void chkEnableInputMap_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                switch (tabControlInputMap.SelectedIndex)
                {
                    case 0: // Mouse
                        Settings.Input.EnableMouseMap = chkEnableInputMap.Checked;
                        break;
                    case 1: // KeyboardA
                        Settings.Input.EnableKeyboardAMap = chkEnableInputMap.Checked;
                        break;
                    case 2: // KeyboardB
                        Settings.Input.EnableKeyboardBMap = chkEnableInputMap.Checked;
                        break;
                    case 3: // KeyboardC
                        Settings.Input.EnableKeyboardCMap = chkEnableInputMap.Checked;
                        break;
                    case 4: // XArcadeLeft
                        Settings.Input.EnableXArcadeLeftMap = chkEnableInputMap.Checked;
                        break;
                    case 5: // XArcadeRight
                        Settings.Input.EnableXArcadeRightMap = chkEnableInputMap.Checked;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("chkEnableInputMap_CheckedChanged", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void trkMouseSpeed_Scroll(object sender, EventArgs e)
        {
            Settings.Input.MouseSpeed = trkMouseSpeed.Value;
        }

        private void butXArcadeDefaults_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmShowPicture showPic = new frmShowPicture("X-Arcade Defaults", Settings.Media.XArcadeWinUAE))
                    showPic.ShowDialog(Global.MainForm);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butXArcadeDefaults_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butXArcadeKeys_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmShowPicture showPic = new frmShowPicture("X-Arcade Keys", Settings.Media.XArcadeKeys))
                    showPic.ShowDialog(Global.MainForm);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butXArcadeKeys_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void tabControlInputMap_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateInputMaps();
        }

        private void butMapInput_Click(object sender, EventArgs e)
        {
            try
            {
                if (lvwKeyMappings.SelectedItems.Count == 0)
                    return;

                int KeyCode = 0;

                if (frmInput.TryGetKey(ref KeyCode))
                {
                    int selectedIndex = lvwKeyMappings.SelectedItems[0].Index;

                    lvwKeyMappings.SelectedItems[0].SubItems[2].Text = ((Keys)KeyCode).ToString();

                    switch (tabControlInputMap.SelectedIndex)
                    {
                        case 0: // Mouse
                            Global.WinUAE.Mouse[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                        case 1: // KeyboardA
                            Global.WinUAE.KeyboardA[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                        case 2: // KeyboardB
                            Global.WinUAE.KeyboardB[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                        case 3: // KeyboardC
                            Global.WinUAE.KeyboardC[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                        case 4: // XArcadeLeft
                            Global.WinUAE.XArcadeLeft[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                        case 5: // XArcadeRight
                            Global.WinUAE.XArcadeRight[selectedIndex].NewInput = (uint)KeyCode;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butMapInput_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butWHDLoadConfig_Click(object sender, EventArgs e)
        {
            Global.WinUAE.LaunchWinUAEGUI(Settings.File.UAEConfig_WHDLoad);
        }

        private void butGameBaseConfig_Click(object sender, EventArgs e)
        {
            Global.WinUAE.LaunchWinUAEGUI(Settings.File.UAEConfig_GameBase);
        }

        private void txtAmigaConfigFile_TextChanged(object sender, EventArgs e)
        {
            Settings.Config.AmigaConfigFile = txtAmigaConfigFile.Text;
        }

        private void txtROMFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Config.ROMFolder = txtROMFolder.Text;
        }

        private void txtOutputFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Config.OutputFolder = txtOutputFolder.Text;
        }

        private void chkOnlyApply_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Config.OnlyApply = chkOnlyApply.Checked;
        }

        private void txtOnlyApplyWord_TextChanged(object sender, EventArgs e)
        {
            Settings.Config.OnlyApplyWord = txtOnlyApplyWord.Text;
        }

        private void butAmigaConfigFile_Click(object sender, EventArgs e)
        {
            string fileName = null;

            if(FileIO.TryOpenFile(this, Settings.Folder.Configs, txtAmigaConfigFile.Text, ".uae", out fileName))
                txtAmigaConfigFile.Text = fileName;
        }

        private void butROMFolder_Click(object sender, EventArgs e)
        {
            string folder = null;

            if(FileIO.TryOpenFolder(this, txtROMFolder.Text, out folder))
                txtROMFolder.Text = folder;
        }

        private void butOutputFolder_Click(object sender, EventArgs e)
        {
            string folder = null;

            if (FileIO.TryOpenFolder(this, txtOutputFolder.Text, out folder))
                txtOutputFolder.Text = folder;
        }

        private void butGenUAEFiles_Click(object sender, EventArgs e)
        {
            try
            {
                List<string> files = new List<string>();

                DirectoryInfo di = new DirectoryInfo(txtROMFolder.Text);

                foreach (FileInfo fi in di.GetFiles("*.adf"))
                    files.Add(fi.FullName);

                files.Sort();
                List<string> configFiles = new List<string>();
                List<UAENode> UAEConfigArray = Global.WinUAE.ReadUAEConfig(txtAmigaConfigFile.Text);

                string file = String.Empty;
                string oldFile = String.Empty;

                foreach (string f in files)
                {
                    if (chkOnlyApply.Checked)
                        if (!f.Contains(txtOnlyApplyWord.Text))
                            continue;

                    if (f.IndexOf("(") != -1)
                        file = f.Substring(0, f.IndexOf("("));
                    else
                        file = f;

                    if (file != oldFile)
                    {
                        Global.WinUAE.WriteUAEConfig(txtOutputFolder.Text, UAEConfigArray, configFiles);
                        configFiles.Clear();
                    }
                    configFiles.Add(f);
                    oldFile = file;

                    Application.DoEvents();
                }

                Global.WinUAE.WriteUAEConfig(txtOutputFolder.Text, UAEConfigArray, configFiles);

                MessageBox.Show(this, "Done!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, "Error!");
                LogFile.WriteEntry("butGenUAEFiles_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void chkEnableDisplayOptions_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.Enabled = chkEnableDisplayOptions.Checked;
        }

        private void cboFullScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.FullScreen = cboFullScreen.SelectedIndex;

            SetResolution();
        }

        private void cboVSync_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.VSync = cboVSync.SelectedIndex;
        }

        private void cboScreen_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.Screen = cboScreen.SelectedIndex;
        }

        private void cboDepth_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.Depth = cboDepth.SelectedIndex;
        }

        private void cboRefresh_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.Refresh = cboRefresh.SelectedIndex;
        }

        private void cboResolution_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.Resolution = cboResolution.SelectedIndex;
        }

        private void cboLineMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Display.LineMode = cboLineMode.SelectedIndex;
        }

        private void chkCenterHorizontal_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.CenterHorizontal = chkCenterHorizontal.Checked;
        }

        private void chkCenterVertical_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.CenterVertical = chkCenterVertical.Checked;
        }

        private void chkBlackerThanBlack_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.BlackerThanBlack = chkBlackerThanBlack.Checked;
        }

        private void chkRemoveInterlaceArtifacts_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.RemoveInterlaceArtifacts = chkRemoveInterlaceArtifacts.Checked;
        }

        private void chkFilteredLowResolution_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.FilteredLowResolution = chkFilteredLowResolution.Checked;
        }

        private void chkResolutionAutoswitch_CheckedChanged(object sender, EventArgs e)
        {
            Settings.Display.ResolutionAutoswitch = chkResolutionAutoswitch.Checked;
        }

        private void rdoCLIHideDefault_CheckedChanged(object sender, EventArgs e)
        {
            SetCLIHide();
        }

        private void rdoCLIHideBackground_CheckedChanged(object sender, EventArgs e)
        {
            SetCLIHide();
        }

        private void rdoCLIHideAll_CheckedChanged(object sender, EventArgs e)
        {
            SetCLIHide();
        }

        void WebLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                LinkLabel linkLabel = (LinkLabel)sender;

                string addr = linkLabel.Text;

                Process.Start("http://" + addr);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("WebLink_LinkClicked", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butSourceFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                if (FileIO.TryOpenFolder(this, txtSourceFolder.Text, out fileName))
                    txtSourceFolder.Text = fileName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butSourceFolder_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butDestinationFolder_Click(object sender, EventArgs e)
        {
            try
            {
                string fileName = null;

                if (FileIO.TryOpenFolder(this, txtDestinationFolder.Text, out fileName))
                    txtDestinationFolder.Text = fileName;
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butDestinationFolder_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void butConvertArtwork_Click(object sender, EventArgs e)
        {
            try
            {
                tabControlMain.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                switch((ROMType)cboArtworkROMType.SelectedIndex)
                {
                    case ROMType.GameBase:
                        ConvertArtworkGameBase(Settings.File.GameBaseXml, "GameBase", Global.GBDatabase.GameBaseArray);
                        break;
                    case ROMType.WHDLoad:
                        ConvertArtworkWHDLoad();
                        break;
                    case ROMType.SPS:
                        ConvertArtworkSPS();
                        break;
                    case ROMType.DemoBase:
                        ConvertArtworkGameBase(Settings.File.DemoBaseXml, "DemoBase", Global.GBDatabase.DemoBaseArray);
                        break;
                }

            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butConvertArtworks_Click", "MainForm", ex.Message, ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControlMain.Enabled = true;
            }
        }

        private void butGenerateGameLists_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Settings.File.GameBaseDatabase))
                {
                    MessageBox.Show("GameBase Database Not Found!", "Generate Game Lists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tabControlMain.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                Global.GBDatabase.GetGameBaseGameList(Settings.File.GameBaseDatabase, ref Global.GBDatabase.GameBaseHash, ref Global.GBDatabase.GameBaseArray, ref Global.GBDatabase.GameBaseTotal, ref Global.GBDatabase.GameBaseVersion);
                Global.GBDatabase.GetWHDLoadGameList(Settings.File.GameBaseDatabase);
                Global.GBDatabase.GetSPSGameList(Settings.File.GameBaseDatabase);

                Global.GBDatabase.GameBaseArray.Sort();
                Global.GBDatabase.WHDLoadArray.Sort();
                Global.GBDatabase.SPSArray.Sort();

                Global.GBDatabase.AddGameExGameBaseDatabase(Settings.File.GameEx_GameBaseDatabase);
                Global.GBDatabase.AddGameExWHDLoadDatabase(Settings.File.GameEx_WHDLoadDatabase);
                Global.GBDatabase.AddGameExSPSDatabase(Settings.File.GameEx_SPSDatabase);

                Global.GBDatabase.WriteGameBaseMapFile(Settings.File.GameEx_GameBaseMap, Settings.Folder.GameBaseROMs, Global.GBDatabase.GameBaseArray);
                Global.GBDatabase.WriteWHDLoadMapFile(Settings.File.GameEx_WHDLoadMap);
                Global.GBDatabase.WriteSPSMapFile(Settings.File.GameEx_SPSMap);

                Global.GBDatabase.WriteGameBaseXml(Settings.File.GameBaseXml);
                Global.GBDatabase.WriteWHDLoadXml(Settings.File.WHDLoadXml);
                Global.GBDatabase.WriteSPSXml(Settings.File.SPSXml);

                GetDatabaseVersion();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butGenerateGameLists_Click", "MainForm", ex.Message, ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControlMain.Enabled = true;
            }
        }

        private void butGenerateDemoLists_Click(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(Settings.File.DemoBaseDatabase))
                {
                    MessageBox.Show("DemoBase Database Not Found!", "Generate Demo Lists", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                tabControlMain.Enabled = false;
                this.Cursor = Cursors.WaitCursor;

                Global.GBDatabase.GetGameBaseGameList(Settings.File.DemoBaseDatabase, ref Global.GBDatabase.DemoBaseHash, ref Global.GBDatabase.DemoBaseArray, ref Global.GBDatabase.DemoBaseTotal, ref Global.GBDatabase.DemoBaseVersion);

                Global.GBDatabase.DemoBaseArray.Sort();

                Global.GBDatabase.FixDemoBaseData();

                Global.GBDatabase.AddGameExDemoBaseDatabase(Settings.File.GameEx_DemoBaseDatabase);

                Global.GBDatabase.WriteGameBaseMapFile(Settings.File.GameEx_DemoBaseMap, Settings.Folder.DemoBaseROMs, Global.GBDatabase.DemoBaseArray);

                Global.GBDatabase.WriteDemoBaseXml(Settings.File.DemoBaseXml);

                GetDatabaseVersion();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butGenerateDemoLists_Click", "MainForm", ex.Message, ex.StackTrace);
            }
            finally
            {
                this.Cursor = Cursors.Default;
                tabControlMain.Enabled = true;
            }
        }

        private void cboRunROMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.General.LoaderMode = (ROMType)cboRunROMType.SelectedIndex;
        }

        private void butRunROM_Click(object sender, EventArgs e)
        {
            try
            {
                using (frmRunROM runROM = new frmRunROM())
                    runROM.ShowDialog(this);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("butRunROM_Click", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void txtSourceFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Tools.ArtworkSource = txtSourceFolder.Text;
        }

        private void txtDestinationFolder_TextChanged(object sender, EventArgs e)
        {
            Settings.Tools.ArtworkDest = txtDestinationFolder.Text;
        }

        private void cboArtworkROMType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Tools.ArtworkROMType = (ROMType)cboArtworkROMType.SelectedIndex;
        }

        private void cboArtworkType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Settings.Tools.ArtworkType = (ArtworkType) cboArtworkType.SelectedIndex;
        }

        private void chkDeleteTempFiles_CheckedChanged(object sender, EventArgs e)
        {
            Settings.WHDRun.DeleteTempFiles = chkDeleteTempFiles.Checked;
        }

        private void chkRemoveMissingGames_CheckedChanged(object sender, EventArgs e)
        {
            Settings.GameEx.RemoveMissingGames = chkRemoveMissingGames.Checked;
        }

        private void chkFilterGames_CheckedChanged(object sender, EventArgs e)
        {
            Settings.GameEx.FilterGames = chkFilterGames.Checked;
        }

        private void txtFilterString_TextChanged(object sender, EventArgs e)
        {
            Settings.GameEx.FilterString = txtFilterString.Text;
        }

        private void butGenerateMapFiles_Click(object sender, EventArgs e)
        {
            tabControlMain.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            if (Global.GBDatabase.GameBaseArray.Count == 0)
                Global.GBDatabase.ReadGameBaseXml(Settings.File.GameBaseXml, Global.GBDatabase.GameBaseArray);

            if (Global.GBDatabase.WHDLoadArray.Count == 0)
                Global.GBDatabase.ReadWHDLoadXml();

            if (Global.GBDatabase.SPSArray.Count == 0)
                Global.GBDatabase.ReadSPSXml();

            if (Global.GBDatabase.DemoBaseArray.Count == 0)
                Global.GBDatabase.ReadGameBaseXml(Settings.File.DemoBaseXml, Global.GBDatabase.DemoBaseArray);

            Global.GBDatabase.WriteGameBaseMapFile(Settings.File.GameEx_GameBaseMap, Settings.Folder.GameBaseROMs, Global.GBDatabase.GameBaseArray);
            Global.GBDatabase.WriteWHDLoadMapFile(Settings.File.GameEx_WHDLoadMap);
            Global.GBDatabase.WriteSPSMapFile(Settings.File.GameEx_SPSMap);
            Global.GBDatabase.WriteGameBaseMapFile(Settings.File.GameEx_DemoBaseMap, Settings.Folder.DemoBaseROMs, Global.GBDatabase.DemoBaseArray);

            this.Cursor = Cursors.Default;
            tabControlMain.Enabled = true;
        }

        private void GetDatabaseVersion()
        {
            try
            {
                Global.GBDatabase.GetGameBaseVersionXml(Settings.File.GameBaseXml, "GameBase", ref Global.GBDatabase.GameBaseVersion);
                Global.GBDatabase.GetGameBaseVersionXml(Settings.File.DemoBaseXml, "DemoBase", ref Global.GBDatabase.DemoBaseVersion);

                lblGameBaseVersion.Text = String.Format("GameBase Version: {0}", Global.GBDatabase.GameBaseVersion);
                lblDemoBaseVersion.Text = String.Format("DemoBase Version: {0}", Global.GBDatabase.DemoBaseVersion);
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("GetDatabaseVersion", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void ConvertArtworkGameBase(string gamebaseXml, string folderName, List<GameBaseNode> gameBaseArray)
        {
            if (gameBaseArray.Count == 0)
                Global.GBDatabase.ReadGameBaseXml(gamebaseXml, gameBaseArray);

            if (!Directory.Exists(txtDestinationFolder.Text))
                Directory.CreateDirectory(txtDestinationFolder.Text);

            foreach (GameBaseNode gamebaseNode in gameBaseArray)
            {
                if (!String.IsNullOrEmpty(gamebaseNode.FileName) && !String.IsNullOrEmpty(gamebaseNode.ScrnshotFilename))
                {
                    string RomFile = Path.GetFileName(gamebaseNode.FileName);
                    string ScrnshotFile = Path.GetFileName(gamebaseNode.ScrnshotFilename);
                    string SourceFileA = null;
                    string SourceFileB = null;
                    string DestFile = null;

                    switch (Settings.Tools.ArtworkType)
                    {
                        case ArtworkType.Titlescreen:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, gamebaseNode.ScrnshotFilename);
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile);
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot1:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, gamebaseNode.ScrnshotFilename.Substring(0, gamebaseNode.ScrnshotFilename.LastIndexOf('.')) + "_1.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_1.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot2:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, gamebaseNode.ScrnshotFilename.Substring(0, gamebaseNode.ScrnshotFilename.LastIndexOf('.')) + "_2.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_2.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.BoxArtFront:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(gamebaseNode.ScrnshotFilename), gamebaseNode.Name + "_Front.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), gamebaseNode.Name + "_Front.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                        case ArtworkType.BoxArtBack:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(gamebaseNode.ScrnshotFilename), gamebaseNode.Name + "_Back.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), gamebaseNode.Name + "_Back.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                    }

                    if (File.Exists(SourceFileA))
                        File.Copy(SourceFileA, DestFile, true);
                    else if (File.Exists(SourceFileB))
                        File.Copy(SourceFileB, DestFile, true);
                }
            }
        }

        private void ConvertArtworkWHDLoad()
        {
            if (Global.GBDatabase.WHDLoadArray.Count == 0)
                Global.GBDatabase.ReadWHDLoadXml();

            if (!Directory.Exists(txtDestinationFolder.Text))
                Directory.CreateDirectory(txtDestinationFolder.Text);

            foreach (WHDLoadNode whdloadNode in Global.GBDatabase.WHDLoadArray)
            {
                if (!String.IsNullOrEmpty(whdloadNode.ScrnshotFilename) && !String.IsNullOrEmpty(whdloadNode.ScrnshotFilename))
                {
                    string RomFile = Path.GetFileName(whdloadNode.FileName);
                    string ScrnshotFile = Path.GetFileName(whdloadNode.ScrnshotFilename);
                    string SourceFileA = null;
                    string SourceFileB = null;
                    string DestFile = null;

                    switch (Settings.Tools.ArtworkType)
                    {
                        case ArtworkType.Titlescreen:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, whdloadNode.ScrnshotFilename);
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile);
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot1:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, whdloadNode.ScrnshotFilename.Substring(0, whdloadNode.ScrnshotFilename.LastIndexOf('.')) + "_1.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_1.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot2:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, whdloadNode.ScrnshotFilename.Substring(0, whdloadNode.ScrnshotFilename.LastIndexOf('.')) + "_2.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_2.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.BoxArtFront:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(whdloadNode.ScrnshotFilename), whdloadNode.Name + "_Front.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), whdloadNode.Name + "_Front.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                        case ArtworkType.BoxArtBack:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(whdloadNode.ScrnshotFilename), whdloadNode.Name + "_Back.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), whdloadNode.Name + "_Back.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                    }

                    if (File.Exists(SourceFileA))
                        File.Copy(SourceFileA, DestFile, true);
                    else if (File.Exists(SourceFileB))
                        File.Copy(SourceFileB, DestFile, true);
                }
            }
        }

        private void ConvertArtworkSPS()
        {
            if (Global.GBDatabase.SPSArray.Count == 0)
                Global.GBDatabase.ReadSPSXml();

            if (!Directory.Exists(txtDestinationFolder.Text))
                Directory.CreateDirectory(txtDestinationFolder.Text);

            foreach (SPSNode spsNode in Global.GBDatabase.SPSArray)
            {
                if (!String.IsNullOrEmpty(spsNode.ScrnshotFilename) && !String.IsNullOrEmpty(spsNode.ScrnshotFilename))
                {
                    string RomFile = Path.GetFileName(spsNode.FileName);
                    string ScrnshotFile = Path.GetFileName(spsNode.ScrnshotFilename);
                    string SourceFileA = null;
                    string SourceFileB = null;
                    string DestFile = null;

                    switch (Settings.Tools.ArtworkType)
                    {
                        case ArtworkType.Titlescreen:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, spsNode.ScrnshotFilename);
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile);
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot1:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, spsNode.ScrnshotFilename.Substring(0, spsNode.ScrnshotFilename.LastIndexOf('.')) + "_1.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_1.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.Screenshot2:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, spsNode.ScrnshotFilename.Substring(0, spsNode.ScrnshotFilename.LastIndexOf('.')) + "_2.png");
                            SourceFileB = Path.Combine(txtSourceFolder.Text, ScrnshotFile.Substring(0, ScrnshotFile.LastIndexOf('.')) + "_2.png");
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "png"));
                            break;
                        case ArtworkType.BoxArtFront:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(spsNode.ScrnshotFilename), spsNode.Name + "_Front.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), spsNode.Name + "_Front.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                        case ArtworkType.BoxArtBack:
                            SourceFileA = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(spsNode.ScrnshotFilename), spsNode.Name + "_Back.jpg"));
                            SourceFileB = Path.Combine(txtSourceFolder.Text, Path.Combine(Path.GetDirectoryName(ScrnshotFile), spsNode.Name + "_Back.jpg"));
                            DestFile = Path.Combine(txtDestinationFolder.Text, Path.ChangeExtension(RomFile, "jpg"));
                            break;
                    }

                    if (File.Exists(SourceFileA))
                        File.Copy(SourceFileA, DestFile, true);
                    else if (File.Exists(SourceFileB))
                        File.Copy(SourceFileB, DestFile, true);
                }
            }
        }

        private void PopulateInputMaps()
        {
            try
            {
                this.lvwKeyMappings.Items.Clear();

                butXArcadeDefaults.Visible = false;
                butXArcadeKeys.Visible = false;
                lblMouseSpeed.Visible = false;
                trkMouseSpeed.Visible = false;

                switch (tabControlInputMap.SelectedIndex)
                {
                    case 0: // Mouse
                        for (int i = 0; i < Global.WinUAE.Mouse.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.Mouse[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.Mouse[i].Input).ToString(), ((Keys)Global.WinUAE.Mouse[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableMouseMap;

                        lblMouseSpeed.Visible = true;
                        trkMouseSpeed.Visible = true;
                        break;
                    case 1: // KeyboardA
                        for (int i = 0; i < Global.WinUAE.KeyboardA.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.KeyboardA[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.KeyboardA[i].Input).ToString(), ((Keys)Global.WinUAE.KeyboardA[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableKeyboardAMap;
                        break;
                    case 2: // KeyboardB
                        for (int i = 0; i < Global.WinUAE.KeyboardB.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.KeyboardB[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.KeyboardB[i].Input).ToString(), ((Keys)Global.WinUAE.KeyboardB[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableKeyboardBMap;
                        break;
                    case 3: // KeyboardC
                        for (int i = 0; i < Global.WinUAE.KeyboardC.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.KeyboardC[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.KeyboardC[i].Input).ToString(), ((Keys)Global.WinUAE.KeyboardC[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableKeyboardCMap;
                        break;
                    case 4: // XArcadeLeft
                        for (int i = 0; i < Global.WinUAE.XArcadeLeft.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.XArcadeLeft[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.XArcadeLeft[i].Input).ToString(), ((Keys)Global.WinUAE.XArcadeLeft[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableXArcadeLeftMap;

                        butXArcadeDefaults.Visible = true;
                        butXArcadeKeys.Visible = true;
                        break;
                    case 5: // XArcadeRight
                        for (int i = 0; i < Global.WinUAE.XArcadeRight.Length; i++)
                        {
                            this.lvwKeyMappings.Items.Add(Global.WinUAE.XArcadeRight[i].Name);
                            this.lvwKeyMappings.Items[this.lvwKeyMappings.Items.Count - 1].SubItems.AddRange(new string[] { ((Keys)Global.WinUAE.XArcadeRight[i].Input).ToString(), ((Keys)Global.WinUAE.XArcadeRight[i].NewInput).ToString() });
                        }
                        chkEnableInputMap.Checked = Settings.Input.EnableXArcadeRightMap;

                        butXArcadeDefaults.Visible = true;
                        butXArcadeKeys.Visible = true;
                        break;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("PopulateInputMaps", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void SetResolution()
        {
            try
            {
                switch (Settings.Display.FullScreen)
                {
                    case 0: // Windowed
                        cboScreen.Items.Clear();
                        cboScreen.Items.AddRange(Global.WinUAE.WindowedResolutions);
                        cboDepth.Items.Clear();
                        cboRefresh.Items.Clear();

                        cboScreen.SelectedIndex = (Settings.Display.Screen < cboScreen.Items.Count ? Settings.Display.Screen : -1);
                        break;
                    case 1: // Fullscreen
                        cboScreen.Items.Clear();
                        cboDepth.Items.Clear();
                        cboRefresh.Items.Clear();

                        //foreach (DisplayMode displayMode in Global.DisplayMode)
                        //    cboScreen.Items.Add(displayMode.ToString());

                        for (int i = 0; i < Global.Screen.Length; i++)
                            cboScreen.Items.Add(Global.Screen[i].ToString());

                        for (int i = 0; i < Global.Depth.Length; i++)
                            cboDepth.Items.Add(Global.Depth[i].ToString());

                        for (int i = 0; i < Global.Refresh.Length; i++)
                            cboRefresh.Items.Add(Global.WinUAE.RefreshRateToStr(Global.Refresh[i]));

                        cboScreen.SelectedIndex = (Settings.Display.Screen < cboScreen.Items.Count ? Settings.Display.Screen : -1);
                        cboDepth.SelectedIndex = (Settings.Display.Depth < cboDepth.Items.Count ? Settings.Display.Depth : -1);
                        cboRefresh.SelectedIndex = (Settings.Display.Refresh < cboRefresh.Items.Count ? Settings.Display.Refresh : -1);
                        break;
                    case 2: // Full-window
                        cboScreen.Items.Clear();
                        cboDepth.Items.Clear();
                        cboRefresh.Items.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("SetResolution", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void SetCLIHide()
        {
            try
            {
                if (rdoCLIHideDefault.Checked)
                {
                    Settings.WHDRun.CLIHide = CLIHideType.Default;
                }
                else if (rdoCLIHideBackground.Checked)
                {
                    Settings.WHDRun.CLIHide = CLIHideType.Background;
                }
                else if (rdoCLIHideAll.Checked)
                {
                    Settings.WHDRun.CLIHide = CLIHideType.All;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("SetCLIHide", "MainForm", ex.Message, ex.StackTrace);
            }
        }

        private void CreateWebLinks(Control parent)
        {
            try
            {
                Size linkSize = new Size((parent.Width - 32) / 2, (parent.Height - 32) / WebLinks.Length);

                parent.SuspendLayout();

                for (int i = 0; i < WebLinks.Length; i++)
                {
                    LinkLabel linkLabel = new LinkLabel();
                    Label labelDesc = new Label();

                    linkLabel.Location = new Point(16, 16 + linkSize.Height * i);
                    linkLabel.Size = linkSize;
                    linkLabel.AutoSize = false;
                    linkLabel.TextAlign = ContentAlignment.MiddleRight;
                    linkLabel.Text = WebLinks[i].Name;

                    labelDesc.Location = new Point(16 + linkSize.Width, 16 + linkSize.Height * i);
                    labelDesc.Size = linkSize;
                    labelDesc.Font = new Font(labelDesc.Font, FontStyle.Bold);
                    labelDesc.AutoSize = false;
                    labelDesc.TextAlign = ContentAlignment.MiddleLeft;
                    labelDesc.Text = WebLinks[i].Description;

                    linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(WebLink_LinkClicked);

                    parent.Controls.Add(linkLabel);
                    parent.Controls.Add(labelDesc);
                }

                parent.ResumeLayout();
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("CreateWebLinks", "MainForm", ex.Message, ex.StackTrace);
            }
        }
    }
}