find . -name "*.csproj";

dotnet sln add ./src/PaymentMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context PaymentDbContext --startup-project ../Payment.API 

dotnet ef database update --context PaymentDbContext --startup-project ../Payment.API