using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ContinuedFractionsResearcher
{
    public class Researcher
    {
        private double ChartAccuracy { get; }
        public Dictionary<double, int> Chart { get; }

        public Researcher(double chartAccuracy)
        {
            ChartAccuracy = chartAccuracy;

            Chart = new Dictionary<double, int>();
            for (var i = 0; i < 1 / chartAccuracy; i++)
                Chart[i * chartAccuracy] = 0;
        }

        public void GenerateContinuedFractions(
            int fractionsCount,
            (int Min, int Max) partialQuotientsCountRange,
            (int Min, int Max) partialQuotientsValueRange)
        {
            var (minCount, maxCount) = partialQuotientsCountRange;
            var partialQuotients = new List<int>();
            for (var i = 0; i < fractionsCount; i++)
            {
                var partialQuotientsCount =
                    (minCount == maxCount) ? minCount : RandomNumberGenerator.GetNextInt32(); // TODO Generate in range [minCount; maxCount]
                partialQuotients.Clear();
                for (var j = 0; j < partialQuotientsCount; j++)
                {
                    var partialQuotient = RandomNumberGenerator.GetNextInt32(); // TODO Generate in range [pQVR.Min; pQVR.Max]
                    partialQuotients.Add(partialQuotient);
                }

                var fraction = new ContinuedFraction(partialQuotients);
                var nearest = (int)(fraction.Value / ChartAccuracy);
                Chart[ChartAccuracy * nearest] += 1;
            }
        }
    }
}
