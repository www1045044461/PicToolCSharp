using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace IncreaseName
{
    public class ICMPNameAdd : IComparer<FileInfo>
    {
        public int Compare(FileInfo x,FileInfo y)
        {
            Regex regex2 = new Regex("([a-zA-Z]?)([0-9]+)");

            string t1 = FormatMethods.GetId(x.Name);
            string t2 = FormatMethods.GetId(y.Name);

            var ret1 = regex2.Match(t1);
            var ret2 = regex2.Match(t2);

            //满足a23这种
            if(ret2.Success && ret2.Groups.Count>=3)
            {
                int v1 = Convert.ToInt32(ret1.Groups[2].Value);
                int v2 = Convert.ToInt32(ret2.Groups[2].Value);
                return v1.CompareTo(v2);
            }else if(ret2.Success && ret2.Groups.Count>=2 && ret2.Groups[1].Length == 0)
            {
                //纯数字
                int _t1 = Convert.ToInt32(t1);
                int _t2 = Convert.ToInt32(t2);
                return _t1.CompareTo(_t2);
            }else
            {
                //纯字母
                return t1.CompareTo(t2);
            }
        }
    }

    /// <summary>
    /// 仅限于字符串的类型2模式
    /// </summary>
    public class NameAddComparerMethod2 : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            Regex regex2 = new Regex("([a-zA-Z]?)([0-9]+)");

            string t1 = x;
            string t2 = y;

            var ret1 = regex2.Match(t1);
            var ret2 = regex2.Match(t2);

            //满足a23这种
            if(ret2.Success && ret2.Groups.Count>=3)
            {
                int v1 = Convert.ToInt32(ret1.Groups[2].Value);
                int v2 = Convert.ToInt32(ret2.Groups[2].Value);
                return v1.CompareTo(v2);
            }else if(ret2.Success && ret2.Groups.Count>=2 && ret2.Groups[1].Length == 0)
            {
                //纯数字
                int _t1 = Convert.ToInt32(t1);
                int _t2 = Convert.ToInt32(t2);
                return _t1.CompareTo(_t2);
            }else
            {
                //纯字母
                return t1.CompareTo(t2);
            }
        }
    }

     public class NameAddComparerMethod3 : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            Regex regex2 = new Regex("([a-zA-Z]?)([0-9]+)");

            string t1 = x;
            string t2 = y;

            var ret1 = regex2.Match(t1);
            var ret2 = regex2.Match(t2);

            //满足a23这种
            if(ret2.Success && ret2.Groups.Count>=3)
            {
                int v1 = Convert.ToInt32(ret1.Groups[2].Value);
                int v2 = Convert.ToInt32(ret2.Groups[2].Value);
                return v2.CompareTo(v1);
            }else if(ret2.Success && ret2.Groups.Count>=2 && ret2.Groups[1].Length == 0)
            {
                //纯数字
                int _t1 = Convert.ToInt32(t1);
                int _t2 = Convert.ToInt32(t2);
                return _t2.CompareTo(_t1);
            }else
            {
                //纯字母
                return t2.CompareTo(t1);
            }
        }
    }

    /// <summary>
    /// 扩展模式1
    /// </summary>
    public class NameAddComparerMethod1 : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            return y.CompareTo(x);
        }
    }

    /// <summary>
    /// 扩展模式0
    /// </summary>
    public class NameAddComparerMethod0 : IComparer<String>
    {
        public int Compare(string x, string y)
        {
            return x.CompareTo(y);
        }
    }
}
