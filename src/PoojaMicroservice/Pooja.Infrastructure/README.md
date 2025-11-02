find . -name "*.csproj";

dotnet sln add ./src/PoojaMicroservice/*/*.csproj

Get-ChildItem -Path ./src/PoojaMicroservice -Recurse -Filter *.csproj | Select-Object FullName

Get-ChildItem -Path ./src/PoojaMicroservice -Recurse -Filter *.csproj | ForEach-Object {dotnet sln add $_.FullName}

cd src/poojamicroservice/pooja.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context PoojaDbContext --startup-project ../Pooja.API 

dotnet ef database update --context PoojaDbContext --startup-project ../Pooja.API