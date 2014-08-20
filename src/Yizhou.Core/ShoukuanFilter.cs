using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class ShoukuanFilter
    {
        public RegexList KeywordRegex;

        public DateRange XiadanRiqi { set; get; }

        public DateRange JiekuanRiqi { set; get; }

        public DateRange ShoukuanRiqi { set; get; }

        public List<Shoukuan> Filtrate(List<Shoukuan> list)
        {
            return list.Where(m => this.IsMatch(m)).ToList();
        }

        private bool IsMatch(Shoukuan shoukuan)
        {
            if (this.KeywordRegex != null && !this.KeywordRegex.IsMatch(shoukuan.Keywords))
            {
                return false;
            }
            if (this.XiadanRiqi != null && !this.XiadanRiqi.InRange(shoukuan.Dingdan.XiadanRiqi))
            {
                return false;
            }
            if (this.JiekuanRiqi != null && !this.JiekuanRiqi.InRange(shoukuan.Dingdan.JiekuanRiqi))
            {
                return false;
            }
            if (this.ShoukuanRiqi != null && !this.JiekuanRiqi.InRange(shoukuan.ShoukuanRiqi))
            {
                return false;
            }
            return true;
        }
    }
}
