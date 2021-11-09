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