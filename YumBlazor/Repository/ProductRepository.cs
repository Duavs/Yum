using Microsoft.EntityFrameworkCore;
using YumBlazor.Data;
using YumBlazor.Repository.IRepository;

namespace YumBlazor.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _db;
    private readonly IWebHostEnvironment _webHostEnvironment;
    public ProductRepository(ApplicationDbContext db, IWebHostEnvironment webHostEnvironment)
    {
        _db = db;
        _webHostEnvironment = webHostEnvironment;
        
    }
    public async Task<Product>  CreateAsync(Product obj)
    {
        await _db.Product.AddAsync(obj);
        await _db.SaveChangesAsync();
        return obj;
    }

    public async Task<Product> UpdateAsync(Product obj)
    {
        var objFromDb = _db.Product.FirstOrDefault(u => u.Id == obj.Id);
        if (objFromDb != null)
        {
            objFromDb.Name = obj.Name;
            objFromDb.Description = obj.Description;
            objFromDb.SpecialTag = obj.SpecialTag;
            objFromDb.Price = obj.Price;
            objFromDb.CategoryId = obj.CategoryId;
            objFromDb.ImageUrl = obj.ImageUrl; 
            _db.Product.Update(objFromDb);
            await _db.SaveChangesAsync();
            return objFromDb;
        }

        return obj;
    }

    public async Task<bool>  DeleteAsync(int id)
    {
        var obj = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
        var imagePath = Path.Combine(_webHostEnvironment.WebRootPath, obj.ImageUrl.TrimStart('/'));
        if (File.Exists(imagePath))
        {
            File.Delete(imagePath);
        }
        if (obj != null)
        {
            _db.Product.Remove(obj);
            return (await _db.SaveChangesAsync()) > 0;
        }

        return false;
    }

    public async Task<Product>GetAsync(int id)
    {
       var obj = await _db.Product.FirstOrDefaultAsync(u => u.Id == id);
       if (obj == null)
       {
           return new Product();
       }
       return obj;
    }
    
    public async Task<IEnumerable<Product>> GetAllAsync()
    {

        return await _db.Product.Include(u => u.Category).ToListAsync();
    }

}