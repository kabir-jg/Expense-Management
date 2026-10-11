namespace ExpenseManagement.Api.Application.Features.Expenses.Commands;

public class LineItemCommand
{
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
}