﻿using Moq;
using PubFinderGeneral.Data.Store;
using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Api.Tests
{
    public class TestHelper
    {
        public static IList<Pub> a_list_of_valid_pubs =>
            new List<Pub>
            {
                a_valid_pub()
            };

        public static IList<Pub> a_list_of_many_pubs(int numberofPubs)
        {
            var pubs = new List<Pub>();
            for(var i = 0; i< numberofPubs; i++)
            {
                pubs.Add(a_valid_pub());
            }
            return pubs;
        }
        public static Pub a_valid_pub()
        {
            //using this from file "...escobar","Closed venues","http://leedsbeer.info/?p=765","2012-11-30T21:58:52+00:00","...It's really dark in here!","http://leedsbeer.info/wp-content/uploads/2012/11/20121129_185815.jpg","53.8007317","","23-25 Great George Street, Leeds LS1 3BB","0113 220 4389","EscobarLeeds","2","3","3","3","food,live music,sofas"
            return new Pub
            {
                Name = "...escobar",
                Category = "Closed venues",
                LastUpdatedDateTime = DateTime.Parse("2012-11-30T21:58:52+00:00"),
                Excerpt = "...It's really dark in here!",
                About = new About
                {
                    Website = new Uri("http://leedsbeer.info/?p=765"),
                    Thumbnail = new Uri("http://leedsbeer.info/wp-content/uploads/2012/11/20121129_185815.jpg"),
                    Locale = new Location
                    {
                        Address = "23-25 Great George Street, Leeds LS1 3BB",
                        Latitude = Convert.ToDecimal(53.8007317),
                        Longitude = Convert.ToDecimal(-1.5481764)
                    },
                    PhoneNumber = "0113 220 4389",
                    Twitter = "EscobarLeeds",
                    Ratings = new List<Rating>
                            {
                                new Rating { Name = "Beer", Value = Convert.ToDecimal(2) },
                                new Rating { Name = "Atmosphere", Value = Convert.ToDecimal(3) },
                                new Rating { Name = "Amenities", Value = Convert.ToDecimal(3) },
                                new Rating { Name = "Overall", Value = Convert.ToDecimal(3) },
                            },
                    Tags = new List<string>
                    {
                        "food","live music","sofas"
                    }
                }
            };

        }

        public static IPubDataStore a_mock_pub_data_store(IList<Pub> pubs,  int pageNumber = 1, int pageSize = 25)
        {
            var moc = new Mock<IPubDataStore>();
            moc.Setup(s => s.GetAllPubData(pageNumber, pageSize))
                .ReturnsAsync(pubs.Skip((pageNumber -1)* pageSize).Take(pageSize).ToList());
            return moc.Object;
        }

        public static IPubDataStore a_mock_pub_data_store_that_throws_an_exception(int pageNumber = 1, int pageSize = 25)
        {
            var moc = new Mock<IPubDataStore>();
            moc.Setup(s => s.GetAllPubData(pageNumber, pageSize))
                .ThrowsAsync(new InvalidOperationException());
            return moc.Object;
        }
    }
}