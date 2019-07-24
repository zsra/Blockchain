using Blockchain.Data;
using static Blockchain.IO.IO;
using System.Linq;

namespace Blockchain.Functions
{
    /// <summary>
    /// All kind of transaction functions (Upload, Withdraw, Transfer).
    /// </summary>
    public class Transactions
    { 
        /// <summary>
        /// Upload a wallet.
        /// </summary>
        /// <param name="person">The person, who upload their wallet.</param>
        /// <param name="amount">Uploaded amount.</param>
        public void Upload(Person person, decimal amount)
        {
            var blocks = Chain.GetBlockchain();
            var data = blocks
                .Where(p => p.Data.Person.Id.Equals(person.Id))
                .Select(d => d.Data)
                .FirstOrDefault();
            if(data != null)
            {
                data.Wallet.Balance += amount;
                Write<Block>(blocks);
                WriteTransactionsLogging(data.Id, amount, data.Wallet.Balance);
            } else
            {
                return;
            }        
        }

        /// <summary>
        /// Withdraw money from wallet.
        /// </summary>
        /// <param name="person">Person, who withdraw is money.</param>
        /// <param name="amount">Withdrawed amount.</param>
        public void Withdraw(Person person, decimal amount)
        {
            var blocks = Chain.GetBlockchain();
            var data = blocks
                .Where(p => p.Data.Person.Id.Equals(person.Id))
                .Select(d => d.Data)
                .FirstOrDefault();
            if (data != null)
            {
                if(data.Wallet.Balance >= amount)
                {
                    data.Wallet.Balance -= amount;
                    Write<Block>(blocks);
                    WriteTransactionsLogging(data.Id, -amount, data.Wallet.Balance);
                } else
                {
                    return;
                }             
            }           
        }

        /// <summary>
        /// Transfer function to send money each other.
        /// </summary>
        /// <param name="sender">Sender person.</param>
        /// <param name="reciver">Reciver person.</param>
        /// <param name="amount">Amount.</param>
        public void Transfer(Person sender, Person reciver, 
            decimal amount)
        {
            var blocks = Chain.GetBlockchain();
            var s_data = blocks
                .Where(p => p.Data.Person.Id.Equals(sender.Id))
                .Select(d => d.Data)
                .FirstOrDefault();
            var r_data = blocks
                .Where(p => p.Data.Person.Id.Equals(reciver.Id))
                .Select(d => d.Data)
                .FirstOrDefault();
            if (s_data != null && r_data != null)
            {
                if (s_data.Wallet.Balance >= amount)
                {
                    s_data.Wallet.Balance -= amount;
                    r_data.Wallet.Balance += amount;
                    Write<Block>(blocks);
                    WriteTransactionsLogging(s_data.Person.Id, r_data.Person.Id
                        , amount, s_data.Wallet.Balance);
                }
                else
                {
                    return;
                }
            }
        }
    }
}
