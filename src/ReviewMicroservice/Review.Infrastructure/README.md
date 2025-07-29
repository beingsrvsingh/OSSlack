
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-MKH3V65;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables Reviews, ReviewFeedback, ReviewReportLookup, ReviewReportInfo -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

EntityFrameworkCore\Add-Migration Initial-Create -OutputDir Migrations -Context ReviewDbContext
EntityFrameworkCore\Update-database -context ReviewDbContext

EntityFrameworkCore\Remove-Migration -context contextname

#Select Tables
select * from reviews
select * from ReviewDetails
select * from ReviewReportLookup
select * from reviewauditlogs

#Drop tables
drop table ReviewAuditLogs
drop table Reviews
drop table reviewdetails
drop table reviewreportlookup

dotnet ef migrations add Initial-Create --output-dir Migrations --context ReviewDbContext --startup-project ../Review.API 

dotnet ef database update --context ReviewDbContext --startup-project ../Review.API



