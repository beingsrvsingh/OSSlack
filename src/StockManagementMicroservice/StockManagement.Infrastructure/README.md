#COMMAND

-dotnet ef migrations add Initial-Create --output-dir Migrations --context StockManagementDbContext --startup-project ../Stockmanagement.api

-dotnet ef database update --context StockManagementDbContext --startup-project ../Stockmanagement.API

