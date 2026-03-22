using LogicLibrary1.AppAuth1;
using LogicLibrary1.AppBookInit1;
using LogicLibrary1.AppDb1;
using TestProject1.TestTools1;
using ToolsLib1.FileHandlingTools1;
using Xunit.Abstractions;

namespace TestProject1.RealtimeDbFacts1;

public class GetOperationFacts1 (ITestOutputHelper _ctx)
{
    [Fact] public async Task RealtimeDb_Get_Books()
    {
        var user = new
        {
            Email    = "khenalbarico05@gmail.com",
            Password = "test123456"
        };

        var _sut  = _ctx.Get<IAppDbOperator>();

        var _auth = _ctx.Get<IAppAuthentication>();

        await _auth.SignInAsync(user.Email, user.Password);

        var res  = await _sut.LoadAllBooksAsync<BooksResultAll1>();

        foreach (var book in res)
        {
            var pdfStream = await book.ConvertAsync();

            var outputPath = await TestLauncher.SaveToTempFileAsync(pdfStream, $"{book.Title}.pdf");

            TestLauncher.OpenFile(outputPath);
        }
    }
}
