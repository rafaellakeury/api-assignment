using System.Threading.Tasks;

namespace Assignment.API.Persistence.Repositories
{
    public interface IUnitOfWork
    {
         Task CompleteAsync();
    }
}