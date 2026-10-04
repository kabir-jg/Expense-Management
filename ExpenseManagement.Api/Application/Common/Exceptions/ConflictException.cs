namespace ExpenseManagement.Api.Application.Common.Exceptions;

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message)
    {
       
    }
}