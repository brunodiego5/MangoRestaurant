using Mango.Web.Models;
using Mango.Web.Services.IServices;

namespace Mango.Web.Services;

public class ProductService : BaseService, IProductService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ILogger _logger;

    public ProductService(IHttpClientFactory httpClientFactory, ILogger<BaseService> logger) : base(httpClientFactory, logger)
    {
        _httpClientFactory = httpClientFactory;
        _logger = logger;
    }

    public async Task<T> CreateProductAsync<T>(ProductDto productDto)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.POST,
            Data = productDto,
            Url = $"{StaticDetails.ProductAPIBase}/api/products",
            AccessToken = string.Empty
        });
    }

    public async Task<T> DeleteProductAsync<T>(int id)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.DELETE,
            Url = $"{StaticDetails.ProductAPIBase}/api/products/{id}",
            AccessToken = string.Empty
        });
    }

    public async Task<T> GetAllProductsAsync<T>()
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{StaticDetails.ProductAPIBase}/api/products",
            AccessToken = string.Empty
        });
    }

    public async Task<T> GetProductByIdAsync<T>(int id)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.GET,
            Url = $"{StaticDetails.ProductAPIBase}/api/products/{id}",
            AccessToken = string.Empty
        });
    }

    public async Task<T> UpdateProductAsync<T>(ProductDto productDto)
    {
        return await this.SendAsync<T>(new ApiRequest()
        {
            ApiType = StaticDetails.ApiType.PUT,
            Data = productDto,
            Url = $"{StaticDetails.ProductAPIBase}/api/products",
            AccessToken = string.Empty
        });
    }
}