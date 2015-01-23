using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace FileWiper
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void btnWipeFiles_Click(object sender, EventArgs e)
        {
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var count = 0;
                foreach (var item in openFileDlg.FileNames)
                {
                    try
                    {
                        Helper.WipeFileContent(item);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show(string.Format("{0} file{1} content {2} successfully wiped!",
                    count, count > 1 ? "s'" : "'s", count > 1 ? "are" : "is"));
            }
        }

 

        private void btnDeleteFiles_Click(object sender, EventArgs e)
        {
            if (openFileDlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var count = 0;
                foreach (var item in openFileDlg.FileNames)
                {
                    try
                    {
                        DeleteFile(item);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                MessageBox.Show(string.Format("{0} file{1} {2} successfully deleted!",
                    count, count > 1 ? "s" : "", count > 1 ? "are" : "is"));
            }
        }

        private void DeleteFile(string filename)
        {
            var fileInfo = new System.IO.FileInfo(filename);
            var directory = fileInfo.DirectoryName;
            var name = 0;
            var prefix = "";
            var newFilename = System.IO.Path.Combine(directory, String.Format("{0}{1}", prefix, name));
            while (System.IO.File.Exists(newFilename))
            {
                name++;
                if (name == int.MaxValue)
                {
                    prefix += "x";
                    name = 0;
                }
                newFilename = System.IO.Path.Combine(directory, String.Format("{0}{1}", prefix, name));
            }

            System.IO.File.Move(filename, newFilename);
            System.IO.File.Delete(newFilename);
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            //给所有类型的文件添加自定义的右键菜单
            {
                var itemName = "Wipe Content";
                var associatedProgramFullPath = this.GetType().Assembly.Location;
                //创建项：shell 
                RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"*\shell", true);
                if (shellKey == null)
                {
                    shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
                }

                //创建项：右键显示的菜单名称
                RegistryKey rightCommondKey = shellKey.CreateSubKey(itemName);
                RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

                //创建默认值：关联的程序
                associatedProgramKey.SetValue(string.Empty, associatedProgramFullPath + " %1");

                //刷新到磁盘并释放资源
                associatedProgramKey.Close();
                rightCommondKey.Close();
                shellKey.Close();
            }

            ////给所有文件夹添加自定义的右键菜单
            //{
            //    //创建项：shell 
            //    RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(@"directory\shell", true);
            //    if (shellKey == null)
            //    {
            //        shellKey = Registry.ClassesRoot.CreateSubKey(@"*\shell");
            //    }

            //    //创建项：右键显示的菜单名称
            //    RegistryKey rightCommondKey = shellKey.CreateSubKey(itemName);
            //    RegistryKey associatedProgramKey = rightCommondKey.CreateSubKey("command");

            //    //创建默认值：关联的程序
            //    associatedProgramKey.SetValue("", associatedProgramFullPath);


            //    //刷新到磁盘并释放资源
            //    associatedProgramKey.Close();
            //    rightCommondKey.Close();
            //    shellKey.Close();
            //}
        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {

        }

        private void btnWipeFolder_Click(object sender, EventArgs e)
        {
            if (this.openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var count = 0;
                foreach (var item in System.IO.Directory.GetFiles(this.openFolder.SelectedPath))
                {
                    try
                    {
                        Helper.WipeFileContent(item);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                MessageBox.Show(string.Format("{0} file{1} content {2} successfully wiped!",
                    count, count > 1 ? "s'" : "'s", count > 1 ? "are" : "is"));
            }
        }

        private void btnDeleteFolder_Click(object sender, EventArgs e)
        {
            if(this.openFolder.ShowDialog()== System.Windows.Forms.DialogResult.OK)
            {
                var count = 0;
                foreach (var item in System.IO.Directory.GetFiles(this.openFolder.SelectedPath))
                {
                    try
                    {
                        DeleteFile(item);
                        count++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                DeleteFolder(this.openFolder.SelectedPath);

                MessageBox.Show(string.Format("{0} file{1}(and the folder) {2} successfully deleted!",
                    count, count > 1 ? "s" : "", count > 1 ? "are" : "is"));
            }
        }

        private void DeleteFolder(string foldername)
        {
            var folderInfo = new System.IO.DirectoryInfo(foldername);

            var parentDirectory = folderInfo.Parent.FullName;
            var name = 0;
            var prefix = "";
            var newFoldername = System.IO.Path.Combine(parentDirectory, String.Format("{0}{1}", prefix, name));
            while (System.IO.Directory.Exists(newFoldername))
            {
                name++;
                if (name == int.MaxValue)
                {
                    prefix += "x";
                    name = 0;
                }
                newFoldername = System.IO.Path.Combine(parentDirectory, String.Format("{0}{1}", prefix, name));
            }

            System.IO.Directory.Move(foldername, newFoldername);
            System.IO.Directory.Delete(newFoldername);
        }


    }
}
