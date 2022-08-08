using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Store
{
    public interface IPubDataStore
    {
        Task<(ICollection<Pub>, int)> GetAllPubData(int pageNumber, int pageSize);
    }
}