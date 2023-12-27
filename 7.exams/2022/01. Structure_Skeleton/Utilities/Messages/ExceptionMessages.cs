using System;

namespace ChristmasPastryShop.Utilities.Messages
{
    public static class ExceptionMessages
    {
        public const string NameNullOrWhitespace = "Name cannot be null or whitespace!";

        public const string CapacityLessThanOne = "Capacity has to be greater than 0!";

        public static IFormatProvider InvalidDelicacyType { get; internal set; }
    }
}
