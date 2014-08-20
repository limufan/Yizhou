using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class KehuFilter
    {
        public RegexList KeywordRegex;

        public List<Kehu> Filtrate(List<Kehu> list)
        {
            return list.Where(k => this.KeywordRegex.IsMatch(k.Keywords)).ToList();
        }
    }
}
