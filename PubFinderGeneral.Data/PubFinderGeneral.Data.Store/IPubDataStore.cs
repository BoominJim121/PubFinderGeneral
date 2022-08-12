using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Store
{
    public interface IPubDataStore
    {
        Task<(ICollection<Pub>, int)> GetFilteredPubData(int pageNumber, int pageSize, string tag);
    }
}