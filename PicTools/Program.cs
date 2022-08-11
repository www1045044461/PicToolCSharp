using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PicTools
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            EnumerationOptions options = new EnumerationOptions();

            
            string path = Directory.GetCurrentDirectory();
            if(args.Length>=2)
            {
                path = args[1];
            }

            string[] natives_jpg = Directory.GetFiles(path, "*.jpg");
            string[] native_png = Directory.GetFiles(path, "*.png");
            string[] natives = new string[natives_jpg.Length + native_png.Length];

            natives_jpg.CopyTo(natives, 0);
            native_png.CopyTo(natives, natives_jpg.Length);

            Array.Sort(natives);


            Console.WriteLine($"Input Name Pattern:");
            string pattern = $"\\d*.(jpg|png)";
            string pattern2 = $"\\d*_*.(jpg|png)";

            Regex regex = new Regex(pattern);
            Regex regex2 = new Regex(pattern2);

            var t1 = regex.IsMatch(Path.GetFileName(natives[0]));
            var t2 = regex2.IsMatch(Path.GetFileName(natives[0]));

            int method = 0;
            if (Path.GetFileName(natives[0]).Contains('_') && t2==true)
            {
                method = 1;
            }else if(t1==true)
            {
                method = 2;
            }

            int offset = 0; 
            try
            {
                offset = Convert.ToInt32(args[0]);
            }
            catch (Exception)
            {
                Console.WriteLine($"参数二输入不合法:{args[0]}");
            }

            Func<string,int, string> AddIndex = (a,off1) => {
                string name = Path.GetFileName(a);
                string path = Path.GetDirectoryName(a);
                string[] tt1 = name.Split('_');
                int show_left = tt1[0].Length;
                int temp = Convert.ToInt32(tt1[0]);
                int temp1 = (temp + off1) % (natives.Length);

                string newname = a.Replace(tt1[0], $"{temp1.ToString().PadLeft(show_left,'0')}");

                return Path.Combine(path,newname);
            };

            Func<string, int, string> AddIndex2 = (a, off1) => {
                string name = Path.GetFileName(a);
                string path = Path.GetDirectoryName(a);
                string onlyname = name.Split('.')[0];
                int show_left = onlyname.Length;
                int temp = Convert.ToInt32(onlyname);
                int temp1 = temp + off1;
                //直接数字编号的不能直接取模运算需要全部解决后再处理
                string newname = a.Replace(onlyname, $"{temp1.ToString().PadLeft(show_left, '0')}");

                return Path.Combine(path, newname);
            };

            string[] news = new string[natives.Length];

            for(int i= 0; i < natives.Length; i++)
            {
                if (method == 1)
                    news[i] = AddIndex(natives[i], offset);
                else
                    news[i] = AddIndex2(natives[i], offset);
            }

            if (method == 1)
            {
                Parallel.For(0, news.Length, (a) =>
                {
                    File.Move(natives[a], news[a]);
                    Console.WriteLine($"{natives[a]}==>{news[a]}");
                });
            }
            else if (method == 2)
            {
                //
                if (offset>0)
                {
                    for (int i = natives.Length - 1; i >= 0; i--)
                    {
                        File.Move(natives[i], news[i]);    //先总体便宜然后补充到前面
                        Console.WriteLine($"{natives[i]}==>{news[i]}");
                    }
                }
                else
                {
                    //for (int i = 0; i < natives.Length; i++)
                    //{
                    //    File.Move(natives[i], news[i]);    //先总体便宜然后补充到前面
                    //    Console.WriteLine($"{natives[i]}==>{news[i]}");
                    //}
                }

                Console.WriteLine("===============================");
                //再将最后超出的offset设置到最前面
                for(int i=natives.Length-offset;i<natives.Length;i++)
                {
                   
                    int native_map_index = (i + offset) % natives.Length;
                    File.Move(news[i], natives[native_map_index]);
                    Console.WriteLine($"{news[i]}==>{natives[native_map_index]}");
                }
            }

            Console.WriteLine($"给{news.Length}个文件mod了{args[0]}");
        }
    }
}
