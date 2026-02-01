using Models1.Contracts;

namespace Models1.Users;

public class UserBaseModel : IUserContracts
{
    public string UniqueID { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; //Hash this password before storing it
}
