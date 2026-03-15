
#Migrations-Command

Scaffold-DbContext "Server=DESKTOP-NSNUU6Q;Database=Koofry;User ID=sa;Password=2wsx@WSX!;Encrypt=False;MultipleActiveResultSets=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f

Scaffold-DbContext "Server=****;Database=***;User ID=sa;Password=***;MultipleActiveResultSets=True;Encrypt=False;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Tables AspNetRole -NoOnConfiguring -f


EntityFrameworkCore\Remove-Migration -context contextname

#Configure with Postgres
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.4" />

     "DefaultConnection": "Host=localhost;Database=Koofry;Username=root;Password=root"

#scaffold

cd src/IdentityMicroservice/Identity.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context ApplicationDbContext --startup-project ../Identity.API

dotnet ef database update --context ApplicationDbContext --startup-project ../Identity.API

 #Postgres Prerequisite
 Datatype does not support
 - Nvarchar
 - bit                                 