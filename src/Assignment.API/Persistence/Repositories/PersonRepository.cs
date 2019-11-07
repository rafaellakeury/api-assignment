using System.Threading.Tasks;
using Assignment.API.Domain.Models;
using Assignment.API.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Assignment.API.Persistence.Repositories
{
    public class PersonRepository : BaseRepository, IPersonRepository
    {
        public PersonRepository(AppDbContext context) : base(context)
        {
        }

        public async Task AddLeftPersonAsync(LeftPerson person)
        {
		      await _context.LeftPeople.AddAsync(person);
        }

        public async Task AddRightPersonAsync(RightPerson person)
        {
		      await _context.RightPeople.AddAsync(person);
        }

        public async Task<DiffResult> ListDifferencesAsync(int id)
        {
          var rightPerson = await _context.RightPeople.FirstOrDefaultAsync(p => p.Id == id);
          var leftPerson = await _context.LeftPeople.FirstOrDefaultAsync(p => p.Id == id);
          return  PeopleComparerHelper.Compare(rightPerson, leftPerson);
        }
    }
}