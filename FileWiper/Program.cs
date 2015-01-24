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
                    if (System.IO.File.Exists(item))
                    {
                        Helper.WipeFileContent(item);
                    }
                    else if (System.IO.Directory.Exists(item))
                    {
                        Helper.WipeDirectory(item, System.IO.SearchOption.AllDirectories);
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
