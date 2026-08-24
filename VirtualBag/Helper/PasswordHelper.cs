using System.Security.Cryptography;
using System.Text;

namespace VirtualBag.Helper
{
    public class PasswordHelper
    {
        public static string ToMD5(string password)
        {
            MD5 md5 = MD5.Create();

            byte[] inputBytes =
                Encoding.UTF8.GetBytes(password);

            byte[] hashBytes =
                md5.ComputeHash(inputBytes);

            StringBuilder sb =
                new StringBuilder();

            foreach (byte b in hashBytes)
            {
                sb.Append(b.ToString("x2"));
            }

            return sb.ToString();
        }
    }
}