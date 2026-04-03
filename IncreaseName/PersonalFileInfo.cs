using System.IO;
using System.Net.Sockets;
using System.Text;

namespace IncreaseName
{
    /// <summary>
    /// 根据FileInfo添加合适的前缀
    /// </summary>
    public class PersonalFileInfo 
    {
        /// <summary>
        /// 原始的文件信息
        /// </summary>
        public FileInfo info;
        /// <summary>
        /// 增补后的名称
        /// </summary>
        public string ComparedName;

        /// <summary>
        /// </summary>
        /// <param name="info">原始的文件信息</param>
        /// <param name="formats">需要指定的格式</param>
        public PersonalFileInfo(FileInfo info, int formats)
        {
            this.info = info;
            string pureFileName = Path.GetFileNameWithoutExtension(info.Name);
            string ext = Path.GetExtension(info.Name);
            var spxs = pureFileName.Split('_');
            int rmids = formats - spxs.Length;

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i <rmids; i++)
            {
                builder.Append("0_");
            }

            for (int i = 0; i < spxs.Length ; i++)
            {
                builder.Append(spxs[i]);
                if(i!=spxs.Length-1)
                {
                    builder.Append("_");
                }
            }

            builder.Append(ext);

            this.ComparedName = builder.ToString();
        }

        public PersonalFileInfo(FileInfo info)
        {
            this.info = info;
            ComparedName = info.Name;
        }

    }
}
