using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

namespace IncreaseName
{

    /// <summary>
    /// 操作
    /// </summary>
    enum Operation
    {
        /// <summary>
        /// 递增
        /// </summary>
        Increase,
        /// <summary>
        /// 递减
        /// </summary>
        Decline
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            if(args.Length == 1 && args[0] == "--help")
            {
                ShowHelp();
                return;
            }

            string path = Directory.GetCurrentDirectory();
            Console.WriteLine(path);
            string mode = args[0];
            int startindex = -1;

            string[] nativenames = Directory.GetFiles(path);
            string[] outputs = new string[nativenames.Length];
            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");

            List<PersonalFileInfo> files = new List<PersonalFileInfo>();


            if (mode == "0")
            {
                foreach (var item in nativenames)
                {
                    string temp = Path.Combine(path, item);
                    files.Add(new PersonalFileInfo(new FileInfo(temp)));
                }

                startindex = Convert.ToInt32(args[1]);
                files.Sort(new CreateDatePIncComparer());
            }
            else if (mode == "1")
            {
                foreach (var item in nativenames)
                {
                    string temp = Path.Combine(path, item);
                    files.Add(new PersonalFileInfo(new FileInfo(temp)));
                }

                files.Sort(new CreateDatePDecComparer());
                startindex = Convert.ToInt32(args[1]);
            }
            else if (mode == "2")
            {
                foreach (var item in nativenames)
                {
                    string temp = Path.Combine(path, item);
                    files.Add(new PersonalFileInfo(new FileInfo(temp)));
                }

                files.Sort(new ICMPNameAddP());
                startindex = Convert.ToInt32(args[1]);
            }
            else if (mode == "-E")
            {
                int levels = Convert.ToInt32(args[1]); //数量

                if (args.Length != levels + 3)
                {
                    Console.WriteLine($"错误等级数和参数对不上-->等级数:{levels} 参数数:{args.Length}");
                    return ;
                }

                foreach (var item in nativenames)
                {
                    string temp = Path.Combine(path, item);
                    files.Add(new PersonalFileInfo(new FileInfo(temp),levels));
                }

                //检查文件名模式异常
                foreach (var item in files)
                {
                    var sps = item.ComparedName.Split("_");
                    if(sps.Length != levels) 
                    {
                        Console.WriteLine($"错误文件{item.ComparedName}格式非法!");
                        return;
                    }
                }

                List<IComparer<String>> comparers = new List<IComparer<String>>();

                //根据参数初始化比较器列表
                for(int i=2;i<2+levels; i++)
                {
                    if (args[i] == "0")
                    {
                        comparers.Add(new NameAddComparerMethod0());
                    }
                    else if (args[i] == "1")
                    {
                        comparers.Add(new NameAddComparerMethod1());
                    }
                    else if (args[i] == "2")
                    {
                        comparers.Add(new NameAddComparerMethod2());
                    }
                    else if (args[i] == "3")
                    {
                        comparers.Add(new NameAddComparerMethod3());
                    }
                    else
                    {

                    }
                }

                IComparer<PersonalFileInfo> fcomparer = new MultiCompareP(comparers);

                //对文件进行排序
                files.Sort(fcomparer);

                //最后初始参数
                startindex = Convert.ToInt32(args[args.Length - 1]);
            }

            for (int i = 0; i < files.Count; i++)
            {
                int tih = startindex + i;
                var blcks = files[i].info.FullName.Split(new char[2] { '\\', '\\' });
                string temp1 = blcks[blcks.Length - 1];
                outputs[i] = $"{FormatMethods.FormatFirstOrder(0+startindex,files.Count+startindex,tih)}_{temp1}";
                Console.WriteLine($"从{files[i].info.FullName}===>{outputs[i]}");
                try
                {
                    files[i].info.CopyTo(outputs[i]);
                    files[i].info.Attributes = FileAttributes.Normal;
                    files[i].info.Delete();
                }
                catch (Exception e)
                {
                    Console.WriteLine($"异常{e.Message}==>{e.StackTrace}");
                }
            }
        }

        private static void ShowHelp()
        {
            Console.WriteLine($"将无序的文件首位排序: \n");
            Console.WriteLine($"基础模式:exe [mode] [Dec/Inc] [i1StartIndex]");
            Console.WriteLine($"exe 0 [i1SI] 按照创建日期递减排序");
            Console.WriteLine($"exe 1 [i1SI] 按照创建日期递增排序");
            Console.WriteLine($"exe 2 [i1SI] 按照最后_处的数字部分递增,字母部分忽略");
            Console.WriteLine($"exe 3 [i1SI] 按照最后_处的数字部分递见");
            Console.WriteLine($"exe -E [_分割数量n] [0文本整体自增/1文件整体自减/2按照2的模式自增/3] \n" +
                $"....[0/1/2]  [ilSI]");
        }

        private static void ShowHelp1()
        {
            Console.WriteLine($"将无序的文件首位排序: \n");
            Console.WriteLine($"基础模式:exe [mode] [Dec/Inc] [i1StartIndex]");
            Console.WriteLine($"exe -t 0递减/1递增 [i1SI] 按照创建日期递减排序");
            Console.WriteLine($"exe -n 0递增/1     [i1SI] -i  第几个下标序列 #按照最后_处的数字部分递增,字母部分忽略");
            Console.WriteLine($"exe -E [_分割数量n] [0文本整体自增/1文件整体自减/2按照2的模式自增] \n" +
                $"....[0/1/2]  [ilSI]");
        }
    }
}
