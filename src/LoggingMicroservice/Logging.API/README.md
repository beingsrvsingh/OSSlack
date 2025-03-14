
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-MKH3V65;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

EntityFrameworkCore\Add-Migration InitialCreate -OutputDir Migrations -Context contextname
EntityFrameworkCore\Update-database -context contextname

EntityFrameworkCore\Remove-Migration -context contextname

