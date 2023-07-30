using AzureCosmosDbClientApi.Models;
using AzureCosmosDbClientApi.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace AzureCosmosDbClientApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EnvironmentController : ControllerBase
{
    private readonly IConfiguration configuration;

    public EnvironmentController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpGet()]
    public ActionResult GetContainerProperties([FromRoute] ContainerViewModel container) 
        => Ok(this.configuration["Environment"]);
}
