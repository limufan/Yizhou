using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class DingdanFilter
    {
        public RegexList KeywordRegex { set; get; }

        public DateRange XiadanRiqi { set; get; }

        public DateRange FahuoRiqi { set; get; }

        public List<Dingdan> Filtrate(List<Dingdan> list)
        {
            return list.Where(d => this.IsMatch(d)).ToList();
        }

        private bool IsMatch(Dingdan dingdan)
        {
            if (this.KeywordRegex != null && !this.KeywordRegex.IsMatch(dingdan.Keywords))
            {
                return false;
            }
            if (this.XiadanRiqi != null && !this.XiadanRiqi.InRange(dingdan.XiadanRiqi))
            {
                return false;
            }
            if (this.FahuoRiqi != null && !this.FahuoRiqi.InRange(dingdan.XiadanRiqi))
            {
                return false;
            }
            return true;
        }
    }
}
