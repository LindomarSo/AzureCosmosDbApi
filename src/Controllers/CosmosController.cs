using AzureCosmosDbClientApi.Models;
using AzureCosmosDbClientApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureCosmosDbClientApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CosmosController : ControllerBase
{
    private readonly ICosmosClientService _cosmosClientService;

    public CosmosController(ICosmosClientService cosmosClientService)
    {
        _cosmosClientService = cosmosClientService;
    }

    [HttpGet("getcontainerproperties/{ContainerName}/{DatabaseName}")]
    public async Task<ActionResult> GetContainerProperties([FromRoute] ContainerViewModel container) 
        => Ok(await _cosmosClientService.GetContainerByNameAsync(container));

    [HttpGet("getitems/{ContainerName}/{DatabaseName}")]
    public ActionResult GetItems([FromRoute] ContainerViewModel container) 
        => Ok(_cosmosClientService.GetItemsAsync(container));

    [HttpPost("createdatabase")]
    public async Task<ActionResult> CreateDabase([FromBody] DatabaseViewModel database) 
        => Created("", await _cosmosClientService.CreateNewDatabaseAsync(database));

    [HttpPost("createcontaimer")]
    public async Task<ActionResult> CreateContainer([FromBody] ContainerViewModel container) 
        => Created("", await _cosmosClientService.CreateNewContainerAsync(container));

    [HttpPost("createitem")]
    public async Task<ActionResult> CreateItem([FromBody] UserViewModel userViewModel) 
        => Created("", await _cosmosClientService.CreateItemAsync(userViewModel));

    [HttpDelete("deltecontainer/{ContainerName}/{DatabaseName}")]
    public async Task<ActionResult> DeleteContainer([FromRoute] ContainerViewModel container)
    {
        var status = await _cosmosClientService.DeleteContainerAsync(container);

        if (status == (int)HttpStatusCode.NoContent)
        {
            return NoContent();
        }

        return BadRequest();
    }

    [HttpDelete("deltedatabase/{DatabaseName}")]
    public async Task<ActionResult> DeletedataBase([FromRoute] DatabaseViewModel database)
    {
        var status = await _cosmosClientService.DeleteDatabaseAsync(database);

        if(status == (int)HttpStatusCode.NoContent)
        {
            return NoContent();
        }

        return BadRequest();
    }
}
