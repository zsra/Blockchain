using System;

namespace Blockchain.Data
{
    public class Wallet
    {
        public string Id { get; set; }
        public decimal Balance { get; set; }
        public DateTime Created { get; set; }

        public Wallet()
        {
            this.Id = Cryptography.ID.GenerateId();
            this.Balance = 0;
            this.Created = DateTime.Now;
        }
    }
}
