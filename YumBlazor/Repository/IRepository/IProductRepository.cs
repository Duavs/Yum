 namespace YumBlazor.Repository.IRepository;

using YumBlazor.Data;

public interface IProductRepository
{
    public Task<Product> CreateAsync(Product obj);
    public Task<Product> UpdateAsync(Product obj);
    public Task<bool> DeleteAsync(int id);
    public Task<Product> GetAsync(int id);
    public Task<IEnumerable<Product>> GetAllAsync();
}