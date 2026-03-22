using LogicLibrary1.AppDb1;
using TestProject1.TestTools1;
using Xunit.Abstractions;

namespace TestProject1.AuthHandlerFacts1;

public class RealtimeDbFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task RealtimeDb_Patch_Book_To_User()
    {
        var uid  = "sampleUIDHAHAHA";

        var _sut = _ctx.Get<IAppDbOperator>();

        await _sut.AddUserBookAsync(uid);
    }
}
