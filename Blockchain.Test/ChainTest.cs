using Blockchain.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Blockchain.Test
{
    [TestClass]
    public class ChainTest
    {
        [TestMethod]
        public void ChainingTest()
        {
            Person person0 = new Person("", "", DateTime.Now);
            Wallet wallet0 = new Wallet();
            Data.Data data0 = new Data.Data(person0, wallet0);

            //First Data
            Person person1 = new Person("Monroe Jamal", "Budapest", 
                new DateTime(1979, 1, 21));
            Wallet wallet1 = new Wallet();
            Data.Data data1 = new Data.Data(person1, wallet1);

            //Second Data
            Person person2 = new Person("Wood Dario", "Debrecen", 
                new DateTime(1997, 4, 2));
            Wallet wallet2 = new Wallet();
            Data.Data data2 = new Data.Data(person2, wallet2);

            //Third Data
            Person person3 = new Person("Weiss Ella", "Debrecen", 
                new DateTime(1993, 10, 10));
            Wallet wallet3 = new Wallet();
            Data.Data data3 = new Data.Data(person3, wallet3);

            //Fourth Data
            Person person4 = new Person("Patricia T. Smith", "Pécs",
                new DateTime(1999, 2, 7));
            Wallet wallet4 = new Wallet();
            Data.Data data4 = new Data.Data(person4, wallet4);

            Block genesis = new Block(data0);
            Chain.Add(genesis);

            Block block1 = new Block(data1);
            Chain.Add(block1);

            Block block2= new Block(data2);
            Chain.Add(block2);

            Block block3 = new Block(data3);
            Chain.Add(block3);

            Block block4 = new Block(data4);
            Chain.Add(block4);

            List<Block> chain = IO.IO.Read<Block>();

            Assert.AreEqual(block2.Hash, chain[2].Hash);
        }

        [TestMethod]
        public void ValidTest()
        {
            Assert.IsNotNull(Chain.GetGenesis());
            Assert.IsTrue(Chain.IsChainValid());
            //Chain.DeleteChain();
        }

    }
}
