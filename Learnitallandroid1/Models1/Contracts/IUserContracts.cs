namespace Models1.Contracts;

public interface IUserContracts : ILoginContracts
{
    public string UniqueID { get; set; }
}
