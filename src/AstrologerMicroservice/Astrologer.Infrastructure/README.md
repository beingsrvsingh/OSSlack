find . -name "*.csproj";

dotnet sln add ./src/AstrologerMicroservice/*/*.csproj

cd src/AstrologerMicroservice/astrologer.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context AstrologerDbContext --startup-project ../Astrologer.API 

dotnet ef database update --context AstrologerDbContext --startup-project ../Astrologer.API