using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Red4Assembler {
    public static class GlobalExtensions {

        public static bool IsIn<T>(this T value, params T[] compare) where T : IComparable {
            foreach(var x in compare) 
                if (value.Equals(x))
                    return true;

            return false;
        }

    }
}
