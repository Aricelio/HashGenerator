using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace HashGenerator.Extensions
{
    public static class StringExtension
    {
        /// <summary>String with all characters on alphabet</summary>
        internal static readonly string _alphabet = "!$*()-+:;<>=@[]{}_0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        
        /// <summary>Char array</summary>
        private static readonly ArrayPool<char> _arrayPool = ArrayPool<char>.Shared;

        /// <summary>The Custom Hash Function</summary>
        /// <param name="source">The source</param>
        /// <param name="salt">The sault</param>
        /// <returns>The hash</returns>
        public static string CustomHash(this string source, string salt = "")
        {
            byte[] array;
            using (SHA256Managed sHA256Managed = new SHA256Managed())
            {
                array = sHA256Managed.ComputeHash(Encoding.UTF8.GetBytes(salt + source));
            }

            char[] array2 = _arrayPool.Rent(16);
            int num = 1;
            int num2 = 0;
            while (num < array.Length)
            {
                byte num3 = array[num - 1];
                byte b = array[num];
                int num4 = num3 + b;
                array2[num2] = _alphabet[num4 % _alphabet.Length];
                num += 2;
                num2++;
            }

            string result = new string(array2[..16]);
            _arrayPool.Return(array2);
            return result;
        }
    }
}