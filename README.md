# AspCoreTestbed  

### Next steps (TBD):
Completed: EF Core (DB frist) setup, ASP Core, Repository / UOW pattern, MS Test setup.   
Todo: MVC with Anguar / JQuery front end, MVC routing (explicit) , IOC/DI for testing / logging 
 

This project is used to test various ASP .NET code related stuff, including:  
* Playground for ASP .NET core / MVC
* Entity Framework Core (i.e., EF 7.0)
* Repository / UnitOfWork pattern (both named and generic repositories)

Problem:  Movie Buff, that is movies, movie chatacters and quotes.  

## Project Structure
MovieBuff.Service - the MVC project  
MovieBuff.DAL - the data access layer containing the repositories
MobiveBuff.DAL.Test - the test project for MovieBuff DAL.

## VS Code project / solution related notes

To create a solution:   
> cd to directory 
> dotnet new sln

## Add EntityFramework

Note: needed to upgrade project to targt .netcoreapp2.0

> dotnet add package Microsoft.EntityFrameworkCore.SqlServer 

To use the EF cli tools:
> dotnet add package Microsoft.EntityFrameworkCore.Tools.Dotnet
> dotnet add package Microsoft.EntityFrameworkCore.SqlServer.Design
Note: Since Microsoft.EntityFrameworkCore.SqlServer.Design version 2.0.0 was not 
available, using Microsoft.EntityFrameworkCore.Design instead. (Also, could use 
version 2.0.0-preview1-final.)
 

NOTE: ...Tools.Dotnet needs to be added as a cli reference, but that may not be
the case. IF so, will need to manually add/move it.
<ItemGroup>
    <DotNetCliToolReference Include="Microsoft.EntityFrameworkCore.Tools.DotNet" Version="2.0.0" />
</ItemGroup>

To test EF tools:
> dotnet ef -h

To Generate Scaffolding:
>  dotnet ef dbcontext scaffold "Server=dbname;user id=userid;password=pw;Database=dbname;" Microsoft.EntityFrameworkCore.SqlServer -o model-folder-name
(Note: if using NuGet PM> Scaffold-DbContext ...)

### Extending / working with EF model
Every time you regenerate the EG generated objects, including the MyAppContext object gets 
regenerated. (Note, you have to use the -f option to force over-writing files). (As such
you will need to re-do any inline make to the generated files. See note below.)

As all generated classes are partial classes, use another file (e.g., GenClass.Custom.cs) to
extend the generated classes. This will ensure extensions do not get clobbered. 



To configure Core object ...

To use configuration file ... read settings file and initialize EF, for example:
Add MS extension packages for file confiuration
> dotnet add package Microsoft.Extensions.Configuration 
> dotnet add package Microsoft.Extensions.Configuration.Json
... and initialize EF. That is add the follwoing to the AppContext initialization: 
var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");

IConfigurationRoot Configuration = builder.Build();
optionsBuilder.UseSqlServer(Configuration["ConnectionStrings:MovieBuffDatabase"]);


## useful commands
To add/remove a project to a solution:
> dotnet sln add/remove rel-path\proj.csproj


## Related reading / material

EF documentation home: https://docs.microsoft.com/en-us/ef/#pivot=efcore
EF Core 2.0 quick overview: https://docs.microsoft.com/en-us/ef/core/  

Repository / UOW pattern: https://docs.microsoft.com/en-us/aspnet/mvc/overview/older-versions/getting-started-with-ef-5-using-mvc-4/implementing-the-repository-and-unit-of-work-patterns-in-an-asp-net-mvc-application
.NET CLI for EF: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
http://www.learnentityframeworkcore.com/walkthroughs/existing-database  

MVC:
https://docs.microsoft.com/en-us/aspnet/core/data/ef-mvc/intro
Razor:
https://docs.microsoft.com/en-us/aspnet/core/mvc/views/razor
https://media.readthedocs.org/pdf/mvc/latest/mvc.pdf
ASP partial page updates: https://docs.microsoft.com/en-us/aspnet/web-forms/overview/older-versions-getting-started/aspnet-ajax/understanding-partial-page-updates-with-asp-net-ajax  

Configuration:
https://docs.microsoft.com/en-us/aspnet/core/fundamentals/configuration
https://github.com/aspnet/Docs/blob/master/aspnetcore/fundamentals/configuration/sample/src/ConfigJson/Program.cs
https://docs.microsoft.com/en-us/ef/core/miscellaneous/connection-strings

(Unit) Testing: 3 good options (maybe more): MSTest, Nunit, xUnit
http://asp.net-hacker.rocks/2017/03/31/unit-testing-with-dotnetcore.html

Other:
Working with multiple projects: https://docs.microsoft.com/en-us/dotnet/core/tutorials/libraries#how-to-use-multiple-projects


