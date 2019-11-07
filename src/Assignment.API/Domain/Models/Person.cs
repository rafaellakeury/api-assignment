using System;

namespace Assignment.API
{
    public class Person
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public string City { get; set; }

        public string Profession { get; set; }

        public bool HasName()
        {
            return this.Name != null;
        }

        public bool HasAge()
        {
            return this.Age != 0;
        }

        public bool HasCity()
        {
            return this.City != null;
        }

        public bool HasProfession()
        {
            return this.Profession != null;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Person);
        }

        public bool Equals(Person other)
        {
            return other != null &&
                    Name == other.Name &&
                    Age == other.Age &&
                    City == other.City &&
                    Profession == other.Profession;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Name, Age, City, Profession);
        }
    }
}