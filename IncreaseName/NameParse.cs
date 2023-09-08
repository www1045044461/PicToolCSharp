using System;
using System.Text;

namespace IncreaseName.IncreaseName
{
    public class NameParse
    {
        /// <summary>
        /// 从字母和数字串中返回最后的数字
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static int GetLastNumberAfterZimu(string input)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = input.Length - 1; i >= 0; i--)
            {
                if ('0' <= input[i] && input[i] <= 9)
                {
                    builder.Append(input[i]);
                }
                else
                {
                    break;
                }
            }
            return Convert.ToInt32(builder.ToString());
        }
    }
}