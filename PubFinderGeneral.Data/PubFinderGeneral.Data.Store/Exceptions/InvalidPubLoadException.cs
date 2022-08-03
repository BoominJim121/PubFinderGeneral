using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PubFinderGeneral.Data.Store.Exceptions
{
    public class InvalidPubLoadException: Exception
    {
        public InvalidPubLoadException(string message)
            :base(message) { }
    }
}
