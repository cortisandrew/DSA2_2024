using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArrayGrowth
{
    /// <summary>
    /// The growth strategy will tell the ArrayBasedVector what the next size of the array should be
    /// </summary>
    public interface IGrowthStrategy
    {
        int GetNewLength(int currentArrayLength);
    }
}
