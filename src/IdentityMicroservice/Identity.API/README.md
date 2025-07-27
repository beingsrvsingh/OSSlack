#LocationMigration - 

Steps - 

#Prerequisite

	RegistryKey
	Add OSSlack folder
	Add ConnectionString and SecurityKey as string


Excel sheet data migration

update statemaster set name = 'Andaman and Nico.In.' where id = 1
update statemaster set name = 'Dadra and Nagar Hav.' where id = 8
update statemaster set name = 'Daman and Diu' where id = 9
update statemaster set name = 'Jammu and Kashmir' where id = 15
update statemaster set name = 'Megalaya' where id = 23


original

update statemaster set name = 'ANDAMAN & NICOBAR' where id = 1
update statemaster set name = 'DADRA & NAGAR HAVELI' where id = 8
update statemaster set name = 'DAMAN & DIU' where id = 9
update statemaster set name = 'JAMMU & KASHMIR' where id = 15
update statemaster set name = 'MEGHALAYA' where id = 23


delete from CountryMaster
delete from StateMaster
delete from PincodeMaster
delete from DistrictMaster
delete from AreaMaster

DBCC CHECKIDENT(COUNTRYMASTER,RESEED,0)
DBCC CHECKIDENT(STATEMASTER,RESEED,0)
DBCC CHECKIDENT(PINCODEMASTER,RESEED,0)
DBCC CHECKIDENT(DISTRICTMASTER,RESEED,0)
DBCC CHECKIDENT(AREAMASTER,RESEED,0)

## PRE-REQUISIT

// Register essential framework services like IHttpClientFactory and IMemoryCache here in Program.cs.
// These services are core infrastructure components provided by Microsoft.Extensions.DependencyInjection.
// Registering them at this level ensures they are available throughout the microservice and properly integrated with Autofac.
// Avoid registering these inside Autofac modules, as these methods are specific to IServiceCollection and must be configured early.

builder.Services.AddHttpClient();
builder.Services.AddMemoryCache();  

## TO INSERT BULK RECORD (MYSQL)
-	Add in connection string - AllowLoadLocalInfile=true;
-	SHOW VARIABLES LIKE 'local_infile';
-	SET GLOBAL local_infile = 1;
