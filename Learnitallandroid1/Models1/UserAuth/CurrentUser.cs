using System.ComponentModel.DataAnnotations;

namespace Models1.UserAuth;

public class CurrentUser
{
    [Required] public string Uid { get; set; } = string.Empty;
               public string Token { get; set; } = string.Empty;
}
