using AutoMapper;
using Mango.Services.ProductAPI.DbContexts;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.Dtos;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Repository;

public class ProductRepository : IProductRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IMapper _mapper;
    private readonly ILogger<ProductRepository> _logger;

    public ProductRepository(ApplicationDbContext dbContext, IMapper mapper, ILogger<ProductRepository> logger)
    {
        _dbContext = dbContext;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
    {
        if (productDto == null)
        {
            _logger.LogError($"ERROR: Trying to create/update a product based on a null DTO.");
            throw new ArgumentNullException(nameof(productDto));
        }

        Product product = _mapper.Map<ProductDto, Product>(productDto);
        if(product.ProductId > 0){
            _dbContext.Products.Update(product);
        }
        else{
            _dbContext.Products.Add(product);
        }

        await _dbContext.SaveChangesAsync();
        return _mapper.Map<Product, ProductDto>(product);
    }

    public async Task<bool> DeleteProduct(int productId)
    {
        try
        {
            Product product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == productId);
            if(product == null){
                return false;
            }
            
            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return true;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception.Message);
            return false;
        }
    }

    public async Task<ProductDto> GetProductById(int productId)
    {
        if(productId <= 0){
            _logger.LogError($"ERROR: The requested id [{productId}] is not a valid id.");
            return null;
        }
        
        Product product = await _dbContext.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
        return _mapper.Map<ProductDto>(product);
    }

    public async Task<IEnumerable<ProductDto>> GetProducts()
    {
        List<Product> productList = await _dbContext.Products.ToListAsync();
        return _mapper.Map<List<ProductDto>>(productList);
    }
}