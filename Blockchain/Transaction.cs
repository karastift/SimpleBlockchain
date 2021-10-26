using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;


namespace Blockchain
{
    class Transaction
    {
        public byte[] hash
        {
            get;set;
        }
        public byte[] signedHash
        {
            get;set;
        }

        public string sender
        {
            get;set;
        }
        public string receiver
        {
            get;set;
        }
        public int amount
        {
            get;set;
        }
        public string time
        {
            get;set;
        }

        public Transaction(string sender, string receiver, int amount)
        {
            this.sender = sender;
            this.receiver = receiver;
            this.amount = amount;
            this.time = DateTime.Now.ToString();
            this.CalculateHash();
        }

        public void Sign(RSAParameters keyPair)
        {
            // Create a new instance of RSACryptoServiceProvider using the
            // key from RSAParameters.
            RSACryptoServiceProvider RSAalg = new RSACryptoServiceProvider();

            RSAalg.ImportParameters(keyPair);

            // Hash and sign the data. Pass a new instance of SHA256
            // to specify the hashing algorithm.
            this.signedHash = RSAalg.SignData(this.Byteify(), SHA256.Create());
        }

        public void CalculateHash()
        {
            this.hash = HelperFunctions.GetSHA256Hash(this.Stringify());
        }

        public string Stringify()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.sender);
            sb.Append(this.receiver);
            sb.Append(this.amount);
            sb.Append(this.time);

            return sb.ToString();
        }
        public byte[] Byteify()
        {
            return Encoding.ASCII.GetBytes(this.Stringify());
        }
    }
}
