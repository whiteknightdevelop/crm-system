using System.Threading.Tasks;
using Petadmin.Models;

namespace Petadmin.Services.Interfaces
{
    public interface IRegisterService
    {
        Task<RegisterPage> GetRegisterPageAsync();
    }
}
