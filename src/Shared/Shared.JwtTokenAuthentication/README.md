
#Migrations-Command

Set the Identity.API as startup project and run the below command

Scaffold-DbContext "Server=DESKTOP-MKH3V65;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

------------------------------------------------------------------------------------------------------------

EntityFrameworkCore\Add-Migration InitialCreate -OutputDir Infrastructure/Migrations -Context TokenDbContext
EntityFrameworkCore\Update-database -context TokenDbContext

EntityFrameworkCore\Remove-Migration -context contextname

