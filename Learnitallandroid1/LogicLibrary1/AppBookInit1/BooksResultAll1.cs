using System.ComponentModel.DataAnnotations;

namespace LogicLibrary1.AppBookInit1;

public class BooksResultAll1 : IBookResult
{
    [Required] public string Uid            { get; set; } = "";
    [Required] public string DriveUrl       { get; set; } = "";
               public string Title          { get; set; } = "";
               public string Classification { get; set; } = "";
               public string Description    { get; set; } = "";
               public string Topic          { get; set; } = "";
               public string Course         { get; set; } = "";
}
