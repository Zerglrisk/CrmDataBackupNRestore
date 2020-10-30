using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public static class Extension
    {
        public static bool RenameKey<TKey, TValue>(this IDictionary<TKey, TValue> dict, TKey from, TKey to)
        {
            TValue value;
            if (!dict.TryGetValue(from, out value)) //!dict.Remove(from, out value) : for core
            {
                return false;
            }

            dict.Remove(from);
            dict[to] = value;
            return true;
        }
    }
}
