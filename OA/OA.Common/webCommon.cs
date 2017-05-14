using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OA.Common
{
    public class webCommon
    {
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string GetMd5String(string str)
        {
            MD5 md5 = MD5.Create();
            byte[] buffer = Encoding.UTF8.GetBytes(str);
            byte[] md5buffer = md5.ComputeHash(buffer);
            StringBuilder sb = new StringBuilder();

            foreach(byte b in md5buffer)
            {
                sb.Append(b.ToString("x2"));
            }
            //完成后释放资源
            md5.Clear();
            return sb.ToString();
        }
    }
}
