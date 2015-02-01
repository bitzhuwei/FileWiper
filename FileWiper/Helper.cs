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
        public static bool WipeFileContent(string filename)
        {
            try
            {
                var fileInfo = new System.IO.FileInfo(filename);
                fileInfo.IsReadOnly = false;
                using (var stream = new System.IO.StreamWriter(filename, false))
                {
                    stream.Write("http://bitzhuwei.cnblogs.com");
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        internal static int WipeDirectory(string directoryName, System.IO.SearchOption wipeOption)
        {
            var count = 0;
            foreach (var item in System.IO.Directory.GetFiles(directoryName, "*", wipeOption))
            {
                if (WipeFileContent(item))
                {
                    count++;
                }
            }
            return count;
        }
    }
}
