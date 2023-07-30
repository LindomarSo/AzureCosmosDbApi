using AzureCosmosDbClientApi.Models;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;

namespace AzureCosmosDbClientApi.Services;

public class CosmosClientService : ICosmosClientService
{
    private readonly CosmosClient _cosmosClient;

    public CosmosClientService(IOptions<CosmosSettings> cosmosSettings)
    {
        _cosmosClient = new CosmosClient(cosmosSettings.Value.EndpointUri, cosmosSettings.Value.PrimaryKey);
    }

    public async Task<string> CreateNewContainerAsync(ContainerViewModel containerView)
    {
        Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(containerView.DatabaseName);
        Container container = await database.CreateContainerIfNotExistsAsync(containerView.ContainerName, "/LastName");

        return container.Id;
    }

    public async Task<UserModel> CreateItemAsync(UserViewModel userViewModel)
    {
        Database database = _cosmosClient.GetDatabase(userViewModel.DatabaseName);
        Container conteiner = database.GetContainer(userViewModel.ContainerName);
        var userModel = new UserModel(userViewModel.Id, userViewModel.UserName, userViewModel.LastName, userViewModel.UserEmail);

        var itemResponse = await conteiner.CreateItemAsync<UserModel>(userModel);        

        return itemResponse.Resource;
    }

    public async Task<string> CreateNewDatabaseAsync(DatabaseViewModel databaseView)
    {
        Database database = await _cosmosClient.CreateDatabaseIfNotExistsAsync(databaseView.DatabaseName);

        return database.Id;
    }

    public async Task<int> DeleteContainerAsync(ContainerViewModel container)
    {
        Database database = _cosmosClient.GetDatabase(container.DatabaseName);
        Container conteiner = database.GetContainer(container.ContainerName);

        var response = await conteiner.DeleteContainerAsync();

        return (int)response.StatusCode;
    }

    public async Task<int> DeleteDatabaseAsync(DatabaseViewModel database)
    {
        Database dataBase = _cosmosClient.GetDatabase(database.DatabaseName);

        var response = await dataBase.DeleteAsync();

        return (int)response.StatusCode;
    }

    public async Task<ContainerProperties> GetContainerByNameAsync(ContainerViewModel container)
    {
        Database database = _cosmosClient.GetDatabase(container.DatabaseName);
        Container conteiner = database.GetContainer(container.ContainerName);

        return await conteiner.ReadContainerAsync();
    }

    public object GetItemsAsync(ContainerViewModel container)
    {
        var containerClient = _cosmosClient.GetContainer(container.DatabaseName, container.ContainerName);

        return containerClient.GetItemLinqQueryable<UserModel>(true);
    }
}
