﻿using System;


namespace Blockchain
{
    class Program
    {
        static void Main(string[] args)
        {
            Blockchain blockchain = new Blockchain();
            Wallet wallet = new Wallet();

            //for (int i = 0; i < 12; i++)
            //{
            //  wallet.SendMoney(2, "you", blockchain);
            //}

            wallet.SendMoney(2, "you", blockchain);
            blockchain.MinePendingTransactions(wallet);

            Console.ReadKey();
        }
    }
}
