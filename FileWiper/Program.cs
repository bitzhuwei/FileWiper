using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWiper
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(String[] args)
        {
            if (args != null && args.Length > 0)
            {
                Wipe(args);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new FormMain());
            }
        }

        private static void Wipe(String[] args)
        {
            foreach (var item in args)
            {
                try
                {
                    //MessageBox.Show(string.Format("Press OK to start wiping {0}.", item));
                    if (System.IO.File.Exists(item))
                    {
                        if (MessageBox.Show(string.Format("Press Yes to wipe {0}.", item), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            Helper.WipeFileContent(item);
                        }
                    }
                    else if (System.IO.Directory.Exists(item))
                    {
                        var files = System.IO.Directory.GetFiles(item, "*", System.IO.SearchOption.AllDirectories);
                        if (files.Length == 0)
                        {
                            MessageBox.Show(string.Format(
                                "No file exists under {0}", item), "Tip", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (MessageBox.Show(string.Format(
                            "Are you sure to wipe {0} files under {1}?", files.Length, item), "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            foreach (var file in files)
                            {
                                Helper.WipeFileContent(file);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString(), "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
        }
    }
}
