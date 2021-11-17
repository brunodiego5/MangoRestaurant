# MangoRestaurant
Microservices Architecture with .NET Core MVC(.NET 6) and Identity Server Integration with Azure Service Bus.

In case you want to see the notes I will take along the development of this project, I'll leave here the [Notion link](https://thoracic-lake-895.notion.site/MicroServices-14ec1275fe4d443fbcd6a4b2dc01d540) to you (keep in mind that you will find a mix of English and Portuguese on those notes).

## TroubleShoot
### Nuget
If you had some trouble into restoring some nuget packages, please consider to check your packages reference. If you don't have the reference to Nuget.org source please run the following command:

```
dotnet nuget add source --name nuget.org https://api.nuget.org/v3/index.json
```

## Notes
### Seeding the database
To seed the database with some data, we are going to modify our ApplicationDbContext class a little bit.
We will need to override the OnModelCreating method with the following:
```
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
  base.OnModelCreating(modelBuilder);

  modelBuilder.Entity<Product>().HasData(new Product
  {
      ProductId = 1,
      Name = "Samosa",
      Price = 15,
      Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
      ImageUrl = "",
      CategoryName = "Appetizer"
  });
  modelBuilder.Entity<Product>().HasData(new Product
  {
      ProductId = 2,
      Name = "Paneer Tikka",
      Price = 13.99,
      Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
      ImageUrl = "",
      CategoryName = "Appetizer"
  });
  modelBuilder.Entity<Product>().HasData(new Product
  {
      ProductId = 3,
      Name = "Sweet Pie",
      Price = 10.99,
      Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
      ImageUrl = "",
      CategoryName = "Dessert"
  });
  modelBuilder.Entity<Product>().HasData(new Product
  {
      ProductId = 4,
      Name = "Pav Bhaji",
      Price = 15,
      Description = "Praesent scelerisque, mi sed ultrices condimentum, lacus ipsum viverra massa, in lobortis sapien eros in arcu. Quisque vel lacus ac magna vehicula sagittis ut non lacus.<br/>Sed volutpat tellus lorem, lacinia tincidunt tellus varius nec. Vestibulum arcu turpis, facilisis sed ligula ac, maximus malesuada neque. Phasellus commodo cursus pretium.",
      ImageUrl = "",
      CategoryName = "Entree"
  });
}
```

Those lines of code above will add four products to our database.

### Migrations
To add a migration, on Package Manager Console, you need to perform the following commands:

1) This one will create a migration folder called ***Migrations***, and inside of it the migration
class itself.
```
add-migration AddProductModelToDb
```

2) With the migration file checked, to push to the database we will use the  command bellow.
```
update-database
```

Those steps will generate the necessary changes into your database by taking advantage of ***codeFirst*** paradigm. 
Which means that based on the value ***Database***, of your connection string, it will define the name
for your database:
```
...
  "ConnectionStrings": {
    "DefaultConnection": "...Database=MangoProductAPI..."
  }
...
```

And the migration file itself will be the one responsible for create the table itself into your database. 