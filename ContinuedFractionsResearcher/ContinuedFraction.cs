namespace ContinuedFractionsResearcher;

public class ContinuedFraction
{
    public double Value { get; }

    private int MinValue { get; }
    private int MaxValue { get; }

    private int PartialQuotientsCount { get; }

    public ContinuedFraction(int partialQuotientsCount, (int minValue, int maxValue) partialQuotientValueRange)
    {
        MinValue = partialQuotientValueRange.minValue;
        MaxValue = partialQuotientValueRange.maxValue;

        PartialQuotientsCount = partialQuotientsCount;
            
        Value = CountContinuedFraction(0);
    }

    private double CountContinuedFraction(int i)
    {
        var partialQuotient = RandomNumberGenerator.GetNextInt(MinValue, MaxValue);
            
        if (i == PartialQuotientsCount - 1)
            return 1 / (double)partialQuotient;

        return 1 / (partialQuotient + CountContinuedFraction(++i));
    }
}