find . -name "*.csproj";

dotnet sln add ./src/AstrologerMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context AstrologerDbContext --startup-project ../AstrologerMicroservice.API 

dotnet ef database update --context AstrologerDbContext --startup-project ../AstrologerMicroservice.API