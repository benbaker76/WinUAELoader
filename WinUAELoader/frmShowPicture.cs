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

namespace WinUAELoader
{
    public partial class frmShowPicture : Form
    {
        public frmShowPicture(string Title, string imageFile)
        {
            InitializeComponent();

            this.Text = Title;
            this.pictureBox1.Image = Bitmap.FromFile(imageFile);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}