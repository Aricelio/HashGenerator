using System.Buffers;
using System.Security.Cryptography;
using System.Text;

namespace HashGenerator.Extensions
{
    /// <summary>Strig Extensrion class</summary>
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
        public static string CustomHash(this string source, string? salt)
        {
            // Convert the input values into a byte data
            var byteString = Encoding.UTF8.GetBytes(salt + source);
            
            // Gets the SHA256 hash from the byte string
            byte[] byteArray = SHA256.HashData(byteString);
            
            // Gets a char array with at least 16 of length
            char[] charArray = _arrayPool.Rent(16);
            int num = 1;
            int num2 = 0;

            while (num < byteArray.Length)
            {
                byte num3 = byteArray[num - 1];
                byte b = byteArray[num];
                int num4 = num3 + b;
                charArray[num2] = _alphabet[num4 % _alphabet.Length];
                num += 2;
                num2++;
            }

            string result = new (charArray[..16]);
            _arrayPool.Return(charArray);
            return result;
        }
    }
}