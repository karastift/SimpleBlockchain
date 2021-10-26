using System.Security.Cryptography;


namespace Blockchain
{
    class Wallet
    {
        public RSA KeyPair { get; }
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
            blockchain.AddTransaction(transaction, transaction.SignedHash, this);
        }
    }
}
