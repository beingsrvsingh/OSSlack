find . -name "*.csproj";

dotnet sln add ./src/CartMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context Cart --startup-project ../Cart.API 

dotnet ef database update --context Cart --startup-project ../Cart.API