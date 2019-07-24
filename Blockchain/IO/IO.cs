using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Blockchain.IO
{
    /// <summary>
    /// IO class Contains all |IO files| functions.
    /// </summary>
    public static class IO
    {
        /// <summary>
        /// Locations of Chain.json and logs files.
        /// </summary>
        public static string Location = "";

        /// <summary>
        /// List<> convert to serialized json string.
        /// </summary>
        /// <typeparam name="T">Block</typeparam>
        /// <param name="list">That list will be converted.</param>
        /// <returns>Serialized json string.</returns>
        private static string List2JsonString<T>(List<T> list)
            => JsonConvert.SerializeObject(list, Formatting.Indented);

        /// <summary>
        /// Serialized json string convert to List<>.
        /// </summary>
        /// <typeparam name="T">Block</typeparam>
        /// <param name="input">Serialized json string.</param>
        /// <returns></returns>
        private static List<T> Json2List<T>(string input)
            => JsonConvert.DeserializeObject<List<T>>(input);

        /// <summary>
        /// Blockchain list write to chain.json.
        /// </summary>
        /// <typeparam name="T">Block</typeparam>
        /// <param name="list">That list will be stored in the chain.json</param>
        public static void Write<T>(List<T> list)
        {
            var output = List2JsonString<T>(list);
            string PATH = @"" + Location + "chain.json";
            File.WriteAllText(PATH, output, Encoding.UTF8);
        }

        /// <summary>
        /// Read a chain.json into the list.
        /// </summary>
        /// <typeparam name="T">Block</typeparam>
        /// <returns>Return with List of blocks.</returns>
        public static List<T> Read<T>()
        {
            string PATH = @"" + Location + "chain.json";
            try
            {
                string text = File.ReadAllText(PATH);
                return Json2List<T>(text);
            } catch (IOException)
            {
                throw new IOException();
            }
        }

        /// <summary>
        /// Write a transfer into a transactions_log.txt.
        /// </summary>
        /// <param name="s_hash">Sender person's hash.</param>
        /// <param name="r_hash">Reciver person's hash.</param>
        /// <param name="amount">Amount</param>
        /// <param name="balance">Sender balance after a transfer.</param>
        public static void WriteTransactionsLogging(string s_hash, string r_hash, 
            decimal amount, decimal balance)
        {
            string line = "[" + DateTime.Now
                .ToString("MM/dd/yyyy h:mm:ss tt")
                + "] - sender: " + s_hash + " - reciever: " 
                + r_hash + " - amount: " +
                amount.ToString() + "\n";
            WriteLogging(line, "transactions_log.txt");
        }

        /// <summary>
        /// Upload and Withdraw function.
        /// </summary>
        /// <param name="hash">Uploader/Withdrawer person's hash.</param>
        /// <param name="amount">Amount</param>
        /// <param name="balance">Balance of person after the operation.</param>
        public static void WriteTransactionsLogging(string hash, decimal amount
            , decimal balance)
        {
            string line = null;
            if(amount < 0)
                line = "[" + DateTime.Now
                .ToString("MM/dd/yyyy h:mm:ss tt")
                + "] - Withdrawer: " + hash + " - amount: " +
                amount.ToString() + " balance: " + balance.ToString() + "\n";
            else
                line = "[" + DateTime.Now
                .ToString("MM/dd/yyyy h:mm:ss tt")
                + "] - Uploader: " + hash + " - amount: " +
                amount.ToString() + " balance: " + balance.ToString() + "\n";

            WriteLogging(line, "transactions_log.txt");
        }

        /// <summary>
        /// Write out the mining info into the mining_log.txt.
        /// </summary>
        /// <param name="hash">Block's hash.</param>
        /// <param name="difficulty">Level of Difficulty.</param>
        public static void WriteMiningLogging(string hash, int difficulty)
        {       
            string line = "[" + DateTime.Now
                .ToString("MM/dd/yyyy h:mm:ss tt") 
                + "] - Block is mined:\t" + hash + " - diff:\t" +
                difficulty.ToString() + "\n";
            WriteLogging(line, "mining_log.txt");
        }

        /// <summary>
        /// Logging writer to all logger functions.
        /// </summary>
        /// <param name="msg">Message that will be append into the Log files.</param>
        /// <param name="filename">Target file.</param>
        private static void WriteLogging(string msg, string filename)
        {
            string PATH = @"" + Location + filename;
            if (!File.Exists(PATH))
                File.WriteAllText(PATH, msg, Encoding.UTF8);
            else
                File.AppendAllText(PATH, msg, Encoding.UTF8);
        }
    }
}
