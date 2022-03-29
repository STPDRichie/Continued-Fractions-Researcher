using System;

namespace ContinuedFractionsResearcher
{
    public static class RandomNumberGenerator
    {
        private static byte[] bytes = new byte[4];

        private static System.Security.Cryptography.RandomNumberGenerator rnd = System.Security.Cryptography
            .RandomNumberGenerator
            .Create();
        // private static Random rnd = new Random();

        public static int GetNextInt16()
        {
            // return rnd.Next(1, 1000);
            rnd.GetBytes(bytes);
            return Math.Abs(BitConverter.ToInt16(bytes, 0));
        }
    }
}
