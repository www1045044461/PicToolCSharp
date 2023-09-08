using System.Collections.Generic;
using System.IO;

namespace IncreaseName
{
    public class ICMPInc : IComparer<FileInfo>
    {
        public int Compare(FileInfo x,FileInfo y)
        {
            return y.CreationTime.CompareTo(x.CreationTime);
        }
    }
}
