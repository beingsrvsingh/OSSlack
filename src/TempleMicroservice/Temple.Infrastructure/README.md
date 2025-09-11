find . -name "*.csproj";

dotnet sln add ./src/TempleMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context TempleDbContext --startup-project ../Temple.API 

dotnet ef database update --context TempleDbContext --startup-project ../Temple.API