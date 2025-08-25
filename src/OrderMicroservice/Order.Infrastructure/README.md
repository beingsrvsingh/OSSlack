find . -name "*.csproj";

dotnet sln add ./src/OrderMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context OrderDbContext --startup-project ../Order.API 

dotnet ef database update --context OrderDbContext --startup-project ../Order.API