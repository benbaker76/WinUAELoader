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
using System.Drawing;

namespace WinUAELoader
{
    class FileIO
    {
        public static bool TryOpenFile(Form owner, string initialDirectory, string initialFileName, string extension, out string fileName)
        {
            fileName = null;

            try
            {
                OpenFileDialog fd = new OpenFileDialog();

                fd.Title = "Open File";
                fd.InitialDirectory = initialDirectory;
                fd.FileName = initialFileName;
                fd.Filter = String.Format("{0} Files (*{1})|*{2}|All Files (*.*)|*.*", extension.Substring(1, 1).ToUpper() + extension.Substring(2), extension, extension);
                fd.RestoreDirectory = true;
                fd.CheckFileExists = true;

                if (fd.ShowDialog(owner) == DialogResult.OK)
                {
                    fileName = fd.FileName;

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryOpenFile", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool TryOpenFolder(Form owner, string selectedPath, out string Folder)
        {
            Folder = null;

            try
            {
                FolderBrowserDialog fb = new FolderBrowserDialog();

                fb.SelectedPath = selectedPath;
                fb.ShowNewFolderButton = true;

                if (fb.ShowDialog(owner) == DialogResult.OK)
                {
                    Folder = fb.SelectedPath;

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TryOpenFolder", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static Bitmap LoadImage(string FileName)
        {
            Bitmap bmp = null;

            try
            {
                if (File.Exists(FileName))
                {
                    Bitmap bmpTemp = (Bitmap)Bitmap.FromFile(FileName);
                    bmp = new Bitmap(bmpTemp);
                    bmpTemp.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("LoadImage", "FileIO", ex.Message, ex.StackTrace);
            }

            return bmp;
        }

        public static bool TrySaveFile(Control parent, string initialDirectory, string initialFileName, string extension, out string fileName)
        {
            fileName = null;

            try
            {
                SaveFileDialog fd = new SaveFileDialog();

                fd.Title = "Save Layout";
                fd.InitialDirectory = initialDirectory;
                fd.FileName = initialFileName;
                fd.Filter = String.Format("{0} Files (*{1})|*{2}|All Files (*.*)|*.*", extension.Substring(1, 1).ToUpper() + extension.Substring(2), extension, extension);
                fd.OverwritePrompt = true;
                fd.RestoreDirectory = true;

                if (fd.ShowDialog(parent) == DialogResult.OK)
                {
                    fileName = fd.FileName;

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TrySaveFile", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static bool TrySaveImage(Control parent, string initialDirectory, string initialFileName, out string fileName)
        {
            fileName = null;

            try
            {
                SaveFileDialog fd = new SaveFileDialog();

                fd.Title = "Save Image";
                fd.InitialDirectory = initialDirectory;
                fd.FileName = initialFileName;
                fd.Filter = "Image Files (*.png)|*.png|All Files (*.*)|*.*";
                fd.OverwritePrompt = true;
                fd.RestoreDirectory = true;

                if (fd.ShowDialog(parent) == DialogResult.OK)
                {
                    fileName = fd.FileName;

                    return true;
                }
            }
            catch (Exception ex)
            {
                LogFile.WriteEntry("TrySaveImage", "FileIO", ex.Message, ex.StackTrace);
            }

            return false;
        }

        public static string GetRelativeFolder(string Folder, string Filename)
        {
            string NewFilename = Filename.Replace(Folder, "");

            if (NewFilename.StartsWith(Path.DirectorySeparatorChar.ToString()))
                NewFilename = NewFilename.Substring(1, NewFilename.Length - 1);

            return NewFilename;
        }
    }
}
