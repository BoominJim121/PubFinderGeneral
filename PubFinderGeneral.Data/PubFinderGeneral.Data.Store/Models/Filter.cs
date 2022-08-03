using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubFinderGeneral.Data.Store.Models
{
    public class Filter
    {
        public Distance Distance { get; set; }
        
        public string Name { get; set; }
        
        public IList<string> Tags { get; set; }

        public Rating Rating { get; set; }

        public IList<Pub> MatchFilter(IList<Pub> allPubs)
        {
            return allPubs; // add functionality to filter further down the line. 
        }
    }
    public class Distance
    {
        public Location StartLocation { get; set; }
        public int M_Radius { get;set; }
    }

    
}
