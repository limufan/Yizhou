using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class DingdanMingxiFilter
    {
        public Regex KeywordRegex;

        public List<DingdanMingxi> Filtrate(List<DingdanMingxi> list)
        {
            return list.Where(k => this.KeywordRegex.IsMatch(k.Keywords)).ToList();
        }
    }
}
