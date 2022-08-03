using PubFinderGeneral.Data.Store.Exceptions;
using PubFinderGeneral.Data.Store.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubFinderGeneral.Data.Store
{
    public  class CsvPubDataStore: IPubDataStore
    {
        private string _fileName = default;
        private string _path = default;
        private bool _loaded = false;
        private IList<Pub> Pubs = new List<Pub>();
        public CsvPubDataStore(string path, string fileName)
        {
            _path = path;
            _fileName = fileName;
            Load();
        }

        private void Load()
        {
            if (!_loaded)
            {
                var lines  = File.ReadAllLines(Path.Combine(_path, _fileName))
                    .Skip(1);
                if(lines.Any())
                {
                    foreach(var line in lines)
                    {
                        var pub = FromCsv(line);
                        if(pub != null)
                            Pubs.Add(pub);
                    }
                }
                else
                {
                    throw new InvalidPubLoadException($"Using {nameof(CsvPubDataStore)} when loading, file {_path} does not contain valid pubs.");
                }
            }
        }

        public ICollection<Pub> GetAllPubData()
        {
            Load();
            return Pubs;
        }

        public ICollection<Pub> GetFilteredPubData(Filter filter)
        {
            Load();
            return filter.MatchFilter(Pubs);
        }

        private static Pub FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            if (values.Count() != 15) return null; // invalid pub don't add
            var pub = new Pub();
            pub.Name = values[0];
            pub.Category = Pub.GetCategoryFromDescription(values[1]);
            pub.LastUpdatedDateTime = Convert.ToDateTime(values[3]);
            pub.Excerpt = values[4];
            var about = new About
            {
                Website = new Uri(values[2]),
                Thumbnail = new Uri(values[5]),
                Locale = new Location
                {
                    Address = values[8],
                    Latitude = Convert.ToDouble(values[6]),
                    Longitude = Convert.ToDouble(values[7]),
                },
                PhoneNumber = values[9],
                Twitter = values[10],
                Ratings = new List<Rating>
                {
                    new Rating { Name = "Beer", Value = Convert.ToInt16(values[11]) },
                    new Rating { Name = "Atmosphere", Value = Convert.ToInt16(values[12]) },
                    new Rating { Name = "Amenities", Value = Convert.ToInt16(values[13]) },
                    new Rating { Name = "Overall", Value = Convert.ToInt16(values[14]) },
                }
            };
            about.SetTags(values[15]);
            pub.About = about;
            return pub;
        }
    }
}
