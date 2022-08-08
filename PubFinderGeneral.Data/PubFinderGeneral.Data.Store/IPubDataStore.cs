using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Store
{
    public interface IPubDataStore
    {
        Task<ICollection<Pub>> GetAllPubData(int pageNumber, int pageSize);
    }
}