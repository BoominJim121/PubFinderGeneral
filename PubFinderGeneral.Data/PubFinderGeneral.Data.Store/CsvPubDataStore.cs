using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Extensions.Logging;
using PubFinderGeneral.Data.Store.Exceptions;
using PubFinderGeneral.Data.Store.Models;
using System.Globalization;
using System.Text;

namespace PubFinderGeneral.Data.Store
{
    public class CsvPubDataStore : IPubDataStore
    {
        private string _fileName = default;
        private string _path = default;
        private bool _loaded = false;
        private IList<Pub> Pubs = new List<Pub>();

        private readonly ILogger<CsvPubDataStore> _logger;
        public CsvPubDataStore( string path, string fileName)
        {
            _path = path;
            _fileName = fileName;
        }

        private async Task Load()
        {
            if (!_loaded)
            {
                var fullPath = Path.Combine(_path, _fileName);
                IList<CsvPub> records = null;
                using (var reader = new StreamReader(fullPath))
                {
                    var configuration = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        Encoding = Encoding.Unicode, // Our file uses UTF-8 encoding.
                        Delimiter = ",", // The delimiter is a comma.
                        HeaderValidated = null,
                        MissingFieldFound = null,
                        BadDataFound = null,
                        
                    };
                    using (var csv = new CsvReader(reader, configuration))
                    {
                        csv.Context.RegisterClassMap<CsvPubMapper>();
                        records = csv.GetRecords<CsvPub>().ToList();

                    }
                }
                if (records != null)
                {

                    Pubs = await FromCsv(records);
                    _loaded = true;
                }
                else
                {
                    throw new InvalidPubLoadException($"Using {nameof(CsvPubDataStore)} when loading, file {_path} does not contain valid pubs.");
                }
            }
        }

        public async Task<(ICollection<Pub>, int)> GetAllPubData( int pageNumber, int pageSize)
        {
            await Load();
            var pubs = Pubs
                .OrderBy(pub => pub.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalPageCount = GetTotalPages(pageSize);
            return (pubs, totalPageCount);
        }

        private int GetTotalPages(int pageSize)
        {
            var totalPageCount = Math.DivRem(Pubs.Count(), pageSize, out var remain);
            if (remain > 0) totalPageCount++;
            return totalPageCount;
        }

        private static async Task<IList<Pub>> FromCsv(IList<CsvPub> pubs)
        {
            var myPubs = new List<Pub>();

            foreach (var x in pubs)
            {
                try
                {
                    myPubs.Add(new Pub
                    {
                        Name = x.name,
                        Category = x.category,
                        LastUpdatedDateTime = Convert.ToDateTime(x.date),
                        Excerpt = x.excerpt,
                        About = new About
                        {
                            Website = new Uri(x.url),
                            Thumbnail = new Uri(x.thumbnail),
                            Locale = new Location
                            {
                                Address = x.address,
                                Latitude = Convert.ToDecimal(x.lat),
                                Longitude = Convert.ToDecimal(x.lng)
                            },
                            PhoneNumber = x.phone,
                            Twitter = x.twitter,
                            Ratings = new List<Rating>
                            {
                                new Rating { Name = "Beer", Value = Convert.ToDecimal(x.stars_beer) },
                                new Rating { Name = "Atmosphere", Value = Convert.ToDecimal(x.stars_atmosphere) },
                                new Rating { Name = "Amenities", Value = Convert.ToDecimal(x.stars_amenities) },
                                new Rating { Name = "Overall", Value = Convert.ToDecimal(x.stars_value) },
                            },
                            Tags = x.tags == null ? new List<string>() : x.tags

                        }
                    }) ;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return myPubs;
        }

        public async Task<(ICollection<Pub>, int)> GetFilteredPubData(int pageNumber, int pageSize, string tag)
        {
            await Load();
            var pubs = Pubs
                .Where(x => x.About.Tags.Any(t => string.Compare(t, tag, StringComparison.InvariantCultureIgnoreCase) ==0))
                .OrderBy(pub => pub.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToList();
            int totalPageCount = GetTotalPages(pageSize);
            return (pubs, totalPageCount);
        }

        public class CsvPub
        {
            public string name { get; set; }

            public string category { get; set; }

            public string url { get; set; }

            public string date { get; set; }

            public string excerpt { get; set; }

            public string thumbnail { get; set; }

            public string lat { get; set; }

            public string lng { get; set; }

            public string address { get; set; }

            public string phone { get; set; }

            public string twitter { get; set; }

            public string stars_beer { get; set; }

            public string stars_atmosphere { get; set; }

            public string stars_amenities { get; set; }

            public string stars_value { get; set; }

            public IList<string> tags { get; set; }

        }
        public class CsvPubMapper: ClassMap<CsvPub>
        {
            public CsvPubMapper()
            {
                Map(x => x.name).Name("name");
                Map(x => x.category).Name("category");
                Map(x => x.url).Name("url");
                Map(x => x.date).Name("date");
                Map(x => x.excerpt).Name("excerpt");
                Map(x => x.thumbnail).Name("thumbnail");
                Map(x => x.lat).Name("lat");
                Map(x => x.lng).Name("lng");
                Map(x => x.address).Name("address");
                Map(x => x.phone).Name("phone");
                Map(x => x.twitter).Name("twitter");
                Map(x => x.stars_beer).Name("stars_beer");
                Map(x => x.stars_atmosphere).Name("stars_atmosphere");
                Map(x => x.stars_amenities).Name("stars_amenities");
                Map(x => x.stars_value).Name("stars_value");
                Map(x => x.tags).Name("tags").Convert(row =>
                {
                    var columnValue = row.Row.GetField<string>("tags");
                    return columnValue?.Split(',').ToList() ?? new List<string>();
                });
            }
        }
    }

}
