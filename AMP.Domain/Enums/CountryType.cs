using System.Collections.Generic;

namespace AMP.Domain.Enums
{

    public enum Countries
    {
        Ghana = 1,
        Nigeria
    }

    //public enum Languages
    //{
    //    English = 1,
    //    Fante,
    //    Twi,
    //    Ewe
    //}
    public static class CountryType
    {
        //public static readonly ImmutableArray<KeyValuePair<int, string>> Countries =
        //    new ImmutableArray<KeyValuePair<int, string>>
        //{
        //    new KeyValuePair<int,string>(1, "Ghana"),
        //    new KeyValuePair<int,string>(2, "Nigeria"),
        //};

        public static readonly Dictionary<int, string> Countries = new Dictionary<int, string>
        {
            {1, "Ghana"},
            {2, "Nigeria"},
        };
        
        public static readonly Dictionary<int, string> Languages = new Dictionary<int, string>
        {
            {1, "Fante"},
            {2, "Twi"},
        };
    }
}