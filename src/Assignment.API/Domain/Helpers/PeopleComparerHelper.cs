using System.Collections.Generic;
using Assignment.API.Domain.Models;

namespace Assignment.API.Persistence.Repositories
{
    public static class PeopleComparerHelper
    {
        public static DiffResult Compare(Person rightPerson, Person leftPerson)
        {
            if (rightPerson == null || leftPerson == null)
                return null;

            var diffResult = new DiffResult();

            if (rightPerson.Equals(leftPerson))
            {
                diffResult.AreEqual = true;
                diffResult.AreSameSize = true;
            }
            else if (AreSameSize(rightPerson, leftPerson))
            {
                diffResult.AreEqual = false;
                diffResult.AreSameSize = true;
                diffResult.Differences = GetPeopleDifferences(rightPerson, leftPerson);
            }

            return diffResult;
        }

        private static bool AreSameSize(Person rightPerson, Person leftPerson)
        {
            return rightPerson != null && leftPerson != null &&
                    rightPerson.HasName() == leftPerson.HasName() &&
                    rightPerson.HasAge() == leftPerson.HasAge() &&
                    rightPerson.HasCity() == leftPerson.HasCity() &&
                    rightPerson.HasProfession() == leftPerson.HasProfession();
        }

        private static List<string> GetPeopleDifferences(Person rightPerson, Person leftPerson)
        {
            var differences = new List<string>();
            if (rightPerson.Name != leftPerson.Name)
            {
                differences.Add(PersonPropertyTypes.Name);
            }
            if (rightPerson.Age != leftPerson.Age)
            {
                differences.Add(PersonPropertyTypes.Age);
            }
            if (rightPerson.City != leftPerson.City)
            {
                differences.Add(PersonPropertyTypes.City);
            }
            if (rightPerson.Profession != leftPerson.Profession)
            {
                differences.Add(PersonPropertyTypes.Profession);
            }

            return differences;
        }
    }
}