using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileWiper
{
    class Helper
    {
        public static void WipeFileContent(string filename)
        {
            try
            {
                using (var stream = new System.IO.StreamWriter(filename, false))
                {
                    stream.Write("http://bitzhuwei.cnblogs.com");
                }
            }
            catch (Exception)
            {
            }
        }

        internal static void WipeDirectory(string directoryName, System.IO.SearchOption wipeOption)
        {
            foreach (var item in System.IO.Directory.GetFiles(directoryName, "*", wipeOption))
            {
                WipeFileContent(item);
            }
        }
    }
}
