using System.Collections.Concurrent;

namespace Replace
{
    internal class Program
    {
        static void Main(string[] args)
        {
            char old_s = ':';
            char old_s1 = '：';
            char new_s = '_';
            string cur_path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(cur_path);
            string[] newfiles= files.ToArray();

            ConcurrentQueue<string> logout = new ConcurrentQueue<string>();

            Parallel.For(0, newfiles.Length, a =>
            {
                string filename = new FileInfo(files[a]).Name;
                if (filename.Contains(old_s) || filename.Contains(old_s1)) 
                {
                    filename = filename.Replace(old_s, new_s);
                    filename = filename.Replace(old_s1, new_s);
                    string newfilename = Path.Combine(cur_path, filename);
                    File.Move(files[a], newfilename);
                    logout.Enqueue($"{filename}-->${newfilename}"); 
                }
            });

            foreach (var file in logout)
            {
                Console.WriteLine(file);
            }
        }
    }
}
