
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-MKH3V65;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

EntityFrameworkCore\Add-Migration Initial-Create -OutputDir Migrations -Context CatalogDbContext
EntityFrameworkCore\Update-database -context CatalogDbContext

EntityFrameworkCore\Remove-Migration -context contextname

