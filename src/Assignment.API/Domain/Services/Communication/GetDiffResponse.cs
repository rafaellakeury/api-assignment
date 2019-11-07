namespace Assignment.API.Domain.Services.Communication
{
    public class GetDiffResponse : BaseResponse
    {
        public DiffResult DiffResult { get; private set; }

        private GetDiffResponse(bool success, string message, DiffResult diffResult) : base(success, message)
        {
            DiffResult = diffResult;
        }

        public GetDiffResponse(DiffResult diffResult) : this(true, string.Empty, diffResult)
        { }

        public GetDiffResponse(string message) : this(false, message, null)
        { }
    }
}