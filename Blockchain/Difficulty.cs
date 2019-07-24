using System;
using System.Collections.Generic;
using System.Text;

namespace Blockchain
{
    public static class Difficulty
    {
        public static readonly int DEFAULT = 2;
        public static int DYNAMIC = 1;
        
        public static void CheckAndReset()
        {
            if(DYNAMIC == 33)
            {
                DYNAMIC = 1;
            }
        }

    }
}
