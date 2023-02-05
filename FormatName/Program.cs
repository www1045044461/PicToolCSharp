using System;
using System.IO;
using System.Text;

namespace FormatName
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();

            Console.WriteLine(path);

            string[] nativenames = Directory.GetFiles(path);
            string outputs;

            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");
            string format = args[0];

            int index = format.IndexOf('n');

            string temp1 = format.Substring(0, index);

            foreach (var item in nativenames)
            {
               FileInfo fileInfo = new FileInfo(item);
               var  blcks  = fileInfo.FullName.Split("\\");
               string temp2 = blcks[blcks.Length - 1];
               outputs = $"{temp1}{temp2}";
               Console.WriteLine($"从{fileInfo.FullName}===>{outputs}");
               fileInfo.MoveTo(outputs);
            }

        }
    }
}
