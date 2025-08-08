dotnet ef migrations add Initial-Create --output-dir Migrations --context ProductDbContext --startup-project ../Product.API 

dotnet ef database update --context ProductDbContext --startup-project ../Product.API