using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGrowth
{
    public class IncrementalGrowthStrategy : IGrowthStrategy
    {
        private const int DEFAULT_GROWTH_RATE = 2;
        private readonly int _growth;

        public IncrementalGrowthStrategy() : this(DEFAULT_GROWTH_RATE)
        {

        }

        public IncrementalGrowthStrategy(int growth)
        {
            _growth = growth;
        }
        public int GetNewLength(int currentArrayLength)
        {
            return currentArrayLength + _growth;
        }
    }
}
