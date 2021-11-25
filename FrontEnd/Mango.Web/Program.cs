using Mango.Web;
using Mango.Web.Services;
using Mango.Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add service to use HTTP client factory
builder.Services.AddHttpClient<IProductService, ProductService>();

// Registering the value of ProductAPIBase as the one that is set on the ProductAPI element on appsettings.json
StaticDetails.ProductAPIBase=builder.Configuration["ServiceUrls:ProductAPI"];

// Registering the IProductRepository
builder.Services.AddScoped<IProductService, ProductService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();