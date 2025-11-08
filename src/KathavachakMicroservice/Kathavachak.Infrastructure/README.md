find . -name "*.csproj";

dotnet sln add ./src/KathavachakMicroservice/*/*.csproj

Get-ChildItem -Path ./src/KathavachakMicroservice -Recurse -Filter *.csproj | Select-Object FullName

Get-ChildItem -Path ./src/KathavachakMicroservice -Recurse -Filter *.csproj | ForEach-Object {dotnet sln add $_.FullName}

cd src/KathavachakMicroservice/Kathavachak.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context KathavachakDbContext --startup-project ../Kathavachak.API

dotnet ef database update --context KathavachakDbContext --startup-project ../Kathavachak.API