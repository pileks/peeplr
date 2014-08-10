## Peeplr ##

*An AngularJS learning project - basic CRUD operations over a list of contacts.*


----------

## Features ##

- Search contacts by first name, last name and associated tags
- Simple contact bookmarking (just hit Ctrl+D!)
- Associate any number of emails, tags and numbers to each contact
- Tags are unique & automatically stored, no need to hassle with more CRUD
- Typeahead for tags so you can easily assign existing tags to contacts

## Setup ##

To run Peeplr without changing anything inside the project itslef, follow these steps:

- Create a database named "Peeplr" in your localhost MSSQL instance
- Run **peeplr-test-data/Peeplr-test-data.sql** to fill the database with schema and test data.
	- Optionally, you can run **Peeplr.Data/Internal/PeeplrDatabaseModel.edmx.sql** in order to just specify the database schema 
- Enable NuGet package restore for the solution
- Build the solution & run the Peeplr.Web project

