Documentation Link: https://documenter.getpostman.com/view/32464919/2s9YymFPQv

In the ExpPayment.Data folder run below comand to Add Migrations to database:
  dotnet ef migrations add InitialMigration -s ../ExpPayment.Api/ 
Then To update database with the migration run below comand in Sln Folder (Which is upper folder of ExpPayment.Data and generally can move from ExpPayment.Data to sln folder with "cd .." command):
  dotnet ef database update --project "./ExpPayment.Data" --startup-project "./ExpPayment.Api"

Default Admin User Info:
USERNAME: admin
PASSWORD: admin123

Defaul Personel User Info:
USERNAME: personel
PASSWORD: personel123
