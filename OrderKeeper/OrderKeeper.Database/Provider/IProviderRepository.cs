namespace OrderKeeper.Database.Provider;

public interface IProviderRepository
{
    Task<ProviderModel> CreateProvider(string name);

    Task<bool> Delete(ProviderModel providerModel);

    Task<ProviderModel> GetOne(int id);

    Task<List<ProviderModel?>> ListAll();
}