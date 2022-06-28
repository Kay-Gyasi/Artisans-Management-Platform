using System.Collections.Generic;
using System.Collections.Immutable;

namespace AMP.Domain.Enums
{
    public static class CountryType
    {
        public static readonly ImmutableArray<KeyValuePair<int, string>> Countries =
            new ImmutableArray<KeyValuePair<int, string>>
        {
            new KeyValuePair<int,string>(1, "Ghana"),
            new KeyValuePair<int,string>(2, "Nigeria"),
        };

        public static readonly ImmutableArray<KeyValuePair<int, string>> Languages =
            new ImmutableArray<KeyValuePair<int, string>>
            {
                new KeyValuePair<int, string>(1, "Fante"),
                new KeyValuePair<int, string>(1, "Twi"),
            };
    }
}