using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;

namespace IncreaseName
{
    public class ICMPInc : IComparer<FileInfo>
    {
        public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            return y.CreationTime.CompareTo(x.CreationTime);
        }
    }

    public class ICMPDec : IComparer<FileInfo>
    {
        public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            return x.CreationTime.CompareTo(y.CreationTime);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            Console.WriteLine(path);

            int mode = Convert.ToInt32(args[0]);

            int startindex = Convert.ToInt32(args[1]);

            int rename = 0;

            try
            {
                rename = Convert.ToInt32(args[2]);
            }
            catch (Exception e)
            {
                rename = 0;
            }

            string[] nativenames = Directory.GetFiles(path);
            string[] outputs = new string[nativenames.Length];

            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");

            List<FileInfo> files= new List<FileInfo>();

            foreach (var item in nativenames)
            {
                string temp = Path.Combine(path, item);
                files.Add(new FileInfo(temp));
            }

            if (mode == 0)
            {
                files.Sort(new ICMPInc());
            }else
            {
                files.Sort(new ICMPDec());
            }

           for(int i=0;i<files.Count;i++)
           {
               int tih = startindex +i;
               var  blcks  = files[i].FullName.Split("\\");
               string temp1 = blcks[blcks.Length - 1];
                if (rename == 0)
                {
                    outputs[i] = $"{tih}_{temp1}";
                }else
                {
                    if (temp1.Contains(".jpg"))
                    {
                        outputs[i] = $"{tih}.jpg";
                    }else
                    {
                        outputs[i] = $"{tih}.png";
                    }
                }
               Console.WriteLine($"从{files[i].FullName}===>{outputs[i]}");
               files[i].CopyTo(outputs[i]);
               files[i].Delete();
           }
        }
    }
}
