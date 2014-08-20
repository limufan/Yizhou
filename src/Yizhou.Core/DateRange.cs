using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Yizhou.Core
{
    [Serializable]
    public class DateRange
    {
        public DateRange()
        {
        }

        public DateRange(DateTime? start, DateTime? end)
        {
            this.start = start;
            this.end = end;
        }

        public DateTime? start { set; get; }

        public DateTime? end { set; get; }

        public bool InRange(DateTime? date)
        {
            if (!date.HasValue)
            {
                return false;
            }

            if (this.end.HasValue && this.start.HasValue)
            {
                if (date.Value.Date < start.Value.Date || date.Value.Date > this.end.Value.Date)
                {
                    return false;
                }
            }
            else if (this.end.HasValue)
            {
                if (date.Value.Date > this.end.Value.Date)
                {
                    return false;
                }
            }
            else if (this.start.HasValue)
            {
                if (date.Value.Date < start.Value.Date)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
