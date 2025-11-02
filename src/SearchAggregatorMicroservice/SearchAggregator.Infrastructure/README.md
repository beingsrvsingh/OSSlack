find . -name "*.csproj";

dotnet sln add ./src/SearchAggregatorMicroservice/*/*.csproj

Get-ChildItem -Path ./src/SearchAggregatorMicroservice -Recurse -Filter *.csproj | Select-Object FullName

Get-ChildItem -Path ./src/SearchAggregatorMicroservice -Recurse -Filter *.csproj | ForEach-Object {dotnet sln add $_.FullName}

dotnet ef migrations add Initial-Create --output-dir Migrations --context SearchDbContext --startup-project ../SearchAggregator.API 

dotnet ef database update --context SearchDbContext --startup-project ../SearchAggregator.API