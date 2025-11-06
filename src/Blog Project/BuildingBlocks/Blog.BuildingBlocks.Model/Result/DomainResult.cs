namespace Blog.BuildingBlocks.Model.Result
{
    public record DomainResult
    {
        public static readonly DomainResult None = new(string.Empty, string.Empty, ErrorType.Failure);

        public static readonly DomainResult NullValue = new(
            "General.Null",
            "Null value was provided",
            ErrorType.Failure);

        public DomainResult(string code, string errorMessage, ErrorType type)
        {
            Code = code;
            ErrorMessage = errorMessage;
            Type = type;
        }
        public string Code { get; }

        public string ErrorMessage { get; }

        public ErrorType Type { get; }

        public static DomainResult FailureResult(string code ,string errorMessage) =>
            new DomainResult(code, errorMessage, ErrorType.Failure);

        public static DomainResult NotFoundResult(string code,string errorMessage)=>
            new DomainResult(code , errorMessage, ErrorType.NotFound);

        public static DomainResult ConflictResult(string code,string errorMessage)=>
            new DomainResult(code,errorMessage,ErrorType.Conflict); 

        public static DomainResult ValidatoiResult(string code,string errorMessage)=>
            new DomainResult(code,errorMessage,ErrorType.Validation);
    }
}
