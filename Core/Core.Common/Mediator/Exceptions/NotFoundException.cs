namespace Core.Common.Mediator.Exceptions;


[Serializable]
  public abstract class NotFoundException : ApplicationException
    {
        protected NotFoundException(string message)
            : base("Not Found", message)
        {
        }
    }