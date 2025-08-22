
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-MKH3V65;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

EntityFrameworkCore\Add-Migration Initial-Create -OutputDir Migrations -Context CatalogDbContext
EntityFrameworkCore\Update-database -context CatalogDbContext

EntityFrameworkCore\Remove-Migration -context contextname

-- Disable foreign key checks temporarily to allow truncation of related tables
SET FOREIGN_KEY_CHECKS = 0;

TRUNCATE TABLE catalogdb.catalog_attribute_allowed_value;
TRUNCATE TABLE catalogdb.catalog_attribute;
TRUNCATE TABLE catalogdb.sub_category_localized_text;
TRUNCATE TABLE catalogdb.sub_category_master;
TRUNCATE TABLE catalogdb.category_localized_text;
TRUNCATE TABLE catalogdb.category_master;
TRUNCATE TABLE catalogdb.pooja_kit_item_master;
TRUNCATE TABLE catalogdb.catalog_attribute_icon;

SET FOREIGN_KEY_CHECKS = 1;



find . -name "*.csproj";

dotnet sln add ./src/CatalogMicroservice/*/*.csproj

dotnet ef migrations add Initial-Create --output-dir Migrations --context CatalogDbContext --startup-project ../Catalog.API 

dotnet ef database update --context CatalogDbContext --startup-project ../Catalog.API
