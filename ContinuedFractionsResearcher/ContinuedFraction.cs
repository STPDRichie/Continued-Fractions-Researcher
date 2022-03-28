using System.Collections.Generic;

namespace ContinuedFractionsResearcher
{
    public class ContinuedFraction
    {
        public double Value { get; }

        public ContinuedFraction(IReadOnlyList<int> partialQuotients)
        {
            Value = CountContinuedFraction(0, partialQuotients);
        }

        private static double CountContinuedFraction(int i, IReadOnlyList<int> partialQuotients)
        {
            if (i == partialQuotients.Count - 1)
                return 1 / (double)partialQuotients[i];

            return 1 / (partialQuotients[i] + CountContinuedFraction(++i, partialQuotients));
        }
    }
}
