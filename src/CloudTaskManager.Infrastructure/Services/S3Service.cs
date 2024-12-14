using Amazon.S3;
using Amazon.S3.Model;
using CloudTaskManager.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;
using Amazon.Runtime;

namespace CloudTaskManager.Infrastructure.Services;

public class S3Service : IS3Service
{
    private readonly IAmazonS3 _s3Client;
    private readonly IConfiguration _configuration;

    public S3Service(IConfiguration configuration)
    {
        _configuration = configuration;
        var credentials = new BasicAWSCredentials(
            _configuration["AWS:AccessKey"],
            _configuration["AWS:SecretKey"]
        );
        _s3Client = new AmazonS3Client(credentials);
    }

    public async Task<string> UploadFileAsync(string bucketName, string key, Stream fileStream)
    {
        try
        {
            var putRequest = new PutObjectRequest
            {
                BucketName = bucketName,
                Key = key,
                InputStream = fileStream
            };

            await _s3Client.PutObjectAsync(putRequest);
            
            return $"s3://{bucketName}/{key}";
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при загрузке файла в S3: {ex.Message}");
        }
    }

    public async Task<Stream> DownloadFileAsync(string bucketName, string key)
    {
        try
        {
            var getRequest = new GetObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            var response = await _s3Client.GetObjectAsync(getRequest);
            return response.ResponseStream;
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при скачивании файла из S3: {ex.Message}");
        }
    }

    public async Task DeleteFileAsync(string bucketName, string key)
    {
        try
        {
            var deleteRequest = new DeleteObjectRequest
            {
                BucketName = bucketName,
                Key = key
            };

            await _s3Client.DeleteObjectAsync(deleteRequest);
        }
        catch (Exception ex)
        {
            throw new Exception($"Ошибка при удалении файла из S3: {ex.Message}");
        }
    }
} 