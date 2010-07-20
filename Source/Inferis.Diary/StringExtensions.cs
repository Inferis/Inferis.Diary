using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Inferis.Diary {
    internal static class StringExtensions {
        public static string AnyOfOrDefault(this IEnumerable<string> sequence, IEnumerable<string> choices)
        {
            return sequence.AnyOfOrDefault(choices, false);
        }

        public static string AnyOfOrDefault(this IEnumerable<string> sequence, IEnumerable<string> choices, bool ignoreCase)
        {
            return sequence.FirstOrDefault(item => choices.Any(c => string.Compare(c, item, ignoreCase) == 0));
        }
    }
}
