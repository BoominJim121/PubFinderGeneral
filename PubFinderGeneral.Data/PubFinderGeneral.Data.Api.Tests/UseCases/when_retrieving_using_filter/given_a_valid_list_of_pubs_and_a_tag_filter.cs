using FluentAssertions;
using PubFinderGeneral.Data.Api.Models.Response;
using PubFinderGeneral.Data.Store.Models;
using System.Net;
using Xunit;

using static PubFinderGeneral.Data.Api.Tests.TestHelper;

namespace PubFinderGeneral.Data.Api.Tests.UseCases.when_retrieving_using_filter
{
    public class given_a_valid_list_of_pubs_and_a_tag_filter : IClassFixture<given_a_valid_list_of_pubs_and_a_tag_filter.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public PubsResponse _response;

            protected override async Task Setup()
            {
                var pubs = new List<Pub>
                {
                    a_valid_pub_with_tags(new List<string> { "bar", "food", "garden"}),
                    a_valid_pub_with_tags(new List<string> { "open late", "food", "patio"}),
                    a_valid_pub_with_tags(new List<string> { "pub", "music", "garden"}),
                    a_valid_pub_with_tags(new List<string> { "club", "live music", "late opening"})
                };

                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs, 1, 10, "food"))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=1&pageSize=10&tags=food");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<PubsResponse>();
            }

        }
        public given_a_valid_list_of_pubs_and_a_tag_filter(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void returns_success_status_code()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Should().NotBeNull();
            _fixture._response.Pubs.Should().NotBeNull();
            _fixture._response.Pubs.Should().NotBeEmpty();
            _fixture._response.Pubs.FirstOrDefault().Should().NotBeNull();
            _fixture._response.Pubs.FirstOrDefault().Should().BeOfType<Pub>();

        }
        [Fact]
        public void then_response_has_10_pubs()
        {
            _fixture._response.Pubs.Count().Should().Be(2);
        }

    }
}
