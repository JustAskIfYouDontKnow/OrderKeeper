using Microsoft.AspNetCore.Mvc;
using OrderKeeper.Database;

namespace OrderKeeper.API.Controllers.Client;

public class ProviderController : AbstractClientController
{

    public ProviderController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }

}