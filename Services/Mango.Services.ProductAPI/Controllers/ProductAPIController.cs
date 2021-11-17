using Mango.Services.ProductAPI.Models.Dtos;
using Mango.Services.ProductAPI.Repository;
using Microsoft.AspNetCore.Mvc;

namespace Mango.Services.ProductAPI.Controllers;

[Route("api/products")]
public class ProductAPIController : ControllerBase
{
    protected readonly ResponseProductDto _responseProductDto;
    private readonly ILogger<ProductAPIController> _logger;
    private readonly IProductRepository _productRepository;

    public ProductAPIController(ILogger<ProductAPIController>logger, IProductRepository productRepository)
    {
        _logger = logger;
        _productRepository = productRepository;
        _responseProductDto = new ResponseProductDto();
    }

    [HttpGet]
    public async Task<object> GetProducts()
    {
        try
        {
             IEnumerable<ProductDto> productDto = await _productRepository.GetProducts();
            _responseProductDto.Result = productDto;
        }
        catch (Exception exception)
        {
            _logger.LogError($"ERROR: An exception was thrown while calling GetProducts(): {exception.Message} | {exception.StackTrace}");
            _responseProductDto.IsSuccess = false;
            _responseProductDto.ErrorMessages = new List<string>(){$"An exception was thrown while calling GetProducts(): {exception.Message}"};
        }

        return _responseProductDto;
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<object> GetProductsById(int id)
    {
        try
        {
             ProductDto productDto = await _productRepository.GetProductById(id);
            _responseProductDto.Result = productDto;
        }
        catch (Exception exception)
        {
            _logger.LogError($"ERROR: An exception was thrown while calling GetProductsById(): {exception.Message} | {exception.StackTrace}");
            _responseProductDto.IsSuccess = false;
            _responseProductDto.ErrorMessages = new List<string>(){$"An exception was thrown while calling GetProductsById(): {exception.Message}"};
        }

        return _responseProductDto;
    }
    
    [HttpPost]
    public async Task<object> Post([FromBody] ProductDto productDto)
    {
        try
        {
            ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
            _responseProductDto.Result = model;
        }
        catch (Exception exception)
        {
            _logger.LogError($"ERROR: An exception was thrown while calling Post(): {exception.Message} | {exception.StackTrace}");
            _responseProductDto.IsSuccess = false;
            _responseProductDto.ErrorMessages = new List<string>(){$"An exception was thrown while calling Post(): {exception.Message}"};
        }

        return _responseProductDto;
    }

    [HttpPut]
    public async Task<object> Put([FromBody] ProductDto productDto)
    {
        try
        {
            ProductDto model = await _productRepository.CreateUpdateProduct(productDto);
            _responseProductDto.Result = model;
        }
        catch (Exception exception)
        {
            _logger.LogError($"ERROR: An exception was thrown while calling Put(): {exception.Message} | {exception.StackTrace}");
            _responseProductDto.IsSuccess = false;
            _responseProductDto.ErrorMessages = new List<string>(){$"An exception was thrown while calling Put(): {exception.Message}"};
        }

        return _responseProductDto;
    }
}