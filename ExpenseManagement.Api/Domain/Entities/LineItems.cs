namespace ExpenseManagement.Api.Domain.Entities;

public class LineItems
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int ExpenseId { get; set; }
}