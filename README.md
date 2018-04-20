# NetInventory
NetInventory, a simple network inventory web app and database using SQL Database and ASP.NET MVC. 

To demo:
  1) Install SQL Server.
  2) Run the DDL.sql SQL script (from SQL Server Management Studio or directly from Visual Studio) to create the database (named "NetInventory") and to create the tables, views, and triggers.
  3) Run the Data.sql SQL script to populate the database with example data.
  4) Open the NetInvenApplication Visual Studio project.
  5) Run the local debugger to open the website.
  
Current features:
  - View all data about hardware devices in the database.
  - View port maps of network switches.
  - Edit existing devices in the database.
  - Create new devices to add to the database. 

If you have to update the database model in the Visual Studio project, follow the instructions at the below URL lest the database view "All_Device_Data" (which the website works with) be uneditable. 
https://www.c-sharpcorner.com/blogs/entity-framework-error-on-savechanges1
