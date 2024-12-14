namespace CloudTaskManager.Core.Interfaces.Services;

public interface IAwsService
{
    Task<bool> ValidateCredentialsAsync();
    Task<string> GetSecretAsync(string secretName);
} 