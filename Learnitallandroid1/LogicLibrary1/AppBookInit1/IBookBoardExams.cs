using System.ComponentModel.DataAnnotations;

namespace LogicLibrary1.AppBookInit1;

public interface IBookBoardExams
{
    [Required] string Uid            { get; set; }
    [Required] string DriveUrl       { get; set; }
               string Title          { get; set; }
               string Classification { get; set; }
               string Description    { get; set; }
               string Topic          { get; set; }
               string Course         { get; set; }
}
