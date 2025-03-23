using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ContentProcessing.Lib
{
    public class TextToHash
    {
        public static long CreateHash(string input)
        {
            // Use SHA-256 to hash the input sentence
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));  // Compute the hash

                // Convert the first 8 bytes of the hash into a long (numeric) value
                return BitConverter.ToInt64(hashBytes, 0);
            }
        }
    }
}
