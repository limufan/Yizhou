using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class DingdanFilter
    {
        public Regex KeywordRegex;

        public List<Dingdan> Filtrate(List<Dingdan> list)
        {
            return list.Where(k => this.KeywordRegex.IsMatch(k.Keywords)).ToList();
        }
    }
}
