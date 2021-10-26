using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Blockchain
{
    class Wallet
    {
        public RSA KeyPair { get; set; }
        public RSAParameters AllParameters
        {
            get { return this.KeyPair.ExportParameters(true); }
        }

        public RSAParameters PublicParameters
        {
            get { return this.KeyPair.ExportParameters(false); }
        }

        public Wallet()
        {
            this.KeyPair = RSA.Create();
        }

        public void SendMoney(int amount, string receiver, Blockchain blockchain)
        {
            Transaction transaction = new Transaction(this.KeyPair.ToXmlString(false), receiver, amount);

            transaction.Sign(this.AllParameters);
            blockchain.AddTransaction(transaction, transaction.signedHash, this);
        }
    }
}
