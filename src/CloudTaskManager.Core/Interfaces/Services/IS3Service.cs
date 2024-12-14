namespace CloudTaskManager.Core.Interfaces.Services;

public interface IS3Service
{
    Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream);
    Task<Stream> DownloadFileAsync(string bucketName, string key);
    Task DeleteFileAsync(string bucketName, string key);
} 