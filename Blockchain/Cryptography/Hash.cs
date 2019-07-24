using System.Security.Cryptography;
using System.Text;

namespace Blockchain.Cryptography
{
    public static class Hash
    {
        /// <summary>
        /// Craete a string from data and call <see cref="ComputeSHA256(string)"/>
        /// function to create a Hash.
        /// </summary>
        /// <param name="created">Create time of  a block.</param>
        /// <param name="prevHash">Previous Hash.</param>
        /// <param name="data">Data</param>
        /// <param name="nonce">Nonce number</param>
        /// <returns></returns>
        public static string GenerateHash(long created, string prevHash, 
            Data.Data data, int nonce)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(data.Person.Name)
                .Append(data.Person.Address)
                .Append(data.Person.Birthdate)
                .Append(data.Wallet.Created)
                .Append(created)
                .Append(prevHash)
                .Append(nonce);
            return ComputeSHA256(builder.ToString());
        }

        /// <summary>
        /// Create a SHA from string.
        /// </summary>
        /// <param name="rawDataString">The string created by 
        /// <see cref="GenerateHash(long, string, Data.Data, int)"/></param>
        /// <returns>Return with a Hash.</returns>
        private static string ComputeSHA256(string rawDataString)
        {
            using (SHA256 sha = SHA256.Create())
            {
                byte[] hash = sha.ComputeHash(
                    Encoding.UTF8.GetBytes(rawDataString));
                StringBuilder builder = new StringBuilder();
                for(int i = 0; i < hash.Length; i++)
                {
                    string hex = hash[i].ToString("X");
                    if(hex.Length == 1)
                    {
                        builder.Append('0');
                    }
                    builder.Append(hex);
                }
                return builder.ToString();
            }
        }
    }
}
