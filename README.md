# CDRTechTest

# Technology Choices

I decided to use the CSVReader package to save time and ensure the CSV reading logic works as expected. There's no use spending a lot of time building and testing something that is already done to a high standard in public code libraries.

I opted to try and keep the project's controllers as thin as possible. This allows me to keep business logic and data access code where they can be re-used.

I chose to use a MySql Database due to how quickly and easily you can create the DB and get it up and running via docker image. It's also a Database technology I'm familiar with due to past experience within the business.

I'm using Podman to run the required containers as a license free alternative to docker.

I've also decided to use Entity Framework DB migration to automate DB table creation. This allows me to ensure there's no mismatch between the database and my DB contexts, not to mention saving time on DB setup.

I've also used Moq and AutoMoq for my unit tests to enable me to easily mock class dependencies in my unit tests.

I also decided to represent the Currency and Call Type columns as Enums to ensure consistency in the database and validate input.

# Assumptions Made

The first assumption I was required to make was related to fact that the "call type" column was missing from the example test data provided in the tech test, but was quite often mentioned in the requirements. As such, I added some hard-coding of the "call type" when mapping from the CSV file to the Database entities. This allowed me to implement the required functionality at a very small time cost.

The next assumption was the how the date filtering should work. I opted for a json body with StartDate and EndDate attributes, as adding dates as raw text to query string parameters would cause lots of issues.
# Application Setup

## Database

To setup the Database for the application, simply pull the mysql docker image (I'm using podman as it's a license free alternative):

`podman pull docker.io/library/mysql`

Once the image has been pulled, run the image with the following configuration:

`podman run -d --name mysql -e MYSQL_ROOT_PASSWORD=DbPw -e MYSQL_DATABASE=CdrDb -e MYSQL_USER=CdrUser -e MYSQL_PASSWORD=CdrPassword -p 3306:3306 mysql`

Once the DB container is up and running, you'll need to run the Entity Framework database migration. To do this, run the following in CMD from the root directory:

`dotnet ef database update --context CDRContext --project "src\CDR.Data" --startup-project "src\CDR.API"`

## Running the application

To run the application, imply:
- Open the solution in the IDE of your choice
- Ensure the CDR.API project is set at the startup project
- Run the project
Alternatively you can do the above by using dotnet cmd commands.

You should then be able to communicate with the API using the rest client of your choice, i.e. postman, bruno, etc.

To run the unit tests, either use the IDE of your choice to run them, or run `dotnet test` in the src directory.
# Future Considerations

Given more time, I would've liked to add more unit tests to ensure high code coverage within the project. 

I would've also liked to have used AutoMapper to handle some of the more 1-to-1 mapping functionality.

Additionally, if the project was taken further, I would've liked to expand the CDR search functionality to accept more parameters, and making existing ones optional to create more robust and flexible search functionality .

I would have also liked to have used the AutoFac package to speed up future dependency registration, as adding a new registration per newly added class can make the project balloon quite quickly.

I also would have liked to make the API output a bit more user friendly when reporting on Enums, i.e. converting them to their string/name values.