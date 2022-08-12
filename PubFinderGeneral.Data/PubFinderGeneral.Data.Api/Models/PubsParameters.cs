using System.ComponentModel.DataAnnotations;

namespace PubFinderGeneral.Data.Api.Models
{
    public class PubsParameters
    {
        const int maxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 25;

        [Range(1, Int32.MaxValue)]
        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

        public string tags { get; set; }
    }
}
