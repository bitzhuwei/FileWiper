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
            using (var stream = new System.IO.StreamWriter(filename, false))
            {
                stream.Write("http://bitzhuwei.cnblogs.com");
            }
        }
    }
}
