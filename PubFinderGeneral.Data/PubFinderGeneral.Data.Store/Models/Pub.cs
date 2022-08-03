using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PubFinderGeneral.Data.Store.Models
{
    public class Pub
    {

        public string Name { get; set; }

        public Category Category { get; set; }

        public string Excerpt { get; set; }

        public DateTimeOffset LastUpdatedDateTime { get; set; }

        public About About { get; set; }

        public static Category GetCategoryFromDescription(string description)
        {
            foreach(Category val in Enum.GetValues(typeof(Category)))
            {
                var desc = StringValueOfEnum(val);
                if (desc.Equals(description, StringComparison.InvariantCultureIgnoreCase)) return val;
            }
            return Category.Uncategorized;
        }
        
        static string StringValueOfEnum(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (attributes.Length > 0)
            {
                return attributes[0].Description;
            }
            else
            {
                return value.ToString();
            }
        }
    }

    public class About
    {

        public Uri Website { get; set; }

        public Uri Thumbnail { get; set; }

        public Location Locale { get; set; }

        public string PhoneNumber { get; set; }

        public IList<Rating> Ratings { get; set; }

        public string Twitter { get; set; }

        public IList<string> Tags { get; set; }

        public void SetTags(string tags)
        {
            Tags = new List<string>();
            foreach (var tag in tags.Split(','))
            {
                Tags.Add(tag);
            }
        }

    }

    public class Rating
    {
        public string Name { get; set; }

        public short Value { get; set; }
    }

    public class Location
    {
        public string Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public enum Category
    {
        [Description("Bar Reviews")]
        BarReviews = 0,
        [Description("Closed Venues")]
        ClosedVenues = 1,
        [Description("Other Reviews")]
        OtherReviews = 2,
        [Description("Pub Reviews")]
        PubReviews = 3,
        [Description("Uncategorized")]
        Uncategorized = 4
    }
}
