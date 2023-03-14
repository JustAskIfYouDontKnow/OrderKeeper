using Microsoft.AspNetCore.Mvc;
using OrderKeeper.Database;

namespace OrderKeeper.API.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public abstract class AbstractClientController : ControllerBase
{
    protected readonly IDatabaseContainer DatabaseContainer;
    protected AbstractClientController(IDatabaseContainer databaseContainer)
    {
        DatabaseContainer = databaseContainer;
    }
}