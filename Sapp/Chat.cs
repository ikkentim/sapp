using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Sapp
{
    static class Chat
    {
        public static string RemoveColorTags(string s)
        {
            return Regex.Replace(s, @"\{[0-9a-fA-F]{1,6}\}", "");
        }
    }
}
