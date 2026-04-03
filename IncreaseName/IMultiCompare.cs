using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncreaseName
{
    /// <summary>
    /// 复数比较器
    /// </summary>
    public class MultiCompare : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            string t1 = x.Name;
            string t2 = y.Name;

            var s1s = t1.Split('_');
            var s2s = t2.Split('_');

            if(s1s.Length != s2s.Length || s1s.Length != _comparers.Count)
            {
                throw new Exception($"比较等级和名字分割数量不一致:{s1s.Length}{s2s.Length}{_comparers.Count}");
            }

            int ret  = 0;

            for(int i=0;i<s1s.Length;i++)
            {
                ret = _comparers[i].Compare(s1s[i], s2s[i]);
                if(ret != 0)
                {
                    break;
                }else
                {
                    continue; //相等比教下一级
                }
            }

            return ret;
        }

        public MultiCompare(ICollection<IComparer<String>> comparers) 
        {
            _comparers = new List<IComparer<string>>();
            foreach (var comparer in comparers)
            {
                _comparers.Add(comparer);
            }
        }

        private List<IComparer<String>> _comparers = null;
    }

    public class MultiCompareP : IComparer<PersonalFileInfo>
    {
        public int Compare(PersonalFileInfo x, PersonalFileInfo y)
        {
            string t1 = x.ComparedName;
            string t2 = y.ComparedName;

            var s1s = t1.Split('_');
            var s2s = t2.Split('_');

            if (s1s.Length != s2s.Length || s1s.Length != _comparers.Count)
            {
                throw new Exception($"比较等级和名字分割数量不一致:{s1s.Length}{s2s.Length}{_comparers.Count}");
            }

            int ret = 0;

            for (int i = 0; i < s1s.Length; i++)
            {
                ret = _comparers[i].Compare(s1s[i], s2s[i]);
                if (ret != 0)
                {
                    break;
                }
                else
                {
                    continue; //相等比教下一级
                }
            }

            return ret;
        }

        public MultiCompareP(ICollection<IComparer<String>> comparers)
        {
            _comparers = new List<IComparer<string>>();
            foreach (var comparer in comparers)
            {
                _comparers.Add(comparer);
            }
        }

        private List<IComparer<String>> _comparers = null;
    }
}
