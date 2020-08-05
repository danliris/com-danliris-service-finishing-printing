# com-danliris-service-finishing-printing

[![codecov](https://codecov.io/gh/danliris/com-danliris-service-finishing-printing/branch/dev/graph/badge.svg)](https://codecov.io/gh/danliris/com-danliris-service-finishing-printing) [![Build Status](https://travis-ci.com/danliris/com-danliris-service-finishing-printing.svg?branch=dev)](https://travis-ci.com/danliris/com-danliris-service-finishing-printing) [![Maintainability](https://api.codeclimate.com/v1/badges/dd8f946180b0971daf85/maintainability)](https://codeclimate.com/github/danliris/com-danliris-service-finishing-printing/maintainability)

DanLiris Application is a enterprise project that aims to manage the business processes of a textile factory, PT. DanLiris.
This application is a microservices application consisting of services based on .NET Core and Aurelia Js which part of  NodeJS Frontend Framework. This application show how to implement microservice architecture principles. com-danliris-service-finishing-printing repository is part of service that will serve  finishing printing business activity.

## Prerequisites
* Windows, Mac or Linux
* [Visual Studio Code](https://code.visualstudio.com/) or [Visual Studio](https://visualstudio.microsoft.com/vs/whatsnew/)
* [IIS Web Server](https://www.iis.net/) 
* [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [.NET Core SDK](https://www.microsoft.com/net/download/core#/current) (v2.0.9,  SDK 2.1.202, ASP.NET Core Runtime 2.0.9 )


## Getting Started

- - Fork the repository and then clone the repository using command  `git clone https://github/YOUR-USERNAME/com-danliris-service-finishing-printing.git`  checkout the `dev` branch.


### Command Line

- Install the latest version of the .NET Core SDK from this page <https://www.microsoft.com/net/download/core>
- Next, navigate to root project or wherever your folder is on the commandline in Administrator mode.
- Create empty database.
- Setting connection to database using Connection Strings in appsettings.json. Your appsettings.json look like this:

```
{
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  },

  "ConnectionStrings": {
    "DefaultConnection": "Server=YourDbServer;Database=your_parent_database;Trusted_Connection=True;MultipleActiveResultSets=true",
  },
  "ClientId": "your ClientId",
  "Secret": "Your Secret",
  "ASPNETCORE_ENVIRONMENT": "Development"
}
```
and  Your appsettings.Developtment.json look like this :
```
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```
- Make sure port application has no conflict, setting port application in launchSettings.json
```
Com.Danliris.Service.Production.WebApi 
 ┗ Properties
   ┗ launchSettings.json
```

file launchSettings.json look like this :
```
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:6102/",
      "sslPort": 0
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Com.Danliris.Service.Finishing.Printing.WebApi": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

- Call `dotnet run`.
- Then open the `http://localhost:6102/swagger/index.html` URL in your browser.

### Visual Studio

- Download Visual Studio 2019 (any edition) from https://www.visualstudio.com/downloads/ .
- Open `Com.Danliris.Service.Finishing.Printing.sln` and wait for Visual Studio to restore all Nuget packages.
- Create empty database.
- Setting connection to database using Connection Strings in appsettings.json. Your appsettings.json look like this:

```
{
  "Logging": {
    "IncludeScopes": false,
    "Debug": {
      "LogLevel": {
        "Default": "Warning"
      }
    },
    "Console": {
      "LogLevel": {
        "Default": "Warning"
      }
    }
  },

  "ConnectionStrings": {
    "DefaultConnection": "Server=YourDbServer;Database=your_parent_database;Trusted_Connection=True;MultipleActiveResultSets=true",
  },
  "ClientId": "your ClientId",
  "Secret": "Your Secret",
  "ASPNETCORE_ENVIRONMENT": "Development"
}
```

and  Your appsettings.Developtment.json look like this :
```
{
  "Logging": {
    "IncludeScopes": false,
    "LogLevel": {
      "Default": "Debug",
      "System": "Information",
      "Microsoft": "Information"
    }
  }
}
```

- Make sure port application has no conflict, setting port application in launchSettings.json.
```
Com.Danliris.Service.Production.WebApi 
 ┗ Properties
   ┗ launchSettings.json
```

file launchSettings.json look like this :
```
{
  "iisSettings": {
    "windowsAuthentication": false,
    "anonymousAuthentication": true,
    "iisExpress": {
      "applicationUrl": "http://localhost:6102/",
      "sslPort": 0
    }
  },
  "profiles": {
    "IIS Express": {
      "commandName": "IISExpress",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      }
    },
    "Com.Danliris.Service.Finishing.Printing.WebApi": {
      "commandName": "Project",
      "launchBrowser": true,
      "environmentVariables": {
        "ASPNETCORE_ENVIRONMENT": "Development"
      },
      "applicationUrl": "http://localhost:5000"
    }
  }
}
```

- Ensure `Com.Danliris.Service.Production.WebApi` is the startup project and run it and the browser will launched in new tab `http://localhost:6102/swagger/index.html`


### Run Unit Tests in Visual Studio 
1. You can run all test suite, specific test suite or specific test case on test explorer.
2. Choose Tab Menu **Test** to select differnt menu test.
3. Select **Run All Test** or press (Ctrl + R, A ) to run all test suite.
4. Select **Test Explorer** or press (Ctrl + E, T ) to determine  test suite to run specifically.
5. Select **Analyze Code Coverage For All Test** to generate code coverage. 


## Knows More Details
### Root directory and description

```
com-danliris-service-finishing-printing
 ┣ Com.Danliris.Service.Production.Lib
 ┣ Com.Danliris.Service.Production.Test
 ┣ Com.Danliris.Service.Production.WebApi
 ┣ TestResults
 ┣ .codecov.yml
 ┣ .gitignore
 ┣ .travis.yml
 ┣ Com.Danliris.Service.Finishing.Printing.sln
 ┗ README.md
 ```

**1. Com.Danliris.Service.Production.Lib**

This folder consists of various libraries, domain Models, View Models, and Business Logic.The Model and View Models represents the data structure. Business Logic has responsibility  to organize, prepare, manipulate, and organize data. The tasks are include entering data into databases, updating data, deleting data, and so on. The model carries out its work based on instructions from the controller.


AutoMapperProfiles:

- Colecction class to setup mapping data 

BusinessLogic

- Colecction of classes to prepare, manipulate, and organize data, including CRUD (Create, Read, Update, Delete ) on database.

Models:

- The Model is a collection of objects that Representation of data structure which hold the application data and it may contain the associated business logic.

ViewModels

- The View Model refers to the objects which hold the data that needs to be shown to the user.The View Model is related to the presentation layer of our application. They are defined based on how the data is presented to the user rather than how they are stored.

ModelConfigs

- Collection of classes to setup entity model  that will be used in EF framework to generate table

Migrations

- Collection of classes that generated by EF framework  to setup database and the tables.


PdfTemplates

- Collection of classes to generate report in pdf format.

Services

- Collection of classes and interfaces to validation and authentication user.

Helpers 

- Collection of helper classes that frequently used in various cases. 


Utilities

- Collection of classes that frequently used as utility in various class. 


The folder tree in this folder is:

```
com-danliris-service-finishing-printing
 ┣ Com.Danliris.Service.Production.Lib
 ┃ ┣ AutoMapperProfiles
 ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┣ DailyOperation
 ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┣ Kanban
 ┃ ┃ ┣ Master
 ┃ ┃ ┣ MonitoringEvent
 ┃ ┃ ┣ MonitoringSpecificationMachine
 ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┣ Packing
 ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┗ StrikeOff
 ┃ ┣ bin
 ┃ ┃ ┗ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┣ BusinessLogic
 ┃ ┃ ┣ Facades
 ┃ ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┃ ┣ CostCalculation
 ┃ ┃ ┃ ┣ DailyOperation
 ┃ ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┃ ┣ Kanban
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ MonitoringEvent
 ┃ ┃ ┃ ┣ MonitoringSpecificationMachine
 ┃ ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┃ ┣ OrderStatusReport
 ┃ ┃ ┃ ┣ Packing
 ┃ ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┃ ┗ StrikeOff
 ┃ ┃ ┣ Implementations
 ┃ ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┃ ┣ DailyOperation
 ┃ ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┃ ┣ Kanban
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┃ ┣ BadOutput
 ┃ ┃ ┃ ┃ ┣ DirectLaborCost
 ┃ ┃ ┃ ┃ ┣ DurationEstimation
 ┃ ┃ ┃ ┃ ┣ Instruction
 ┃ ┃ ┃ ┃ ┣ Machine
 ┃ ┃ ┃ ┃ ┣ MachineType
 ┃ ┃ ┃ ┃ ┣ OperationalCost
 ┃ ┃ ┃ ┃ ┗ Step
 ┃ ┃ ┃ ┣ MonitoringEvent
 ┃ ┃ ┃ ┣ MonitoringSpecificationMachine
 ┃ ┃ ┃ ┣ Packing
 ┃ ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┃ ┗ StrikeOff
 ┃ ┃ ┗ Interfaces
 ┃ ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┃ ┣ DailyOperation
 ┃ ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┃ ┣ Kanban
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ MonitoringEvent
 ┃ ┃ ┃ ┣ MonitoringSpecificationMachine
 ┃ ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┃ ┣ OrderStatusReport
 ┃ ┃ ┃ ┣ Packing
 ┃ ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┃ ┗ StrikeOff
 ┃ ┣ Enums
 ┃ ┣ Helpers
 ┃ ┣ Migrations
 ┃ ┣ ModelConfigs
 ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┣ Kanban
 ┃ ┃ ┗ Master
 ┃ ┃ ┃ ┣ DurationEstimation
 ┃ ┃ ┃ ┣ Instruction
 ┃ ┃ ┃ ┗ Step
 ┃ ┣ Models
 ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┣ CostCalculation
 ┃ ┃ ┣ Daily_Operation
 ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┣ Kanban
 ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ BadOutput
 ┃ ┃ ┃ ┣ DirectLaborCost
 ┃ ┃ ┃ ┣ DurationEstimation
 ┃ ┃ ┃ ┣ Instruction
 ┃ ┃ ┃ ┣ Machine
 ┃ ┃ ┃ ┣ MachineType
 ┃ ┃ ┃ ┣ OperationalCost
 ┃ ┃ ┃ ┗ Step
 ┃ ┃ ┣ Monitoring_Event
 ┃ ┃ ┣ Monitoring_Specification_Machine
 ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┣ Packing
 ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┗ StrikeOff
 ┃ ┣ obj
 ┃ ┃ ┣ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┣ PdfTemplates
 ┃ ┣ Services
 ┃ ┃ ┣ HttpClientService
 ┃ ┃ ┣ IdentityService
 ┃ ┃ ┗ ValidateService
 ┃ ┣ Utilities
 ┃ ┃ ┣ BaseClass
 ┃ ┃ ┣ BaseInterface
 ┃ ┣ ViewModels
 ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┣ CostCalculation
 ┃ ┃ ┣ Daily_Operation
 ┃ ┃ ┣ DyestuffChemicalUsageReceipt
 ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┣ Integration
 ┃ ┃ ┃ ┣ Inventory
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┗ Sales
 ┃ ┃ ┃ ┃ ┣ DOSales
 ┃ ┃ ┃ ┃ ┗ FinishingPrinting
 ┃ ┃ ┣ Kanban
 ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ BadOutput
 ┃ ┃ ┃ ┣ DirectLaborCost
 ┃ ┃ ┃ ┣ DurationEstimation
 ┃ ┃ ┃ ┣ Instruction
 ┃ ┃ ┃ ┣ Machine
 ┃ ┃ ┃ ┣ MachineType
 ┃ ┃ ┃ ┣ OperationalCost
 ┃ ┃ ┃ ┗ Step
 ┃ ┃ ┣ Monitoring_Event
 ┃ ┃ ┣ Monitoring_Specification_Machine
 ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┣ OrderStatusReports
 ┃ ┃ ┣ Packing
 ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┗ StrikeOff
 ┃ ┣ Com.Danliris.Service.Finishing.Printing.Lib.csproj
 ┃ ┣ Com.Danliris.Service.Finishing.Printing.Lib.csproj.user
 ┃ ┣ FinishingPrintingLibClassDiagram.cd
 ┃ ┗ ProductionDbContext.cs

 ```


**2. Com.Danliris.Service.Production.WebApi**

This folder consists of Controller API. The controller has responsibility to processing data and  HTTP requests and then send it to a web page. 

The folder tree in this folder is:

```
 ┣ Com.Danliris.Service.Production.WebApi
 ┃ ┣ bin
 ┃ ┃ ┗ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┃ ┃ ┃ ┣ Properties
 ┃ ┣ Controllers
 ┃ ┃ ┗ v1
 ┃ ┃ ┃ ┣ ColorReceipt
 ┃ ┃ ┃ ┣ CostCalculation
 ┃ ┃ ┃ ┣ DailyOperation
 ┃ ┃ ┃ ┣ DyestuffChemicalReceiptUsage
 ┃ ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┃ ┣ Kanban
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ MonitoringEvent
 ┃ ┃ ┃ ┣ MonitoringSpecificationMachine
 ┃ ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┃ ┣ OrderStatusReport
 ┃ ┃ ┃ ┣ Packing
 ┃ ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┃ ┗ StrikeOff
 ┃ ┣ obj
 ┃ ┃ ┣ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┣ Properties
 ┃ ┣ Utilities
 ```

**3. Com.Danliris.Service.Production.Test**

This folder is collection of classes to run code testing. The code testing used in this app is  a unit test using libraries of moq and xunit.

The folder tree in this folder is:

```
┣ Com.Danliris.Service.Production.Test
 ┃ ┣ bin
 ┃ ┃ ┗ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┃ ┃ ┃ ┣ Properties
 ┃ ┣ Controllers
 ┃ ┣ DataUtils
 ┃ ┃ ┣ MasterDataUtils
 ┃ ┣ Facades
 ┃ ┣ Helpers
 ┃ ┣ obj
 ┃ ┃ ┣ Debug
 ┃ ┃ ┃ ┗ netcoreapp2.0
 ┃ ┣ Services
 ┃ ┃ ┣ Master
 ┃ ┃ ┣ OrderStatusReportServiceTest
 ┃ ┣ Utils
 ┃ ┣ ViewModels
 ┃ ┃ ┣ Daily_Operation
 ┃ ┃ ┣ FabricQualityControl
 ┃ ┃ ┣ Integration
 ┃ ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┗ Sales
 ┃ ┃ ┃ ┃ ┗ FinishingPrinting
 ┃ ┃ ┣ Kanban
 ┃ ┃ ┣ Master
 ┃ ┃ ┃ ┣ BadOutput
 ┃ ┃ ┃ ┣ DirectLaborCost
 ┃ ┃ ┃ ┣ DurationEstimation
 ┃ ┃ ┃ ┣ Instruction
 ┃ ┃ ┃ ┣ Machine
 ┃ ┃ ┃ ┣ MachineType
 ┃ ┃ ┃ ┣ OperationalCost
 ┃ ┃ ┃ ┗ Step
 ┃ ┃ ┣ Monitoring_Event
 ┃ ┃ ┣ Monitoring_Specification_Machine
 ┃ ┃ ┣ NewShipmentDocument
 ┃ ┃ ┣ Packing
 ┃ ┃ ┣ PackingReceipt
 ┃ ┃ ┣ ReturToQC
 ┃ ┃ ┣ ShipmentDocument
 ┃ ┃ ┗ StrikeOff
 ┃ ┣ Com.Danliris.Service.Finishing.Printing.Test.csproj
 ┃ ┗ Com.Danliris.Service.Finishing.Printing.Test.csproj.user
```

**TestResults**

- Collections of files generated by the system for purposes of unit test code coverage.

**ProductionDbContext.cs**

This file contain context class that derives from DbContext in entity framework. DbContext is an important class in Entity Framework API. It is a bridge between domain or entity classes and the database. DbContext and context class  is the primary class that is responsible for interacting with the database.


**File Program.cs**

Important class that contains the entry point to the application. The file has the Main() method used to run the application and it is used to create an instance of WebHostBuilder for creating a host for the application. The Startup class to be used by the application is specified in the Main method.

**File Startup.cs**

This file contains Startup class. The Startup class configures services and the app's request pipeline.Optionally includes a ConfigureServices method to configure the app's services. A service is a reusable component that provides app functionality. Services are registered in ConfigureServices and consumed across the app via dependency injection (DI) or ApplicationServices.This class also Includes a Configure method to create the app's request processing pipeline.

**File .travis.yml**

Travis CI is configured by adding a file named .travis.yml. This file in a YAML format text file, located in root directory of the repository. This file specifies the programming language used, the desired building and testing environment (including dependencies which must be installed before the software can be built and tested), and various other parameters

**File .codecov.yml**

This file is used to configure code coverage in unit tests.


**Com.Danliris.Service.Finishing.Printing.sln**

File .sln is extention for *solution* aka file solution for .Net Core, this file is used to manage all project by code editor.

 ### Validation
Data validation using **IValidatableObject**