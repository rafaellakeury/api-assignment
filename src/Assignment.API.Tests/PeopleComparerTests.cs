using NUnit.Framework;
using Assignment.API.Persistence.Repositories;
using System.Collections.Generic;

namespace Assignment.API.Tests
{
    [TestFixture]
    public class PeopleComparerTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CompareEqualPeople_ReturnsExpectedResult()
        {
            //Arrange
            var rightPerson = new Person() { Name = "Rafa", Age = 28, City = "Berlin", Profession = "Dev" };
            var leftPerson = new Person() { Name = "Rafa", Age = 28, City = "Berlin", Profession = "Dev" };
            var expectedResult = new DiffResult() { AreEqual = true, AreSameSize = true, };

            //Act
            var actualResult = PeopleComparerHelper.Compare(rightPerson, leftPerson);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void ComparePeopleWithDifferentSizes_ReturnsExpectedResult()
        {
            //Arrange
            var rightPerson = new Person() { Name = "Rafa", City = "Berlin", Profession = "Dev" };
            var leftPerson = new Person() { Name = "Rafa", Age = 28, City = "Berlin", Profession = "Dev" };
            var expectedResult = new DiffResult() { AreEqual = false, AreSameSize = false };

            //Act
            var actualResult = PeopleComparerHelper.Compare(rightPerson, leftPerson);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CompareDifferentPeopleOfTheSameSize_ReturnsExpectedResult()
        {
            //Arrange
            var rightPerson = new Person() { Name = "Rafa", Age = 18, City = "Denver", Profession = "Dev" };
            var leftPerson = new Person() { Name = "Rafa", Age = 28, City = "Berlin", Profession = "Dev" };
            var expectedDifferences = new List<string>() { "age", "city" };
            var expectedResult = new DiffResult()
            {
                AreEqual = false,
                AreSameSize = true,
                Differences = expectedDifferences
            };

            //Act
            var actualResult = PeopleComparerHelper.Compare(rightPerson, leftPerson);

            //Assert
            Assert.AreEqual(expectedResult, actualResult);
        }

        [Test]
        public void CompareWithNull_ReturnsNull()
        {
            //Arrange
            var rightPerson = new Person() { Name = "Rafa", Age = 18, City = "Denver", Profession = "Dev" };

            //Act
            var actualResult = PeopleComparerHelper.Compare(rightPerson, null);

            //Assert
            Assert.IsNull(actualResult);
        }

        [Test]
        public void CompareNullPeople_ReturnsNull()
        {
            //Act
            var actualResult = PeopleComparerHelper.Compare(null, null);

            //Assert
            Assert.IsNull(actualResult);
        }
    }
}