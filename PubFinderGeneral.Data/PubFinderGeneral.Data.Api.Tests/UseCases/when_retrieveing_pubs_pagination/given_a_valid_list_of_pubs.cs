using FluentAssertions;
using PubFinderGeneral.Data.Store.Models;
using System.Net;
using Xunit;

using static PubFinderGeneral.Data.Api.Tests.TestHelper;

namespace PubFinderGeneral.Data.Api.Tests.UseCases.when_retrieveing_pubs_pagination
{
    public class given_a_valid__list_of_pubs_when_requesting_page_of_10 : IClassFixture<given_a_valid__list_of_pubs_when_requesting_page_of_10.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public List<Pub> _response;

            protected override async Task Setup()
            {
                var pubs = a_list_of_many_pubs(20);
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs, 1, 10))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=1&pageSize=10");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<List<Pub>>();
            }
        }
        public given_a_valid__list_of_pubs_when_requesting_page_of_10(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_success()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Should().NotBeNull();
            _fixture._response.Should().NotBeEmpty();
            _fixture._response.FirstOrDefault().Should().NotBeNull();
            _fixture._response.FirstOrDefault().Should().BeOfType<Pub>();
            
        }
        [Fact]
        public void then_response_has_10_pubs()
        {
            _fixture._response.Count().Should().Be(10);
        }

    }

    public class given_a_valid__list_of_pubs_when_requesting_page_of_15 : IClassFixture<given_a_valid__list_of_pubs_when_requesting_page_of_15.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public List<Pub> _response;

            protected override async Task Setup()
            {
                var pubs = a_list_of_many_pubs(20);
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs, 1, 15))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=1&pageSize=15");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<List<Pub>>();
            }
        }
        public given_a_valid__list_of_pubs_when_requesting_page_of_15(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_success()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Should().NotBeNull();
            _fixture._response.Should().NotBeEmpty();
            _fixture._response.FirstOrDefault().Should().NotBeNull();
            _fixture._response.FirstOrDefault().Should().BeOfType<Pub>();
        }
        [Fact]
        public void then_response_has_15_pubs()
        {
            _fixture._response.Count().Should().Be(15);
        }

    }

    public class given_a_valid__list_of_pubs_when_requesting_page_of_5_second_page : IClassFixture<given_a_valid__list_of_pubs_when_requesting_page_of_5_second_page.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public List<Pub> _response;

            protected override async Task Setup()
            {
                var pubs = a_list_of_many_pubs(20);
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs, 2, 5))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=2&pageSize=5");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<List<Pub>>();
            }
        }
        public given_a_valid__list_of_pubs_when_requesting_page_of_5_second_page(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_success()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Should().NotBeNull();
            _fixture._response.Should().NotBeEmpty();
            _fixture._response.FirstOrDefault().Should().NotBeNull();
            _fixture._response.FirstOrDefault().Should().BeOfType<Pub>();
        }
        [Fact]
        public void then_response_has_5_pubs()
        {
            _fixture._response.Count().Should().Be(5);
        }

    }

    public class given_a_valid__list_of_20_pubs_when_requesting_page_of_15_second_page : IClassFixture<given_a_valid__list_of_20_pubs_when_requesting_page_of_15_second_page.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public List<Pub> _response;

            protected override async Task Setup()
            {
                var pubs = a_list_of_many_pubs(20);
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs, 2, 15))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=2&pageSize=15");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<List<Pub>>();
            }
        }
        public given_a_valid__list_of_20_pubs_when_requesting_page_of_15_second_page(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_success()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Should().NotBeNull();
            _fixture._response.Should().NotBeEmpty();
            _fixture._response.FirstOrDefault().Should().NotBeNull();
            _fixture._response.FirstOrDefault().Should().BeOfType<Pub>();
        }

        [Fact]
        public void then_response_has_5_pubs()
        {
            _fixture._response.Count().Should().Be(5);
        }

    }
}
