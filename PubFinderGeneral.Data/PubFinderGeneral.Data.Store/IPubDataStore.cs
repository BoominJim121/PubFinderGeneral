using PubFinderGeneral.Data.Store.Models;

namespace PubFinderGeneral.Data.Store
{
    public interface IPubDataStore
    {
        ICollection<Pub> GetAllPubData();
        ICollection<Pub> GetFilteredPubData(Filter filter);
    }
}