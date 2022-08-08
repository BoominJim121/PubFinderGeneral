using Microsoft.AspNetCore.Mvc;
using PubFinderGeneral.Data.Api.Models;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] PubsParameters pubParams)
        {
            try
            {
                ICollection<Pub>? pubs = await _datastore.GetAllPubData(pubParams.PageNumber, pubParams.PageSize);

                if (pubs != null)
                {
                    return Ok(pubs);
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
