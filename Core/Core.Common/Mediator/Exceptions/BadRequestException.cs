namespace Core.Common.Mediator.Exceptions;


[Serializable]
 public abstract class BadRequestException : ApplicationException
    {
        protected BadRequestException(string message)
            : base("Bad Request", message)
        {
        }
    }