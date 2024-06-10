Building
#
The project was built using the Layered Structure Design Pattern with five layered structures. The layers are (in order):
#
1. MigrationRunner
2. OnlineBookStore_Ass
3. OnlineBookStore_Data
4. OnlineBookStore_Domain
5. OnlineBookStore_Services
#
Layer Descriptions
#
MigrationRunner: A console application used to transfer the data to the database.
#
OnlineBookStore_Ass: Contains the controllers and logging used for the process. It also includes vital files like appsettings.json, which are core to the project.
#
OnlineBookStore_Domain: Contains the entities required for the development process.
#
OnlineBookStore_Data: Contains all the necessary configurations and setup to communicate with the database.
#
OnlineBookStore_Services: Handles dependencies to ensure the Program.cs file remains clean and follows a proper code pattern.
##
Database Management
#
The database management tool used in this project is PostgreSQL with Dapper. The project also implemented a repository pattern to manage persistence and data retrieval from the database. This abstraction helps to keep the logic organized and maintainable.
#

Running the Project
#
After cloning the project, ensure you have PostgreSQL set up on your PC. Follow these steps:
#

Create a database named online_bookstore.
#
Update the database connection string in the appsettings.json file within the OnlineBookStore_Ass project to match your PostgreSQL password.
