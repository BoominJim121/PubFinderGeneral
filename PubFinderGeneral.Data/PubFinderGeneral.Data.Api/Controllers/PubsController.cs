using Microsoft.AspNetCore.Mvc;
using PubFinderGeneral.Data.Api.Models;
using PubFinderGeneral.Data.Api.Models.Response;
using PubFinderGeneral.Data.Store;
using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PubsController : ControllerBase
    {
        private readonly IPubDataStore _datastore;
        public PubsController(IPubDataStore datastore)
        {
            _datastore = datastore;
        }


        [HttpGet]
        [ProducesResponseType(typeof(IList<Pub>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] PubsParameters pubParams)
        {
            try
            {
                (ICollection<Pub>?, int) pubsAndCount = await _datastore.GetFilteredPubData(pubParams.PageNumber, pubParams.PageSize, pubParams.tags);

                if (pubsAndCount.Item1 != null)
                {
                    var response = new PubsResponse(pubsAndCount.Item1, pubsAndCount.Item2);
                    return Ok(response);
                }
                return NoContent();
            }
            catch (Exception)
            {
                return Problem(
                    $"Error retrieving pubs",
                    statusCode: StatusCodes.Status500InternalServerError);
            }
        }
    }
}
