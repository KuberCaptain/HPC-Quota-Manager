namespace CloudTaskManager.Core.Interfaces.Services;

public interface IEksService
{
    Task<string> DeployWorkloadAsync(string clusterName, string manifest);
    Task<bool> DeleteWorkloadAsync(string clusterName, string name, string @namespace);
    Task<string> GetWorkloadStatusAsync(string clusterName, string name, string @namespace);
} 