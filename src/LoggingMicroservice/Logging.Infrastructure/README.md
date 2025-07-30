dotnet ef migrations add Initial-Create --output-dir Migrations --context LoggerDbContext --startup-project ../Logging.API 

dotnet ef database update --context LoggerDbContext --startup-project ../Logging.API