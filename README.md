Welcome to the PeopleApp by Emilio Nicoli developed in ASP.NET CORE
================

This app is currently incomplete and lacks the presentation layer inform of Empber data bindings and userfull unit tests.  

Key Features
------------

This app demonstrates a ASP.NET Core MVC application with database and Ember.js a implimentation.  However, due to the lack of time, this application remains unfinished :(

Build and Run
-------------

To build and run the sample, navigate to `src/PeopleApp` and run the following commands:

```
dotnet restore
dotnet build
dotnet run
```

`dotnet restore` installs all the dependencies.
`dotnet build` creates the assembly (or assemblies).
`dotnet run` runs the executable. 

To run the tests, navigate to the `tests/PeopleApp.Tests` directory and type the following commands:

```
dotnet restore
dotnet build
dotnet test
```

`dotnet test` runs all the configured tests.

You must run `dotnet restore` in the `src/PeopleApp` directory before you can run
the tests. `dotnet build` will follow the dependency and build both the library and unit
tests projects, but it will not restore NuGet packages.
