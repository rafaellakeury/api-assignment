namespace Assignment.API.Domain.Services.Communication
{
    public class SavePersonResponse : BaseResponse
    {
        public Person Person { get; private set; }

        private SavePersonResponse(bool success, string message, Person person) : base(success, message)
        {
            Person = person;
        }

        public SavePersonResponse(Person person) : this(true, string.Empty, person)
        { }

        public SavePersonResponse(string message) : this(false, message, null)
        { }
    }
}