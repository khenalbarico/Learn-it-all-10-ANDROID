using System.Diagnostics;

namespace TestProject1.TestTools1;

internal class TestLauncher
{
    public static async Task<string> SaveToTempFileAsync(
        Stream stream,
        string fileName)
    {
        var outputPath = Path.Combine(Path.GetTempPath(), fileName);

        await using var fileStream = File.Create(outputPath);
        await stream.CopyToAsync(fileStream);

        return outputPath;
    }
    public static void OpenFile(string filePath)
    {
        if (File.Exists(filePath))
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = filePath,
                UseShellExecute = true
            });
        }
    }
}
