using Microsoft.AspNetCore.Mvc;
using OrderKeeper.Database;

namespace OrderKeeper.API.Controllers.Client;

public class ProviderController : AbstractClientController
{

    public ProviderController(IDatabaseContainer databaseContainer) : base(databaseContainer) { }
    
    [HttpPost]
    public async Task<IActionResult> CreateProvider(string name)
    {
        var createdProvider = await DatabaseContainer.Provider.CreateProvider(name);
        return Ok(createdProvider);
    }
    
    [HttpGet]
    public async Task<IActionResult> GetProviderById(int id)
    {
        var provider = await DatabaseContainer.Provider.GetOne(id);
        return Ok(provider);
    }
    
    [HttpGet]
    public async Task<IActionResult> ListAll(int id)
    {
        var providers = await DatabaseContainer.Provider.GetAll();
        return Ok(providers);
    }
    
    [HttpDelete]
    public async Task<IActionResult> DeleteProviderById(int id)
    {
        var provider = await DatabaseContainer.Provider.GetOne(id);
        await DatabaseContainer.Provider.Delete(provider);
        return Ok();
    }

}