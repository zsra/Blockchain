using Blockchain.Functions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using static Blockchain.IO.IO;

namespace Blockchain.Test
{
    [TestClass]
    public class TransactionTest
    {
        [TestMethod]
        public void UploadTest()
        {
            List<Block> blocks = Chain.GetBlockchain();
            Transactions transaction = new Transactions();
            blocks[0].Data.Wallet.Balance = 30;
            Write<Block>(blocks);
            transaction.Upload(blocks[0].Data.Person, 10);
            blocks = Chain.GetBlockchain();
            Assert.AreEqual(40, blocks[0].Data.Wallet.Balance);

        }

        [TestMethod]
        public void WithdrawTest()
        {
            List<Block> blocks = Chain.GetBlockchain();
            Transactions transaction = new Transactions();
            blocks[0].Data.Wallet.Balance = 20;
            Write<Block>(blocks);
            transaction.Withdraw(blocks[0].Data.Person, 10);
            blocks = Chain.GetBlockchain();

            Assert.AreEqual(10, blocks[0].Data.Wallet.Balance);

        }

        [TestMethod]
        public void ValidTransferTest()
        {
            List<Block> blocks = Chain.GetBlockchain();
            Transactions transaction = new Transactions();
            blocks[1].Data.Wallet.Balance = 100;
            blocks[2].Data.Wallet.Balance = 0;
            Write<Block>(blocks);
            transaction.Transfer(blocks[1].Data.Person,
                blocks[2].Data.Person, 20);

            blocks = Chain.GetBlockchain();
            Assert.AreEqual(80 , blocks[1].Data.Wallet.Balance);
            Assert.AreEqual(20, blocks[2].Data.Wallet.Balance);
        }

        [TestMethod]
        public void InValidTransferTest()
        {
            List<Block> blocks = Chain.GetBlockchain();
            Transactions transaction = new Transactions();
            blocks[1].Data.Wallet.Balance = 0;
            blocks[2].Data.Wallet.Balance = 10;
            Write<Block>(blocks);
            transaction.Transfer(blocks[1].Data.Person,
                blocks[2].Data.Person, 20);

            blocks = Chain.GetBlockchain();
            Assert.AreNotEqual(-10, blocks[1].Data.Wallet.Balance);
            Assert.AreNotEqual(30, blocks[2].Data.Wallet.Balance);
        }
    }
}
