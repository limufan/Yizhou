using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class ChanpinFilter
    {
        public Regex KeywordRegex;

        public List<Chanpin> Filtrate(List<Chanpin> list)
        {
            return list.Where(k => this.KeywordRegex.IsMatch(k.Keywords)).ToList();
        }
    }
}
