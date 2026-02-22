find . -name "*.csproj";

dotnet sln add ./src/BookingMicroservice/*/*.csproj

cd src/BookingMicroservice/astrologer.infrastructure

dotnet ef migrations add Initial-Create --output-dir Migrations --context BookingDbContext --startup-project ../Booking.API 

dotnet ef database update --context BookingDbContext --startup-project ../Booking.API