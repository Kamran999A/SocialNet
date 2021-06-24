using System;
using System.Security.Cryptography;
using System.Text;

namespace Hash
{
    class Hash
    {
        public static string GetHashSha256(string text)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(text);
            SHA256Managed hashstring = new SHA256Managed();
            byte[] hash = hashstring.ComputeHash(bytes);
            var sb = new StringBuilder();
            foreach (byte x in hash)
            {
                sb.Append(String.Format("{0:x2}", x));
            }
            return sb.ToString();
        }
    }
}
