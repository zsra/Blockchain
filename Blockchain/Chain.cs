using System.Collections.Generic;
using System.Linq;
using static Blockchain.IO.IO;

namespace Blockchain
{
    /// <summary>
    /// The <see cref="Chain"/> class contains <see cref="Blockchain"/> 
    /// , methods to valid the Chain (<see cref="IsChainValid"/>)
    /// and Operator methods.
    /// </summary>
    public static class Chain
    {
        private static List<Block> Blockchain = new List<Block>();

        /// <summary>
        /// Add new block to the <see cref="Blockchain"/>.
        /// </summary>
        /// <param name="block">New block</param>
        public static void Add(Block block)
        {
            block.DoMine();
            Blockchain.Add(block);
            Write(Blockchain);
        }

        /// <summary>
        /// GET :: a stored Blockhain.
        /// </summary>
        /// <returns>Return with a stored blockchain.</returns>
        public static List<Block> GetBlockchain()
        {
            if(IsChainValid()) return Read<Block>();
            return null;
        }

        /// <summary>
        /// Get a genesis block.
        /// </summary>
        /// <returns>Return with a genesis block aka. First block.</returns>
        public static Block GetGenesis() 
            => Blockchain.FirstOrDefault();

        /// <summary>
        /// Delete full chain.
        /// </summary>
        public static void DeleteChain()
        {
            Blockchain = new List<Block>();
            Write(Blockchain);
        }

        /// <summary>
        /// Compate the <see cref="Blockchain"/> with a stored chain.
        /// </summary>
        /// <returns>If the chain.log doesn't changed from outside return true</returns>
        private static bool Compare() => 
            !Blockchain.Except(Read<Block>()).ToList().Any()
                && !Read<Block>().Except(Blockchain).ToList().Any();

        /// <summary>
        /// Check the blockchain is valid.
        /// </summary>
        /// <returns>If the chain is valid return with true.</returns>
        public static bool IsChainValid()
        {
            //Check the stored chain is same with blockchain
            if (Compare()) return false;

            List<Block> chain = Read<Block>();
            //Compare registered hash and calculated hash
            bool isHashValid = chain.All(b => b.Hash
            .Equals(Cryptography.Hash.GenerateHash(
                b.Created,
                b.PreviousHash,
                b.Data,
                b.Nonce
                ))) ? true : false;
            if (!isHashValid) return false;

            for (int i = 1; i < chain.Count(); i++)
            {
                //Compare previous hash and registered previous hash
                if (!chain[i - 1].Hash.Equals(chain[i].PreviousHash))
                {
                    return false;
                }

                string target = new string(new char[chain[i].Difficult]).Replace('\0', '0');
                //This block hasn't been mined
                if (!chain[i].Hash.Substring(0, chain[i].Difficult).Equals(target))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
