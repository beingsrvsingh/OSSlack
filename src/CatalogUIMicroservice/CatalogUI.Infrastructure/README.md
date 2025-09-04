find . -name "*.csproj";

dotnet sln add ./src/CatalogUIMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context CatalogUIDbContext --startup-project ../CatalogUI.API

dotnet ef database update --context CatalogUIDbContext --startup-project ../CatalogUI.API