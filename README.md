# MangoRestaurant
Microservices Architecture with .NET Core MVC(.NET 6) and Identity Server Integration with Azure Service Bus

## TroubleShoot
### Nuget
If you had some trouble into restoring some nuget packages, please consider to check your packages reference. If you don't have the reference to Nuget.org source please run the following command:

```
dotnet nuget add source --name nuget.org https://api.nuget.org/v3/index.json
```