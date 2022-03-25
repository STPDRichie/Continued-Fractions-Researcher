using System.Collections.Generic;

namespace ContinuedFractionsResearcher
{
    public class ContinuedFraction
    {
        public double Value { get; }
        
        private readonly List<int> partialQuotients;
        public IReadOnlyList<int> PartialQuotients => partialQuotients;

        public ContinuedFraction(List<int> partialQuotients)
        {
            this.partialQuotients = partialQuotients;
            
            CountContinuedFraction();
        }

        private void CountContinuedFraction()
        {
            
        }
    }
}