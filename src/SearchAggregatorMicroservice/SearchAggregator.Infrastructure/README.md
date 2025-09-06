find . -name "*.csproj";

dotnet sln add ./src/SearchAggregatorMicroservice/*/*.csproj

Get-ChildItem -Path ./src/SearchAggregatorMicroservice -Recurse -Filter *.csproj | Select-Object FullName

Get-ChildItem -Path ./src/SearchAggregatorMicroservice -Recurse -Filter *.csproj | ForEach-Object {dotnet sln add $_.FullName}