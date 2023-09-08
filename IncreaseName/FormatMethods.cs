using System;
using System.Linq;
using System.Text;

namespace IncreaseName
{
    public class FormatMethods
    {
        /// <summary>
        /// 格式化输出后的第一序列序号
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="cur"></param>
        /// <returns></returns>
        public static string FormatFirstOrder(int start, int end, int cur)
        {
            int minlen = $"{start}".Length;
            int maxlen = $"{end}".Length;
            string curstr = $"{cur}";
            int countapp = maxlen - curstr.Length;

            StringBuilder builder = new StringBuilder();
            builder.Append('0', countapp);
            builder.Append(cur);
            return builder.ToString();
        }
        public static string GetId(string name)
        {
            bool is_format1 = name.Contains<char>('_');

            int t1 = -1;
            int t2 = -1;
            string _name = null;
            string id = "";
            if(is_format1)
            {
                t1 = name.LastIndexOf('_');
                t2 = name.LastIndexOf('.');
                _name = name.Substring(t1 + 1, t2 - t1 - 1);
                id = _name;
            }
            else
            {
                id = name;
            }
            return id;
        }
    }
}