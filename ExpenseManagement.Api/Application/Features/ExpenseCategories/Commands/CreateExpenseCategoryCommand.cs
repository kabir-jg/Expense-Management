using ExpenseManagement.Api.Application.DTOs;
using MediatR;

namespace ExpenseManagement.Api.Application.Features.ExpenseCategories.Commands;

public class CreateExpenseCategoryCommand : IRequest<ExpenseCategoryDTO>
{
    public string Name { get; set; }
}