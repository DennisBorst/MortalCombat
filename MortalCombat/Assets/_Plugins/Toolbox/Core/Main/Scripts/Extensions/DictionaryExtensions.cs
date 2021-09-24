using System.Collections.Generic;
using System.Linq;

namespace ToolBox
{
    public static class DictionaryExtensions
    {
        public static void SetAll<Tkey, TValue>(this Dictionary<Tkey, TValue> dict, TValue value)
        {
            var keys = dict.Keys.ToArray();
            foreach (var key in keys)
                dict[key] = value;
        }
    }
}
