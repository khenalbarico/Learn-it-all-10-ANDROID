namespace Models1.Authentication;

public sealed class SendOobCodeRequest
{
    public string RequestType { get; set; } = "VERIFY_EMAIL";
    public string IdToken { get; set; } = string.Empty;
}
