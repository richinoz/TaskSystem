using System;
using System.Data.SqlTypes;

namespace TaskSystem
{

    public static class DateTimeExtensions
    {
        public static DateTime SqlValidDateTime(this DateTime d)
        {
            if (d.Year < 1753)
                d = SqlDateTime.MinValue.Value;

            return new SqlDateTime(d).Value;
        }

    }

}