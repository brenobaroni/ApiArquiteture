
#Generate CompiledContext run terminal in /src/API
```
dotnet ef dbcontext optimize --project Api.Infra --startup-project Api.Web --output-dir CompiledModels

dotnet ef migrations add FirstMigration --project Api.Infra --startup-project Api.Web
```


# Generate Migrations run terminal in /src/API
```
dotnet ef migrations add FirstMigration --project Api.Infra --startup-project Api.Web
dotnet ef database update --project Api.Infra --startup-project Api.Web
```