using Assignment.API.Domain.Models;
using Assignment.API.Domain.Services;
using Assignment.API.Persistence.Repositories;
using Moq;
using NUnit.Framework;

namespace Assignment.API.Tests
{
    [TestFixture]
    public class PersonServiceTests
    {
        private const string GetInvalidIdErrorMessage = "An error occurred when getting the diff: Id must be greater than zero.";
        private const string PostInvalidIdErrorMessage = "An error occurred when saving the person: Id must be greater than zero.";
        private static Mock<IPersonRepository> personRepositoryMock = new Mock<IPersonRepository>();
        private static Mock<IUnitOfWork> unitOfWorkMock = new Mock<IUnitOfWork>();
        private PersonService personService = new PersonService(personRepositoryMock.Object, unitOfWorkMock.Object);

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async System.Threading.Tasks.Task ListInvalidId_ReturnsErrorMessageAsync()
        {
            //Arrange
            var invalidId = 0;

            //Act
            var actualResult = await personService.ListDifferencesAsync(invalidId);

            //Assert
            Assert.AreEqual(GetInvalidIdErrorMessage, actualResult.Message);
        }

        [Test]
        public async System.Threading.Tasks.Task PostInvalidIdToLeftEndpoint_ReturnsErrorMessageAsync()
        {
            //Arrange
            var invalidId = -1;

            //Act
            var actualResult = await personService.SavePersonAsync(invalidId, new LeftPerson());

            //Assert
            Assert.AreEqual(PostInvalidIdErrorMessage, actualResult.Message);
        }

        [Test]
        public async System.Threading.Tasks.Task PostInvalidIdToRightEndpoint_ReturnsErrorMessageAsync()
        {
            //Arrange
            var invalidId = -1;

            //Act
            var actualResult = await personService.SavePersonAsync(invalidId, new RightPerson());

            //Assert
            Assert.AreEqual(PostInvalidIdErrorMessage, actualResult.Message);
        }
    }
}