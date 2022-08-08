using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace PubFinderGeneral.Data.Store.Models
{
    public class Pub
    {
        
        public string Name { get; set; }

        public string Category { get; set; }

        public string Excerpt { get; set; }

        public DateTimeOffset LastUpdatedDateTime { get; set; }

        public About About { get; set; }
    }

    public class About
    {
        public Uri Website { get; set; }

        public Uri Thumbnail { get; set; }

        public Location Locale { get; set; }

        [Phone]
        public string PhoneNumber { get; set; }

        public IList<Rating> Ratings { get; set; }

        public string Twitter { get; set; }

        public IList<string> Tags { get; set; }
    }

    public class Rating
    {
        public string Name { get; set; }

        public decimal Value { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }
    }
}
