using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mango.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;
    private readonly JsonSerializerOptions _jsonOptions;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
        _jsonOptions = new(JsonSerializerDefaults.Web);
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> products = new();
        var response = await _productService.GetAllProductsAsync<ResponseProductDto>();
        if (response != null && response.IsSuccess)
        {
            products = JsonSerializer.Deserialize<List<ProductDto>>(response.Result.ToString(), _jsonOptions);
        }

        return View(products);
    }
}