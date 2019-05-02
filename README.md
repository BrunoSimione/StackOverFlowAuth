# StackOverFlow

This project is a study about ASP.Net MVC, Entity Framework, and Auth control. The idea is to simulate a Q/A website similar as StackOverFlow.

### Technologies
- ASP.NET MVC
- Entity Framework
- Razor, .NET, HTML
- Auth Control

### Extra Info
**The project has Seeds to both Contexts (Data and Login/Security). Please, run the following commands before execute the project:**

- Enable-Migrations -ContextTypeName ApplicationDbContext -MigrationsDirectory Migrations\ApplicationDbContext
- Enable-Migrations -ContextTypeName QuestionAnswerContext -MigrationsDirectory Migrations\QuestionAnswerContext

- Add-Migration -ConfigurationTypeName StackOverFlowAuth.Migrations.ApplicationDbContext.Configuration "InitialDatabaseCreation"
- Add-Migration -ConfigurationTypeName StackOverFlowAuth.Migrations.QuestionAnswerContext.Configuration "InitialDatabaseCreation"

- Update-Database -ConfigurationTypeName StackOverFlowAuth.Migrations.ApplicationDbContext.Configuration
- Update-Database -ConfigurationTypeName StackOverFlowAuth.Migrations.QuestionAnswerContext.Configuration

**Already created users:**
- Username/Email: john@john.com Password: Aa123456* Role: Admin
- Username/Email: mary@mary.com Password: Aa123456* Role: User
- Username/Email: mark@mark.com Password: Aa123456* Role: User
