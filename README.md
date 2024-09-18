Usage <br/>
Change appsettings.development.json.ConnectionStrings.Postgresql to local database if needed
dotnet ef database update --project infrastructure --startup-project web
dotnet run --project web
