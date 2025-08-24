STEPS FOR VISUAL STUDIO CODE

Go To Terminal
OSSlack -> cd src

mkdir AddressMicroservice

cd AddressMicroservice

dotnet new webapi -h Address.API

dotnet new classlib -n Address.Application
dotnet new classlib -n Address.Domain
dotnet new classlib -n Address.Infrastructure

Go back to OSSlack and add the project

find . -name "*.csproj";

dotnet sln add ./src/AddressMicroservice/*/*.csproj

ipconfig getifaddr en0 