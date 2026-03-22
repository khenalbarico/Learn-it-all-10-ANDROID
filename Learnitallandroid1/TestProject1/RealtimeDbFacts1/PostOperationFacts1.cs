using LogicLibrary1.AppAuth1;
using LogicLibrary1.AppDb1;
using TestProject1.TestTools1;
using Xunit.Abstractions;

namespace TestProject1.RealtimeDbFacts1;

public class PostOperationFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task RealtimeDb_Post_Book()
    {
        var user = new
        {
            Email = "khenalbarico05@gmail.com",
            Password = "test123456"
        };

        var payload = new
        {
            DriveUrl = "https://drive.google.com/file/d/1wLVyKKqEp9wMzLib950rEzjODvP6_9Et/view?usp=drive_link",
            Title = "Computer Viruses",
            Classification = "College course",
            Description = "This is a sample description",
            Course = "Information Technology",
            Topic = "Security"
        };

        var _sut = _ctx.Get<IAppDbOperator>();

        var _auth = _ctx.Get<IAppAuthentication>();

        await _auth.SignInAsync(user.Email, user.Password);

        await _sut.AddBookAsync(
            payload.DriveUrl, payload.Title, payload.Classification, payload.Description, payload.Course, payload.Topic);
    }
}
