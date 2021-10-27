using System;
using System.Collections.Generic;


namespace Blockchain
{
    class Blockchain
    {
        public List<Block> Chain { get; set; }
        public List<Transaction> PendingTransactions { get; set; }
        
        private int MinerRewards { get; set; }
        private int BlockSize { get; set; }
        public Block LastBlock
        {
            get { return this.Chain[this.Chain.Count - 1]; }
        }
        
        public Blockchain()
        {
            this.BlockSize = 3;
            Block firstBlock = new Block(new byte[] { }, new List<Transaction>() { }, 0, 0);
            this.Chain = new List<Block> { firstBlock };
            this.PendingTransactions = new List<Transaction>();
        }

        public void AddTransaction(Transaction transaction, byte[] signedTransactionHash, Wallet sender)
        {

            // verify signature with publickey and transaction
            if (HelperFunctions.VerifySignedData(transaction.Byteify(), signedTransactionHash, sender.PublicParameters))
            {
                this.PendingTransactions.Add(transaction);
                Console.WriteLine("Successfully added a transaction.");
            }
            else
            {
                Console.WriteLine("Error: Could not verify a transaction.");
            }
        }

        public bool MinePendingTransactions(Wallet miner)
        {
            if (this.PendingTransactions.Count < 1)
            {
                Console.WriteLine("Cannot mine right now, because there are no pending transactions");
                return false;
            }
            else
            {
                for (int i = 0; i < this.PendingTransactions.Count; i += this.BlockSize)
                {
                    int end = i + this.BlockSize;
                    if (end >= this.PendingTransactions.Count) end = this.PendingTransactions.Count;

                    List<Transaction> transactions = this.PendingTransactions.GetRange(i, end);

                    Block newBlock = new Block(this.LastBlock.Hash, transactions, this.Chain.Count - 1, 0);

                    newBlock.Mine();
                    
                    this.Chain.Add(newBlock);
                    Console.WriteLine("New Block added.");
                }
                
                Console.WriteLine("Finished mining transactions. Paying the miner is not implemented yet.");
                // pay the miner
                
                return false;
            }
        }
        
        public bool IsValidChain()
        {

            return true;
        }

        public int GetBalance(Wallet wallet)
        {

            return 0;
        }
    }
}
