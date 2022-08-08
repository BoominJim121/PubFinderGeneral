using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Api.Models.Response
{
    public class PubsResponse
    {
        public PubsResponse(ICollection<Pub> pubs, int totalPages)
        {
            Pubs = pubs;
            this.totalPages = totalPages;
        }

        public ICollection<Pub> Pubs { get; set; }
        public int totalPages { get; set; }
    }
}
