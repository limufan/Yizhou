using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Yizhou.Core
{
    public class RegexList
    {
        List<Regex> _regexList;

        public RegexList(List<Regex> regexList)
        {
            this._regexList = regexList;
        }

        public bool IsMatch(string input)
        {
            foreach (Regex regex in this._regexList)
            {
                if (!regex.IsMatch(input))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
