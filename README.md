# OnlineBookStore_Ass
## 
Building
The project was built using the Layered Pattern with 5 Layered structures:
The Layers are (in their other):
1. MigrationRunner
2. OnlineBookStore_Ass
3. OnlineBookStore_Data
4. OnlineBookStore_Domain
5. OnlineBookStore_Services

# 
The MigrationRunner.cs is a console app that was used to transfer the Data to the database.
# 
The OnlineBookStore_Ass is where the Controllers and Logging that were used for the process are. Also contains some vital files like appsettings.json that were core to the project.
# 
The OnlineBookStore_Domain is where the Entities that were required for the development process are kept.
# 
The OnlineBookStore_Data is where all that are required to set up and communicate with the Database that was used were set up.
# 
The OnlineBookStore_Services is where the dependencies were handled in parts, so that the Program.cs file does not filled with codes ensuring cleaner code pattern.

# 
The database management tool used in this project is ProgreSQL with Dapper.
The project also implemented a repository pattern, where the abstraction of all the logic that was used for the process was implemented. This was done to manage persistence and retrieving data to and from the database.

# 
Running
#
After cloning the project, it's expected that the person that wants to run the app have ProgreSQL set on his Pc, create a database called online_bookstore and just change the password of the database connection string in the appsettings.json in the OnlineBookStore_Ass and OnlineBookStore_Ass to the password of the user's ProgreSQL.
