using System;

namespace LogoAliTem.Domain.Helpers
{
    public static class TimeZoneHelper
    {
        private static readonly TimeZoneInfo BrasiliaTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");

        public static DateTime ConvertToBrasilia(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, BrasiliaTimeZone);
        }
    }
}