namespace ExpenseManagement.Api.Application.DTOs;

public class LineItemDTO
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? Description { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; }
}