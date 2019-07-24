using System;

namespace Blockchain.Cryptography
{
    public static class ID
    {
        /// <summary>
        /// Generetor of new Id.
        /// </summary>
        /// <returns>Return with an Id.</returns>
        public static string GenerateId()
            => Guid.NewGuid().ToString("N"); 
    }
}
