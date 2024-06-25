# Speaking Roses Test
First of all, greetings and thank you very much for giving me the opportunity to satisfy your requirements through this test. I hope it is a success.
Regarding the necessary configurations and instructions for use, I will list them below:

# Setting
- Use Visual Studio to open the solution (SpeakingRosesTest.sln)
- Restore the database, TaskDB.bak, which is located in the SpeakingRosesTest/DatabaseBackups folder or use the database and table creation scripts saved in the SpeakingRosesTest/SQLScripts folder

# Instructions for use
- Start the web application from Visual Studio
- On the home page and in the nav bar there is an ENTER button that will take you to a control panel which lists 3 buttons: Tasks, Priorities and Statuses. They can click on them to go to consultation pages depending on the button clicked.
- All the tables created have 2 pages, a query page which is displayed first, and a page for inserting or updating data.

# General functionalities
- Use of the C# 12 language, .NET 8 framework for the structure and Blazor to display HTML pages with CSS
- Use of Bootstrap 5 for the layout in conjunction with a web template that I use a lot
- Repository pattern for access to the data layer
- Dependency injection implemented in Blazor pages and repository files
- Code-First or Database-First is not used because I use a code generator that does the task of creating the code and at the same time the tables in a single shot
- Caching to increase server response speed
- Services for data export and import
- Data pagination to reduce unnecessary data load
- Validation system for inserting or updating data
- Division of the system into areas to improve readability and increase development speed

# Added more features
These functionalities that I mark below are to compare my test with respect to the other tests of other candidates, I hope you like it. I'm going to list them:
- Mass import of data in each table with Excel files
- Mass export of data in each table in PDF, Excel and CSV formats
- Bulk actions like copied or full deleted
- Strict or lax search to give greater capacity when searching. For my part, the lax search seems very good to me.
- Built-in CMS but without security functions or integration of users, roles, permissions and pages.
- Use of a web template which I think is very nice, created by Creative-Tim, and called Material
- Fault logging system to explore them from the database
- Audit system for each table so that creation and modification dates and registration of users who create or edit can be recorded
- In addition to the requested table, Tasks, 2 more tables were added to store different states and priorities so that these assignments are not in hardcode.
- Data caching to optimize the response speed by the server
- Possibility of viewing the application with a dark theme

# Considerations to take into account
- Due to my intuition that you are looking for a developer with extensive experience, this first finished project is only done with C#, no JavaScript or AJAX communications, which I consider tedious next to implementing only C# and Blazor. But if you need to work purely with AJAX communication, no problem, give me a few more hours to implement communication with REST APIs and AJAX communication.
- Because there is no physically separated front-end and back-end, there are no drivers, so there is no testing on the APIs. But as I mentioned before, if you need drivers and see if testing is possible, I will gladly do the unit tests.

That's all, I wanted to submit the test as soon as possible so I decided to do it with what I know best, C# and Blazor. But if you need testing with AJAX calls, JavaScript and REST APIs, I make it no problem. I wait attentively for the result. Greetings
