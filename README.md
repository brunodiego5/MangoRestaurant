# MangoRestaurant
Microservices Architecture with .NET Core MVC(.NET 6) and Identity Server Integration with Azure Service Bus.

In case you want to see the notes I will take along the development of this project, I'll leave here the [Notion link](https://thoracic-lake-895.notion.site/MicroServices-14ec1275fe4d443fbcd6a4b2dc01d540) to you (keep in mind that you will find a mix of English and Portuguese on those notes).

## TroubleShoot
### Nuget
If you had some trouble into restoring some nuget packages, please consider to check your packages reference. If you don't have the reference to Nuget.org source please run the following command:

```
dotnet nuget add source --name nuget.org https://api.nuget.org/v3/index.json
```