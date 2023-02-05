using System;
using System.IO;

namespace CycleMove
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string path = Directory.GetCurrentDirectory();
            int n = Convert.ToInt32(args[0]);   
            Console.WriteLine(path);

            string[] nativenames = Directory.GetFiles(path);
            string outputs;

            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");

            foreach (var item in nativenames)
            {
               FileInfo fileInfo = new FileInfo(item);
               var varss  = fileInfo.FullName.Split("\\");
               var blck = varss[varss.Length - 1];
               int index11 = blck.IndexOf('_');
               string num_str = blck.Substring(0, index11 - 1);
               int count = Convert.ToInt32(num_str);
               int count1 = count + n;
               outputs = $"{blck.Substring(index11+1)}";
               Console.WriteLine($"从{fileInfo.FullName}===>{outputs}");
               fileInfo.MoveTo(outputs);
            }
        }
    }
}
