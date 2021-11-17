# BlazorCompanyManager

Database should be created locally, then using migrations file in this repository and Package Manager Console use following commands:

- add-migration StartEmployeeTable
- update-database

After this you have to edit connection string in json file:
```
{
  "ConnectionStrings": {
    "MyConnection": "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\PC\\source\\repos\\BlazorCompanyManager\\BlazorCompanyManager\\BlazorCompanyDB.mdf;Integrated Security=True;Connect Timeout=30"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft": "Warning",
      "Microsoft.Hosting.Lifetime": "Information"
    }
  },
  "AllowedHosts": "*"
}
```
