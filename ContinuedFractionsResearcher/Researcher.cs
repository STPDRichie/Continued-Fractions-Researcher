namespace ContinuedFractionsResearcher;

public class Researcher
{
    private decimal ChartAccuracy { get; }
    public Dictionary<decimal, int> Chart { get; }

    public Researcher(int chartAccuracy)
    {
        ChartAccuracy = 1 / (decimal)chartAccuracy;

        Chart = new Dictionary<decimal, int>();
        for (var i = 0; i < 1 / ChartAccuracy; i++)
            Chart[i * ChartAccuracy] = 0;
    }

    public void GenerateContinuedFractions(
        int fractionsCount,
        (int Min, int Max) partialQuotientsCountRange,
        (int Min, int Max) partialQuotientsValueRange)
    {
        var (minCount, maxCount) = partialQuotientsCountRange;
        var isCountFixed = minCount == maxCount;
            
        for (var i = 0; i < fractionsCount; i++)
        {
            var partialQuotientsCount = isCountFixed ? minCount : RandomNumberGenerator.GetNextInt(minCount, maxCount);

            var fraction = new ContinuedFraction(partialQuotientsCount, partialQuotientsValueRange);
                
            var nearest = (int)(fraction.Value / (double)ChartAccuracy);
            Chart[ChartAccuracy * nearest] += 1;
        }
    }
}