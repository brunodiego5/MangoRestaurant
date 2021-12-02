using Mango.Web.Models;
using Mango.Web.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Mango.Web.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(IProductService productService, ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }

    public async Task<IActionResult> ProductIndex()
    {
        List<ProductDto> products = new();
        var response = await _productService.GetAllProductsAsync<ResponseProductDto>();
        if (response != null && response.IsSuccess)
        {
            products = JsonSerializer.Deserialize<List<ProductDto>>(response.Result.ToString());
        }

        return View(products);
    }
}