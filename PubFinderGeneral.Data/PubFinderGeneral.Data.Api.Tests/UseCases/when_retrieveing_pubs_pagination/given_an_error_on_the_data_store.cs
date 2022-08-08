using FluentAssertions;
using PubFinderGeneral.Data.Store.Models;
using System.Net;
using Xunit;

using static PubFinderGeneral.Data.Api.Tests.TestHelper;

namespace PubFinderGeneral.Data.Api.Tests.UseCases.when_retrieveing_pubs_pagination
{
    public class given_an_error_on_the_data_store : IClassFixture<given_an_error_on_the_data_store.Fixture>
    {
        private readonly Fixture _fixture;

        public class Fixture : BddAsyncTest
        {
            public HttpStatusCode _resultStatusCode;
            public string _requestId = Guid.NewGuid().ToString();

            protected override async Task Setup()
            {
                var result = await connect_to.pub_finder_general_data_api()
                    .with_a_data_store(a_mock_pub_data_store_that_throws_an_exception(1, 10))
                    .execute(client =>
                    {
                        return client.and_get(
                            $"/pubs?pageNumber=1&pageSize=10");
                    });

                _resultStatusCode = result.StatusCode;
            }
        }
        public given_an_error_on_the_data_store(Fixture fixture) => _fixture = fixture;

        [Fact]
        public void then_response_code_indicates_internal_server_error()
        {
            _fixture._resultStatusCode.Should().Be(HttpStatusCode.InternalServerError);
        }

    }
}
