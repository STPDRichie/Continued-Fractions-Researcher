namespace ContinuedFractionsResearcher;

public class Researcher
{
    private decimal ChartAccuracy { get; }
    public Dictionary<decimal, int> Chart { get; }
    public Dictionary<decimal, double> ChangedChart { get; private set; }

    public Researcher(int chartAccuracy)
    {
        ChartAccuracy = 1 / (decimal)chartAccuracy;

        Chart = new Dictionary<decimal, int>();
        ChangedChart = new Dictionary<decimal, double>();
        for (var i = 0; i < 1 / ChartAccuracy; i++)
        {
            Chart[i * ChartAccuracy] = 0;
            ChangedChart[i * ChartAccuracy] = 0;
        }
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

    public void ChangeChartByFunc(Func<double, double, double> func, Func<double, double, double> operation)
    {
        ChangedChart = Chart.ToDictionary(
            entry => entry.Key, 
            entry => (double)entry.Value);
        
        for (var i = 0; i < ChangedChart.Count; i++)
        {
            var number = 1.0;
            if (i != 0 && Chart[ChartAccuracy * i] != 0 && ChartAccuracy * i != 0)
                number = func(Chart[ChartAccuracy * i], (double) (ChartAccuracy * i));
            
            ChangedChart[ChartAccuracy * i] = operation(Chart[ChartAccuracy * i], number);
        }
    }
}