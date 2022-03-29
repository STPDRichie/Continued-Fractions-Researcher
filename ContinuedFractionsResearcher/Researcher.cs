using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace ContinuedFractionsResearcher
{
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
            var partialQuotients = new List<int>();
            for (var i = 0; i < fractionsCount; i++)
            {
                var partialQuotientsCount =
                    (minCount == maxCount) ? minCount : 2; // RandomNumberGenerator.GetNextInt16(); // TODO Generate in range [minCount; maxCount]
                partialQuotients.Clear();
                for (var j = 0; j < partialQuotientsCount; j++)
                {
                    var partialQuotient = RandomNumberGenerator.GetNextInt16(); // TODO Generate in range [pQVR.Min; pQVR.Max]
                    partialQuotients.Add(partialQuotient);
                }

                var fraction = new ContinuedFraction(partialQuotients);
                var nearest = (int)(fraction.Value / (double)ChartAccuracy);
                Chart[ChartAccuracy * nearest] += 1;
            }
        }
    }
}
