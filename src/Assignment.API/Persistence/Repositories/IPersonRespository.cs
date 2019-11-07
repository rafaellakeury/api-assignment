using System.Threading.Tasks;
using Assignment.API.Domain.Models;
using Assignment.API.Persistence.Contexts;

namespace Assignment.API.Persistence.Repositories
{
    public interface IPersonRepository
    {
        Task<DiffResult> ListDifferencesAsync(int id);
        Task AddLeftPersonAsync(LeftPerson person);
        Task AddRightPersonAsync(RightPerson person);
    }
}