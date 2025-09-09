find . -name "*.csproj";

dotnet sln add ./src/PriestMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context PriestDbContext --startup-project ../Priest.API 

dotnet ef database update --context PriestDbContext --startup-project ../Priest.API