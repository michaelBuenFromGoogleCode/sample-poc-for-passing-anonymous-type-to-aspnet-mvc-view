using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TestEf
{
    public static class Helpers
    {
        public static IEnumerable<T> LimitAndOffset<T>(this IEnumerable<T> q,
                            int pageSize, int pageOffset)
        {
            return q.Skip((pageOffset - 1) * pageSize).Take(pageSize);
        }
    }
}