namespace ContinuedFractionsResearcher;

public static class RandomNumberGenerator
{
    public static int GetNextInt(int min, int max) =>
        System.Security.Cryptography.RandomNumberGenerator.GetInt32(min, max);
}