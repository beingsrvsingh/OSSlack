find . -name "*.csproj";

dotnet sln add ./src/CatalogUIMicroservice/*/*.csproj

Get-ChildItem -Path ./src/CatalogUIMicroservice -Recurse -Filter *.csproj | Select-Object FullName

Get-ChildItem -Path ./src/CatalogUIMicroservice -Recurse -Filter *.csproj | ForEach-Object {dotnet sln add $_.FullName}

dotnet ef migrations add Initial-Create --output-dir Migrations --context CatalogUIDbContext --startup-project ../CatalogUI.API

dotnet ef database update --context CatalogUIDbContext --startup-project ../CatalogUI.API