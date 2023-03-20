using Microsoft.EntityFrameworkCore;

namespace OrderKeeper.Database.Provider;

public class ProviderRepository : AbstractRepository<ProviderModel>, IProviderRepository
{

    public ProviderRepository(PostgresContext context) : base(context) { }



    public async Task<ProviderModel> CreateProvider(string name)
    {
        var model = ProviderModel.CreateModel(name);
        var result = await CreateModelAsync(model);

        if (result == null)
        {
            throw new Exception("Provider model is not created.");
        }

        return result;
    }


    public async Task<ProviderModel> GetOne(int id)
    {
        var provider = await DbModel.FindAsync(id);

        if (provider == null)
        {
            throw new Exception("Provider model is not found.");
        }

        return provider;
    }


    public async Task<List<ProviderModel>> ListAll()
    {
        var providers = await DbModel.OrderBy(x => x.Id).ToListAsync();
        return providers.DistinctBy(p => p.Name).ToList();
    }


    public async Task<bool> Delete(ProviderModel providerModel)
    {
        await DeleteModel(providerModel);
        return true;
    }
}