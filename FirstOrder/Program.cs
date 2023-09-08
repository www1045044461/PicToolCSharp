using System.Diagnostics.CodeAnalysis;

namespace FirstOrder
{
    public class ICMPInc : IComparer<FileInfo>
    {
        public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            int x_v = Program.GetFirstOrderIndex((FileInfo)x);
            int y_v = Program.GetFirstOrderIndex((FileInfo)y);
            return y_v.CompareTo(x_v);
        }
    }

    public class ICMPDec : IComparer<FileInfo>
    {
        public int Compare([AllowNull] FileInfo x, [AllowNull] FileInfo y)
        {
            int x_v = Program.GetFirstOrderIndex((FileInfo)x);
            int y_v = Program.GetFirstOrderIndex((FileInfo)y);
            return x_v.CompareTo(y_v);
        }
    }

    internal class Program
    {
        public static int GetFirstOrderIndex(FileInfo x)
        {
            string str = x.Name;
            int index1 = x.Name.IndexOf('_');
            int value = Convert.ToInt32(str.Substring(0, index1));
            return value;
        }

        public static string CreateFileName(string file,int value)
        {
            int v_ = file.IndexOf('_');
            string sub1 = file.Substring(0, v_);
            string sub2 = file.Substring(v_ , file.Length - v_);
            int vv = Convert.ToInt32(sub1);
            int vv1 = vv + value;
            string newName = $"{vv1}{sub2}";
            return newName;
        }

        public static string CombineName(FileInfo info, int value)
        { 
            string Name = CreateFileName(info.Name, value);
            string fullName = Path.Combine(info.DirectoryName, Name);
            return fullName;
        }

        public static void reverse()
        {
            string path = Directory.GetCurrentDirectory();
            string[] nativenames = Directory.GetFiles(path);
            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");
            List<FileInfo> files = new List<FileInfo>();
            foreach (var item in nativenames)
            {
                string temp = Path.Combine(path, item);
                files.Add(new FileInfo(temp));
            }
            files.Sort(new ICMPDec());

            List<FileInfo> files2 = new List<FileInfo>();
            for (int i = 0; i < files.Count; i++)
            {
                files2.Add(files[i]);
            }
            files2.Sort(new ICMPInc());
            string tempname = "";

            for(int i=0;i<files.Count/2;i++)
            {
                File.Replace(files[i].FullName, files2[i].FullName,null);
            }
        }

        static void Main(string[] args)
        {
            int mode = Convert.ToInt32(args[0]);
            int changevalue = Convert.ToInt32(args[1]);
            if (args.Length == 3)
            {
                string rev_str = args[2];
                if(rev_str.Equals("reverse"))
                {
                    reverse();
                }
            }
            string path = Directory.GetCurrentDirectory();
            string[] nativenames = Directory.GetFiles(path);
            Console.WriteLine($"总共扫描到文件数:{nativenames.Length}");
            List<FileInfo> files = new List<FileInfo>();
            foreach (var item in nativenames)
            {
                string temp = Path.Combine(path, item);
                files.Add(new FileInfo(temp));
            }

            if (mode == 0)
            {
                files.Sort(new ICMPDec());
            }
            else
            {
                files.Sort(new ICMPInc());
            }

            List<string> newpaths = new List<string>();
            for(int i=0;i<files.Count;i++)
            {
                newpaths.Add(CombineName(files[i], changevalue));
            }

            if (mode == 0 && changevalue > 0)
            {
                for (int i = newpaths.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{files[i].FullName}==>{newpaths[i]}");
                    files[i].MoveTo(newpaths[i]);
                   
                }
            }
            else if (mode == 0 && changevalue < 0)
            {
                for (int i = 0; i < newpaths.Count; i++)
                {
                    Console.WriteLine($"{files[i].FullName}==>{newpaths[i]}");
                    files[i].MoveTo(newpaths[i]);
                    
                }
            }else if(mode == 1 && changevalue >0)
            {
                for (int i = 0; i < newpaths.Count; i++)
                {
                    Console.WriteLine($"{files[i].FullName}==>{newpaths[i]}");
                    files[i].MoveTo(newpaths[i]);
                    
                }
            }else
            {
                for (int i = newpaths.Count - 1; i >= 0; i--)
                {
                    Console.WriteLine($"{files[i].FullName}==>{newpaths[i]}");
                    files[i].MoveTo(newpaths[i]);
                   
                }
            }
        }
    }
}