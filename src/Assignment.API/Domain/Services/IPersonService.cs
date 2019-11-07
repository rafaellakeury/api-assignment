using System.Threading.Tasks;
using Assignment.API.Domain.Services.Communication;

namespace Assignment.API.Domain.Services
{
    public interface IPersonService
    {
        Task<GetDiffResponse> ListDifferencesAsync(int id);
        Task<SavePersonResponse> SavePersonAsync(int id, Person person);
    }
}