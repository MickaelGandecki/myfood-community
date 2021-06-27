using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace myfoodapp.Hub.Common.Security
{
    public interface IConfiguration : IDisposable
    {
        HashSet<string> ApiKeys { get; }
    }
}
