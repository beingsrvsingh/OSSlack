find . -name "*.csproj";

dotnet sln add ./src/AddressMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context AddressDbContext --startup-project ../Address.API

dotnet ef database update --context AddressDbContext --startup-project ../Address.API