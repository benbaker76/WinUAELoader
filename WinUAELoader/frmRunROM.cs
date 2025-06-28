// Copyright (c) 2008, Ben Baker
// All rights reserved.
// 
// This source code is licensed under the BSD-style license found in the
// LICENSE file in the root directory of this source tree. 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WinUAELoader
{
    public partial class frmRunROM : Form
    {
        public frmRunROM()
        {
            InitializeComponent();
        }

        private void frmRunGame_Load(object sender, EventArgs e)
        {
            PopulateGameList();
        }

        private void butRun_Click(object sender, EventArgs e)
        {
            Global.WinUAE.RunROM(lvwRunGame.SelectedItems[0].SubItems[1].Text);
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PopulateGameList()
        {
            int GameCount = 0;
            int GameTotal = 0;

            switch (Settings.General.LoaderMode)
            {
                case ROMType.GameBase:
                    if (Global.GBDatabase.GameBaseArray.Count == 0)
                        Global.GBDatabase.ReadGameBaseXml(Settings.File.GameBaseXml, Global.GBDatabase.GameBaseArray);

                    foreach(GameBaseNode gamebaseNode in Global.GBDatabase.GameBaseArray)
                    {
                        GameTotal++;

                        string fileName = null;

                        if (!File.Exists(Path.Combine(Settings.Folder.GameBaseROMs, fileName = gamebaseNode.FileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.GameBaseROMs, fileName = Path.GetFileName(gamebaseNode.FileName))))
                                continue;

                        this.lvwRunGame.Items.Add(gamebaseNode.Name);
                        this.lvwRunGame.Items[this.lvwRunGame.Items.Count - 1].SubItems.AddRange(new string[] { fileName });

                        GameCount++;
                    }

                    toolStripStatusLabel1.Text = String.Format("{0} of {1} Games Found.", GameCount, GameTotal);
                    break;
                case ROMType.WHDLoad:
                    if (Global.GBDatabase.WHDLoadArray.Count == 0)
                        Global.GBDatabase.ReadWHDLoadXml();

                    foreach (WHDLoadNode whdloadNode in Global.GBDatabase.WHDLoadArray)
                    {
                        GameTotal++;

                        string fileName = null;

                        if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, fileName = whdloadNode.FileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.WHDLoadROMs, fileName = Path.GetFileName(whdloadNode.FileName))))
                                continue;

                        this.lvwRunGame.Items.Add(whdloadNode.Name);
                        this.lvwRunGame.Items[this.lvwRunGame.Items.Count - 1].SubItems.AddRange(new string[] { fileName });

                        GameCount++;
                    }

                    toolStripStatusLabel1.Text = String.Format("{0} of {1} Games Found.", GameCount, GameTotal);
                    break;
                case ROMType.SPS:
                    if (Global.GBDatabase.SPSArray.Count == 0)
                        Global.GBDatabase.ReadSPSXml();

                    foreach (SPSNode spsNode in Global.GBDatabase.SPSArray)
                    {
                        GameTotal++;

                        string fileName = null;

                        if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, fileName = spsNode.FileName)))
                            if (!File.Exists(Path.Combine(Settings.Folder.SPSROMs, fileName = Path.GetFileName(spsNode.FileName))))
                                continue;

                        this.lvwRunGame.Items.Add(spsNode.Name);
                        this.lvwRunGame.Items[this.lvwRunGame.Items.Count - 1].SubItems.AddRange(new string[] { fileName });

                        GameCount++;
                    }

                    toolStripStatusLabel1.Text = String.Format("{0} of {1} Games Found.", GameCount, GameTotal);
                    break;
                case ROMType.DemoBase:
                    if (Global.GBDatabase.DemoBaseArray.Count == 0)
                        Global.GBDatabase.ReadGameBaseXml(Settings.File.DemoBaseXml, Global.GBDatabase.DemoBaseArray);

                    foreach (GameBaseNode gamebaseNode in Global.GBDatabase.DemoBaseArray)
                    {
                        if (gamebaseNode.FileName != String.Empty)
                        {
                            GameTotal++;

                            string fileName = null;

                            if (!File.Exists(Path.Combine(Settings.Folder.DemoBaseROMs, fileName = gamebaseNode.FileName)))
                                if (!File.Exists(Path.Combine(Settings.Folder.DemoBaseROMs, fileName = Path.GetFileName(gamebaseNode.FileName))))
                                    continue;

                            this.lvwRunGame.Items.Add(gamebaseNode.Name);
                            this.lvwRunGame.Items[this.lvwRunGame.Items.Count - 1].SubItems.AddRange(new string[] { fileName });

                            GameCount++;
                        }
                    }

                    toolStripStatusLabel1.Text = String.Format("{0} of {1} Demos Found.", GameCount, GameTotal);
                    break;
            }
        }

        private void lvwRunGame_DoubleClick(object sender, EventArgs e)
        {
            Global.WinUAE.RunROM(lvwRunGame.SelectedItems[0].SubItems[1].Text);
        }
    }
}