namespace ExpenseManagement.Api.Domain.Entities;

public class Attachment
{
    public int Id { get; set; }
    public required string  FileName { get; set; }
    public required string FileUrl { get; set; }
    public DateTime UploadedAt { get; set; }
    public int ExpenseId { get; set; }
}