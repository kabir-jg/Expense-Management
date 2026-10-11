using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.Expenses.Queries;

public class GetExpensesQuery : IRequest<ExpenseDTO[]>
{
    
}