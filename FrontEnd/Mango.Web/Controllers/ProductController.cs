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

    [HttpGet]
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

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.CreateProductAsync<ResponseProductDto>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> EditProduct(int productId)
    {
        var response = await _productService.GetProductByIdAsync<ResponseProductDto>(productId);
        if (response != null && response.IsSuccess)
        {
            ProductDto model = JsonSerializer.Deserialize<ProductDto>(response.Result.ToString(), _jsonOptions);
            return View(model);
        }

        return NotFound();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> EditProduct(ProductDto model)
    {
        if (ModelState.IsValid)
        {
            var response = await _productService.UpdateProductAsync<ResponseProductDto>(model);
            if (response != null && response.IsSuccess)
            {
                return RedirectToAction(nameof(ProductIndex));
            }
        }

        return View(model);
    }
}