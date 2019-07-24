using Blockchain.Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Blockchain.Test
{
    [TestClass]
    public class NewBlockTest
    {
        [TestMethod]
        public void GenesisTest()
        {
            // Genesis Data
            Person person1 = new Person("First Block", "None", DateTime.Now);
            Wallet wallet1 = new Wallet();
            Data.Data data1 = new Data.Data(person1, wallet1);

            //First Data
            Person person2 = new Person("Second Block", "Debrecen", new DateTime(1997, 4, 2));
            Wallet wallet2 = new Wallet();
            Data.Data data2 = new Data.Data(person2, wallet2);

            //Genesis block
            Block genesisblock = new Block(data1);
            //First real block
            Block block1 = new Block(data2);

            Assert.AreEqual(block1.PreviousHash, genesisblock.Hash);

        }
    }
}
