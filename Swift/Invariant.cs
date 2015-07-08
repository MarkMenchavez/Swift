namespace Swift
{
    using System;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;

    internal static class Invariant
    {
        [SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Reviewed.")]
        public static string Format(string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        ////public static TimeSpan ToTimeSpan(string value, string format)
        ////{
        ////    return ToTimeSpan(value, format, TimeSpan.MinValue);
        ////}

        ////public static TimeSpan ToTimeSpan(string value, string format, TimeSpan defaultValue)
        ////{
        ////    TimeSpan result;

        ////    return TimeSpan.TryParseExact(value, format, CultureInfo.InvariantCulture, TimeSpanStyles.None, out result)
        ////        ? result
        ////        : defaultValue;
        ////}

        ////public static DateTime ToDateTime(string value, string format)
        ////{
        ////    return ToDateTime(value, format, DateTime.MinValue);
        ////}

        public static DateTime ToDateTime(string value, string format, DateTime defaultValue)
        {
            DateTime result;

            return DateTime.TryParseExact(value, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result)
                ? result
                : defaultValue;
        }

        ////public static int ToInt32(string value)
        ////{
        ////    return ToInt32(value, 0);
        ////}

        ////public static int ToInt32(string value, int defaultValue)
        ////{
        ////    int data;
        ////    return int.TryParse(value, NumberStyles.None, CultureInfo.InvariantCulture, out data) ? data : defaultValue;
        ////}

        ////public static decimal ToDecimal(string value, NumberStyles styles, IFormatProvider formatInfo)
        ////{
        ////    return ToDecimal(value, styles, formatInfo, decimal.Zero);
        ////}

        ////public static decimal ToDecimal(string value, NumberStyles styles, IFormatProvider formatInfo, decimal defaultValue)
        ////{
        ////    decimal data;
        ////    return decimal.TryParse(value, styles, formatInfo, out data) ? data : defaultValue;
        ////}
    }
}
