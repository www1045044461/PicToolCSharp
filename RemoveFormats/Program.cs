using System.Collections.Concurrent;

namespace RemoveFormats
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string cur_path = Directory.GetCurrentDirectory();
            var files = Directory.GetFiles(cur_path);
            string[] newfiles = files.ToArray();

            ConcurrentQueue<string> logout = new ConcurrentQueue<string>();

            Parallel.For(0, newfiles.Length, a =>
            {
                string filename = new FileInfo(files[a]).Name;
                var subs = filename.Split('.');

                if (subs.Length > 2)
                {
                    string newfilename = Path.Combine(cur_path, subs[0] + "." + subs.Last());
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
