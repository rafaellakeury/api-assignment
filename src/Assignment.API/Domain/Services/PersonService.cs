using System;
using System.Threading.Tasks;
using Assignment.API.Domain.Models;
using Assignment.API.Domain.Services.Communication;
using Assignment.API.Persistence.Repositories;

namespace Assignment.API.Domain.Services
{
    public class PersonService : IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IUnitOfWork _unitOfWork;
        private const string InvalidIdExceptionMessage = "Id must be greater than zero.";

        public PersonService(IPersonRepository personRepository, IUnitOfWork unitOfWork)
        {
            _personRepository = personRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GetDiffResponse> ListDifferencesAsync(int id)
        {
            try
            {
                ValidateId(id);

                var response = await _personRepository.ListDifferencesAsync(id);

                return new GetDiffResponse(response);
            }
            catch (Exception ex)
            {
                return new GetDiffResponse($"An error occurred when getting the diff: {ex.Message}");
            }
        }

        public async Task<SavePersonResponse> SavePersonAsync(int id, Person person)
        {
            try
            {
                ValidateId(id);

                person.Id = id;
                if (person.GetType() == typeof(LeftPerson))
                {
                    await _personRepository.AddLeftPersonAsync(person as LeftPerson);
                }
                else
                {
                    await _personRepository.AddRightPersonAsync(person as RightPerson);
                }

                await _unitOfWork.CompleteAsync();

                return new SavePersonResponse(person);
            }
            catch (Exception ex)
            {
                return new SavePersonResponse($"An error occurred when saving the person: {ex.Message}");
            }
        }

        private static void ValidateId(int id)
        {
            if (id <= 0)
            {
                throw new Exception(InvalidIdExceptionMessage);
            }
        }
    }
}