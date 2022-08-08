using FluentAssertions;
using PubFinderGeneral.Data.Api.Models.Response;
using PubFinderGeneral.Data.Store.Models;
using System.Net;
using Xunit;

using static PubFinderGeneral.Data.Api.Tests.TestHelper;

namespace PubFinderGeneral.Data.Api.Tests.UseCases.when_retrieveing_pubs
{
    public class given_an_empty_list_of_pubs : IClassFixture<given_an_empty_list_of_pubs.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();
            public PubsResponse _response;

            protected override async Task Setup()
            {
                var pubs = new List<Pub>();
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store(pubs))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs");
                    });

                _resultStatusCode = result.StatusCode;
                _response = await result.deserialize_content_as_<PubsResponse>();
            }
        }
        public given_an_empty_list_of_pubs(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_success()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Fact]
        public void then_response_is_valid()
        {
            _fixture._response.Pubs.Should().NotBeNull();
            _fixture._response.Pubs.Should().BeEmpty();
        }

    }
}
