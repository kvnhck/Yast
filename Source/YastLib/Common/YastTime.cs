using System;

namespace YastLib.Common
{
    public static class YastTime
    {
        static readonly DateTime BaseTime = new DateTime(1970, 1, 1);

        public static double ToSecondsSince1970(DateTime time)
        {
            return (time - BaseTime).TotalSeconds;
        }

        public static DateTime FromSecondsSince1970(double seconds)
        {
            return new DateTime(1970, 1, 1).AddSeconds(seconds);
        }
    }
}
