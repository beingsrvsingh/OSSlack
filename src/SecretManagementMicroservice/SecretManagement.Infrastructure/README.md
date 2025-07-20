#COMMAND

-dotnet ef migrations add Initial-Create --output-dir Migrations --context SecretManagementDbContext --startup-project ../secretmanagement.api

-dotnet ef database update --context SecretManagementDbContext --startup-project ../secretmanagement.API

