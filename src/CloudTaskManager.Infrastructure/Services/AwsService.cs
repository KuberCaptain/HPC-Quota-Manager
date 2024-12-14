using Amazon.Runtime;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using CloudTaskManager.Core.Interfaces.Services;
using Microsoft.Extensions.Configuration;

namespace CloudTaskManager.Infrastructure.Services;

public class AwsService : IAwsService
{
    private readonly IAmazonSecretsManager _secretsManager;
    private readonly IConfiguration _configuration;

    public AwsService(IConfiguration configuration)
    {
        _configuration = configuration;
        var credentials = new BasicAWSCredentials(
            _configuration["AWS:AccessKey"],
            _configuration["AWS:SecretKey"]
        );
        _secretsManager = new AmazonSecretsManagerClient(credentials);
    }

    public async Task<bool> ValidateCredentialsAsync()
    {
        try
        {
            await _secretsManager.ListSecretsAsync(new ListSecretsRequest());
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetSecretAsync(string secretName)
    {
        var request = new GetSecretValueRequest
        {
            SecretId = secretName
        };

        var response = await _secretsManager.GetSecretValueAsync(request);
        return response.SecretString;
    }
} 