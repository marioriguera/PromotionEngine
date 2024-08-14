# Explanation Technical test for software developers | SCRM Lidl International Hub

## Program
<p align="justify">
    I started with the starting point of the Web API, the Program file. I took the liberty of refactoring this file, grouping the necessary configurations in extension functions into other files for later use both in the use of the application and in testing.
</p>

## Coding behavior
<p align="justify">
    I have split the API and Application project configurations into their own folders within each of the projects, using similar names for familiarity. I try to maintain this behavior for the rest of the files and functionalities during development. Also to manage coding rules I have used the Nuget "StyleCop.Analyzer".
</p>

## Logs
<p align="justify">
    I have added the use of "Serilog" so that at any time text files with the extension ".log" can be saved on the computer's hard drive "C:" within a folder with the same name as the project. It has its own configuration where it is highlighted to always have the last seven log files, which must correspond to the last seven days of execution of the application.
</p>

## Documentation
<p align="justify">
  All classes, interfaces, constructors, properties and functions have been commented to enrich   the code documentation. If you are in the development environment, "Swagger" is activated to see  the available endpoints. I have configured the version change so that whoever interacts with   "Swagger" can easily identify the version of the endpoints. In addition, the comments of the  functions in the code will be taken as documentation of the endpoints in "Swagger".
</p>

## Endpoints request validation
<p align="justify">
    Using the "FluentValidation" Nugget I have managed to manage the validations of all the input parameters to the API endpoints. With this I can return messages to clarify the inconsistencies in the properties if they exist.
</p>

## Endpoints response validation
<p align="justify">
    Using the "ErrorOr" Nugget I have refactored the handling of API responses in case the response is not satisfactory. I have seen that you handle it in the code you have given me, although I must add that for other programmer profiles the way you have done it could be cumbersome, which is why I have used this Nugget.
</p>

## Dependency injection
<p align="justify">
    I have found dependencies of the "Handler" provided as an example that are instantiated and not automatically resolved, so I have decided to register them in the application's service container, so that these dependencies are automatically resolved in the constructor.
</p>

## Exceptions handler
<p align="justify">
    In my work experience I have encountered situations in which I have had to handle incitus exceptions for different reasons. So far I have not found the need to do so in this exercise, therefore I have decided to include a Middleware in the application that performs these functions.
</p>

## Mapping
<p align="justify">
    When using data transfer objects I have decided to carry the necessary information from the entities to other layers of the application or respond on my endpoints I have used the "AutoMapper" Nugget to configure and perform these transfers.
</p>

## Tests
<p align="justify">
    Using the split configurations of my "Api" I have developed a series of integration test simulations starting from the controller classes simulating real application behaviors. I have only had to create my own data group that would behave like the database.
</p>

---
> [!IMPORTANT]
> I have tried to maintain the "Vertical Slice" architecture, keeping as many features as possible close to each other and related to each other. Although those I have considered common have been moved to the corresponding folder.
---

## New NuGets packages used

- ErrorOr [NuGet](https://www.nuget.org/packages/ErrorOr), [GitHub](https://github.com/amantinband/error-or)
- FluentValidation [NuGet](https://www.nuget.org/packages/FluentValidation), [GitHub](https://github.com/FluentValidation/FluentValidation)
- FluentValidation.AspNetCore [NuGet](https://www.nuget.org/packages/FluentValidation.AspNetCore), [GitHub](https://github.com/FluentValidation/FluentValidation.AspNetCore)
- Serilog [NuGet](https://www.nuget.org/packages/Serilog), [GitHub](https://github.com/serilog/serilog)
- StyleCop.Analyzers [NuGet](https://www.nuget.org/packages/StyleCop.Analyzers), [GitHub](https://github.com/DotNetAnalyzers/StyleCopAnalyzers)
- AutoMapper [NuGet](https://www.nuget.org/packages/AutoMapper), [GitHub](https://github.com/AutoMapper/AutoMapper)

---
###### I would like to thank you for your time and for the challenge
###### Kind regards
###### Mario David Riguera Castillo, [LinkedIn](https://www.linkedin.com/in/mario-david-riguera-castillo/), [GitHub](https://github.com/marioriguera)
###### 👋😄
---
<p align="center">
  <a href="https://skillicons.dev">
    <img src="https://skillicons.dev/icons?i=cs,dotnet,visualstudio,git,github,docker" />
  </a>
</p>