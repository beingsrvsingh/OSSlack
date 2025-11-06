find . -name "*.csproj";

dotnet sln add ./src/TempleMicroservice/*/*.csproj

cd src/TempleMicroservice/temple.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context TempleDbContext --startup-project ../Temple.API 

dotnet ef database update --context TempleDbContext --startup-project ../Temple.API