using System.Collections.Generic;

namespace ContinuedFractionsResearcher
{
    public class Researcher
    {
        private double chartAccuracy;
        private Dictionary<double, int> chart;

        public Researcher(double chartAccuracy)
        {
            this.chartAccuracy = chartAccuracy;

            chart = new Dictionary<double, int>();
            for (var i = 0; i < 1 / chartAccuracy; i++)
                chart[i * chartAccuracy] = 0;
        }
        
        public void GenerateContinuedFractions(
            int fractionsCount,
            (int Min, int Max) partialQuotientsCountRange,
            (int Min, int Max) partialQuotientsValueRange)
        {
            var (minCount, maxCount) = partialQuotientsCountRange;
            // var (minValue, maxValue) = partialQuotientsValueRange;
            
            var partialQuotients = new List<int>();
            for (var i = 0; i < fractionsCount; i++)
            {
                var partialQuotientsCount = (minCount == maxCount) ? minCount : 0; //rnd next or smth...
                
                partialQuotients.Clear();
                for (var j = 0; j < partialQuotientsCount; j++)
                {
                    var partialQuotient = 0; //rnd next or smth...
                    partialQuotients.Add(partialQuotient);
                }

                var fraction = new ContinuedFraction(partialQuotients);
                var nearest = fraction.Value % chartAccuracy;
                chart[chartAccuracy * nearest] += 1;
            }
        }
    }
}