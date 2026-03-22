using System.Text.RegularExpressions;

namespace ToolsLib1.FileHandlingTools1;

public static class GoogleDriveUrlToStream1
{
    private static readonly HttpClient _httpClient = new();

    public static async Task<Stream> ConvertAsync<T>(this T obj)
    {

        var driveUrlProperty = typeof(T).GetProperty("DriveUrl");

        var driveUrl         = driveUrlProperty?.GetValue(obj)?.ToString();

        if (string.IsNullOrWhiteSpace(driveUrl))
            throw new InvalidOperationException("DriveUrl is null or empty.");

        var fileId = ExtractGoogleDriveFileId(driveUrl);

        var directDownloadUrl = $"https://drive.google.com/uc?export=download&id={fileId}";

        var response = await _httpClient.GetAsync(
            directDownloadUrl,
            HttpCompletionOption.ResponseHeadersRead);

        response.EnsureSuccessStatusCode();

        var memoryStream = new MemoryStream();
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        await responseStream.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        return memoryStream;
    }

    private static string ExtractGoogleDriveFileId(string url)
    {
        if (string.IsNullOrWhiteSpace(url))
            return string.Empty;

        // Handles:
        // https://drive.google.com/file/d/FILE_ID/view?usp=sharing
        var match = Regex.Match(url, @"\/file\/d\/([a-zA-Z0-9_-]+)");
        if (match.Success)
            return match.Groups[1].Value;

        // Handles:
        // https://drive.google.com/open?id=FILE_ID
        // https://drive.google.com/uc?id=FILE_ID&export=download
        match = Regex.Match(url, @"[?&]id=([a-zA-Z0-9_-]+)");
        if (match.Success)
            return match.Groups[1].Value;

        return string.Empty;
    }
}