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

    public class CreateDatePIncComparer : IComparer<PersonalFileInfo>
    {
        public int Compare(PersonalFileInfo x, PersonalFileInfo y)
        {
            return y.info.CreationTime.CompareTo(x.info.CreationTime);
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

    public class CreateDatePDecComparer : IComparer<PersonalFileInfo>
    {
        public int Compare(PersonalFileInfo x, PersonalFileInfo y)
        {
            return x.info.CreationTime.CompareTo(y.info.CreationTime);
        }
    }
}
