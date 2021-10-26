using System;
using System.Text;
using System.Security.Cryptography;


namespace Blockchain
{
    class Transaction
    {
        public byte[] Hash { get;set; }
        public byte[] SignedHash { get;set; }
        public string Sender { get; set; }
        public string Receiver { get; set; }
        public int Amount { get; set; }
        public string Time { get; set; }

        public Transaction(string sender, string receiver, int amount)
        {
            this.Sender = sender;
            this.Receiver = receiver;
            this.Amount = amount;
            this.Time = DateTime.Now.ToString();
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
            this.SignedHash = RSAalg.SignData(this.Byteify(), SHA256.Create());
        }

        public void CalculateHash()
        {
            this.Hash = HelperFunctions.GetSHA256Hash(this.Stringify());
        }

        public string Stringify()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.Sender);
            sb.Append(this.Receiver);
            sb.Append(this.Amount);
            sb.Append(this.Time);

            return sb.ToString();
        }
        public byte[] Byteify()
        {
            return Encoding.ASCII.GetBytes(this.Stringify());
        }
    }
}
