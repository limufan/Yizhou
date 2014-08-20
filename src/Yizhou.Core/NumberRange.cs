using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    [Serializable]
    public class NumberRange
    {
        public NumberRange()
        {
        }

        public NumberRange(decimal? min, decimal? max)
        {
            this.min = min;
            this.max = max;
        }

        public decimal? max { set; get; }

        public decimal? min { set; get; }

        public bool InRange(decimal? value)
        {
            if (!value.HasValue)
            {
                return false;
            }

            if (this.max.HasValue && this.min.HasValue)
            {
                if (value > max || value < this.min)
                {
                    return false;
                }
            }
            else if (this.max.HasValue)
            {
                if (value > this.max)
                {
                    return false;
                }
            }
            else if (this.min.HasValue)
            {
                if (value < this.min)
                {
                    return false;
                }
            }
            return true;
        } 
    }
}
