using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class DingdanMingxiFilter
    {
        public RegexList KeywordRegex;

        public DateRange XiadanRiqi { set; get; }

        public DateRange FahuoRiqi { set; get; }

        public List<DingdanMingxi> Filtrate(List<DingdanMingxi> list)
        {
            return list.Where(m => this.IsMatch(m)).ToList();
        }

        private bool IsMatch(DingdanMingxi dingdanMingxi)
        {
            if (this.KeywordRegex != null && !this.KeywordRegex.IsMatch(dingdanMingxi.Keywords))
            {
                return false;
            }
            if (this.XiadanRiqi != null && !this.XiadanRiqi.InRange(dingdanMingxi.Dingdan.XiadanRiqi))
            {
                return false;
            }
            if (this.FahuoRiqi != null && !this.FahuoRiqi.InRange(dingdanMingxi.Dingdan.XiadanRiqi))
            {
                return false;
            }
            return true;
        }
    }
}
