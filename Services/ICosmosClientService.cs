using AzureCosmosDbClientApi.Models;
using Microsoft.Azure.Cosmos;

namespace AzureCosmosDbClientApi.Services;

public interface ICosmosClientService
{
    Task<string> CreateNewDatabaseAsync(DatabaseViewModel database);
    Task<string> CreateNewContainerAsync(ContainerViewModel container);
    Task<UserModel> CreateItemAsync(UserViewModel userViewModel);
    Task<ContainerProperties> GetContainerByNameAsync(ContainerViewModel container);
    object GetItemsAsync(ContainerViewModel container);
    Task<int> DeleteDatabaseAsync(DatabaseViewModel database);
    Task<int> DeleteContainerAsync(ContainerViewModel container);
}
