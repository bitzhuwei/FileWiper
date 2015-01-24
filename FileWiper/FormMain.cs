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

        Random nameSeed = null;
        private void DeleteFile(string filename)
        {
            var fileInfo = new System.IO.FileInfo(filename);
            var directory = fileInfo.DirectoryName;
            var name = 0;
            var prefix = "";
            var newFilename = System.IO.Path.Combine(directory, String.Format("{0}{1}", prefix, name));
            while (System.IO.File.Exists(newFilename))
            {
                if (nameSeed == null) { nameSeed = new Random(); }
                name += nameSeed.Next();
                if (name > int.MaxValue / 10)
                {
                    prefix += "x";
                    name = 0;
                }
                newFilename = System.IO.Path.Combine(directory, String.Format("{0}{1}", prefix, name));
            }

            System.IO.File.Move(filename, newFilename);
            System.IO.File.Delete(newFilename);
        }

        class RegistryEntry
        {
            public string parentKey;
            public string name;
            public string associatedProgram;
        }
        private void btnRegister_Click(object sender, EventArgs e)
        {
            var entries = new RegistryEntry[]
            {
                //给所有类型的文件添加自定义的右键菜单
                new RegistryEntry(){ parentKey=@"*\shell", name="Wipe Content",
                 associatedProgram=this.GetType().Assembly.Location},
                //给所有文件夹添加自定义的右键菜单
                new RegistryEntry(){ parentKey=@"directory\shell", name="Wipe Directory",
                 associatedProgram=this.GetType().Assembly.Location}

            };
            try
            {
                foreach (var item in entries)
                {
                    //创建项：shell 
                    RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(item.parentKey, true);
                    if (shellKey == null)
                    {
                        shellKey = Registry.ClassesRoot.CreateSubKey(item.parentKey);
                    }

                    //创建项：右键显示的菜单名称
                    using (var rightCmd = shellKey.CreateSubKey(item.name))
                    {
                        using (var associatedProgram = rightCmd.CreateSubKey("command"))
                        {
                            //创建默认值：关联的程序
                            associatedProgram.SetValue(string.Empty,
                                item.associatedProgram + " %1");
                        }
                    }

                    //刷新到磁盘并释放资源
                    shellKey.Close();
                }
                MessageBox.Show("Registered successfully!");
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Please start this program with Administrator and try again.");
            }

        }
        private void btnUnregister_Click(object sender, EventArgs e)
        {
            var entries = new RegistryEntry[]
            {
                //给所有类型的文件添加自定义的右键菜单
                new RegistryEntry(){ parentKey=@"*\shell", name="Wipe Content"},
                //给所有文件夹添加自定义的右键菜单
                new RegistryEntry(){ parentKey=@"directory\shell", name="Wipe Directory"}

            };
            try
            {
                foreach (var item in entries)
                {
                    //创建项：shell 
                    RegistryKey shellKey = Registry.ClassesRoot.OpenSubKey(item.parentKey, true);
                    if (shellKey != null)
                    {
                        shellKey.DeleteSubKeyTree(item.name, false);
                    }

                    //刷新到磁盘并释放资源
                    shellKey.Close();
                }
                MessageBox.Show("Unregistered successfully!");
            }
            catch (System.Security.SecurityException se)
            {
                MessageBox.Show("Please start this program with Administrator and try again.");
            }
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
            if (this.openFolder.ShowDialog() == System.Windows.Forms.DialogResult.OK)
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
