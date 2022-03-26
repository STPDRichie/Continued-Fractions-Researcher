using System;

namespace ContinuedFractionsResearcher
{
    public static class RandomNumberGenerator
    {
        private static byte[] bytes = new byte[4];

        private static System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography
            .RandomNumberGenerator
            .Create();

        public static int GetNextInt32()
        {
            rnd.GetBytes(bytes);
            return Math.Abs(BitConverter.ToInt32(bytes, 0));
        }
    }
    
    
}