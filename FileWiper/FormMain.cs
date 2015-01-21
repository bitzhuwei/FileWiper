using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
                        WipeFileContent(item);
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

        private void WipeFileContent(string filename)
        {
            using (var stream = new System.IO.StreamWriter(filename, false))
            {
                stream.Write("http://bitzhuwei.cnblogs.com");
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

        }

        private void btnUnregister_Click(object sender, EventArgs e)
        {

        }


    }
}
