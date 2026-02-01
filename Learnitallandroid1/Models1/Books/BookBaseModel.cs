using Models1.Contracts;
namespace Models1.Books;

public class BookBaseModel : IBookContracts
{
    public string UniqueID { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string BookUrl { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Course { get; set; } = string.Empty;
    public string Topic { get; set; } = string.Empty;
}
