using System.Collections.Generic;
using System.Threading.Tasks;

namespace Petadmin.Brokers.Interfaces
{
    public interface IRegisterBroker
    {
        Task<IEnumerable<string>> GetGendersListAsync();
    }
}
