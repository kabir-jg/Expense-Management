namespace ExpenseManagement.Api.Domain.Entities;

public class Attachment
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public DateTime UploadedAt { get; set; }
    public int ExpenseId { get; set; }
}