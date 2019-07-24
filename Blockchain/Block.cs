using System;
using System.Linq;

namespace Blockchain
{
    public class Block
    {
        public string Hash { get; set; }
        public string PreviousHash { get; set; }
        public Data.Data Data { get; set; }
        public long Created { get; set; }
        public int Nonce { get; set; }
        public int Difficult { get; set; }

        /// <summary>
        /// Creator of new Block Operator.
        /// </summary>
        /// <param name="data">This data will be contained in the new block.</param>
        /// <param name="prevHash">Previous Hash</param>
        public Block(Data.Data data)
        {
            this.Data = data;
            this.PreviousHash = GetPreviousHash();
            this.Created = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this.Hash = Cryptography
                .Hash.GenerateHash(this.Created, this.PreviousHash, this.Data, this.Nonce);
            this.Nonce = 0;
            this.Difficult = Difficulty.DYNAMIC++;
            Difficulty.CheckAndReset();
        }

        /// <summary>
        /// Get a prevoius Hash.
        /// </summary>
        /// <returns>Return with a last block's hash.</returns>
        private string GetPreviousHash() 
            => Chain.GetBlockchain().Last().Hash;

        /// <summary>
        /// Hash validations by mining.
        /// </summary>
        public void DoMine()
        {
            string target = new string(new char[this.Difficult]).Replace('\0', '0');
            while (!this.Hash.Substring(0, this.Difficult).Equals(target))
            {
                this.Nonce++;
                this.Hash = Cryptography
                .Hash.GenerateHash(this.Created, this.PreviousHash, 
                this.Data, this.Nonce);
            }
            IO.IO.WriteMiningLogging(this.Hash, this.Difficult);
        }
    }
}
