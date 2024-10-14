using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGrowth
{
    public class DoublingGrowthStrategy : IGrowthStrategy
    {
        public int GetNewLength(int currentArrayLength)
        {
            return 2 * currentArrayLength;
        }
    }
}
