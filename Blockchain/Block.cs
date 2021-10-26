using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain
{
    class Block
    {
        public byte[] Hash { get; set; }
        public byte[] PrevHash { get;set; }
        public List<Transaction> Transactions { get; set; }
        public string Time { get; set; }
        public int Index { get; set; }
        public int Nonce { get; set; }

        public Block(byte[] prevHash, List<Transaction> transactions, int index, int nonce)
        {
            this.Time = DateTime.Now.ToString();
            this.PrevHash = prevHash;
            this.Transactions = transactions;
            this.Index = index;
            this.Nonce = nonce;
            this.CalculateHash();
        }
        public void CalculateHash()
        {
            this.Hash = HelperFunctions.GetSHA256Hash(this.Stringify());
        }

        public string Stringify()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(this.PrevHash);
            sb.Append(this.BuildTransactionsHash());
            sb.Append(this.Time);
            sb.Append(this.Index);
            sb.Append(this.Nonce);

            return sb.ToString();
        }

        private string BuildTransactionsHash()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < this.Transactions.Count; i++)
            {
                sb.Append(this.Transactions[i].hash);
            }

            return sb.ToString();
        }

        public void Mine()
        {
            Console.WriteLine("Mining a block...");

            Spinner spinner = new Spinner(Console.CursorLeft, Console.CursorTop, 100);

            string solution = "00";

            spinner.Start();
            bool mined = false;

            while (!mined)
            {
                if (HelperFunctions.ConvertToHexString(this.Hash).StartsWith(solution))
                {
                    spinner.Stop();
                    Console.WriteLine("Finished mined a block.");
                    mined = true;
                }
                else
                {
                    this.Nonce++;
                    this.CalculateHash();
                }
            }
        }
    }
}
