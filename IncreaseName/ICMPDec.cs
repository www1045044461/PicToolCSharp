using System.Collections.Generic;
using System.IO;

namespace IncreaseName
{
    public class ICMPDec : IComparer<FileInfo>
    {
        public int Compare(FileInfo x, FileInfo y)
        {
            return x.CreationTime.CompareTo(y.CreationTime);
        }
    }
}
