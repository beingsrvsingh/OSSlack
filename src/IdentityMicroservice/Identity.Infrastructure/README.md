
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-NSNUU6Q;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

EntityFrameworkCore\Add-Migration Initial-Create -OutputDir Migrations -Context ApplicationDbContext
EntityFrameworkCore\Update-database -context ApplicationDbContext

EntityFrameworkCore\Remove-Migration -context contextname

