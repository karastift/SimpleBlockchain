using System;
using System.Security.Cryptography;
using System.Text;
using System.Reflection;
using Newtonsoft.Json;

namespace Blockchain
{
    class HelperFunctions
    {
        public static byte[] GetSHA256Hash(string input)
        {
            // create hashalgorithm
            SHA256 hashAlgorithm = SHA256.Create();

            // convert string to byte array and compute hash from bytes
            byte[] data = hashAlgorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            return data;
        }

        public static string ConvertToHexString(byte[] input)
        {
            // collect bytes and create a string using StringBuilder
            var sBuilder = new StringBuilder();

            // loop through bytes of hashed data
            // and format each byte as hexadecimal string
            for (int i = 0; i < input.Length; i++)
            {
                sBuilder.Append(input[i].ToString("x2"));
            }

            // return hexadecimal string
            return sBuilder.ToString();
        }

        public static bool VerifySHA256Hash(string input, string hash)
        {
            // hash input
            var hashOfInput = GetSHA256Hash(input);

            // compare hash strings using StringComparer
            StringComparer comparer = StringComparer.OrdinalIgnoreCase;

            return comparer.Compare(hashOfInput, hash) == 0;
        }

        public static bool VerifySignedData(byte[] dataToVerify, byte[] signedHash, RSAParameters public_key)
        {
            try
            {
                // Create a new instance of RSACryptoServiceProvider using the
                // key from RSAParameters.
                RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

                RSAalg.ImportParameters(public_key);

                // Verify the data using the signature.  Pass a new instance of SHA256
                // to specify the hashing algorithm.
                return RSAalg.VerifyData(dataToVerify, SHA256.Create(), signedHash);
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);

                return false;
            }
        }

        public static void PrintObject(object o)
        {
            try
            {
                Console.WriteLine(JsonConvert.SerializeObject(o));
            }
            catch (CryptographicException) 
            {
                Console.WriteLine($"CryptographicExeception when trying to print the object.");
            }
        }
    }
}
