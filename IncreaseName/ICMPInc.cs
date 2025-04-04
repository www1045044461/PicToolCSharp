using System.Collections.Generic;
using System.IO;

namespace IncreaseName
{
    /// <summary>
    /// 创建日期递增
    /// </summary>
    public class CreateDateIncComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x,FileInfo y)
        {
            return y.CreationTime.CompareTo(x.CreationTime);
        }
    }

    /// <summary>
    /// 按照创建日期递减
    /// </summary>
    public class CreateDateDecComparer : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return x.CreationTime.CompareTo(y.CreationTime);
        }
    }
}
