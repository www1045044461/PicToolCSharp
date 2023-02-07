using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace CycleMove
{
    public class ICMPNameAdd : IComparer<FileInfo>
    {
        public static int GetId(string name)
        {
            bool is_format1 = name.Contains('_');

            int t1 = -1;
            int t2 = -1;
            string _name = null;
            int id = -1;
            if (is_format1)
            {
                t1 = name.LastIndexOf('_');
                t2 = name.LastIndexOf('.');
                _name = name.Substring(t1 + 1, t2 - t1 - 1);
                id = Convert.ToInt32(_name);
            }
            else
            {
                id = Convert.ToInt32(name);
            }
            return id;

        }
        public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            int t1 = GetId(x.Name);
            int t2 = GetId(y.Name);
            return t1 >= t2 ? 1 : -1;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            //int incMode = Convert.ToInt32(args[0]);
            Console.WriteLine(path);

            string[] nativenames = Directory.GetFiles(path);
            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");
            //int mode = Convert.ToInt32(args[0]);
            string[] _tempnames = new string[nativenames.Length];

            for (int i = 0; i < nativenames.Length; i++)
            {
                int t1 = nativenames[i].LastIndexOf('\\');
                string name = nativenames[i].Substring(t1 + 1);
                int t2 = name.IndexOf('_');
                string name1 = name.Substring(t2 + 1);
                string ttt = Path.Combine(path,name1);
                _tempnames[i] = ttt;
                File.Copy(nativenames[i], ttt);
                File.Delete(nativenames[i]);
            }
        }
    }
}
