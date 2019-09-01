# epaulette-project
### Current as of 9/01/2019

This repository contains all the code necessary to launch the Epaulette application, including the web client, backend services, and database.

A breakdown of the subdirectories follows:

## epaulette-engine
This contains the React web client for the Epaulette application. To develop and run, you will need: VS Code, Node.js.

Necessary steps:
- npm i
- npm run build-dev
- npm run start

## epaulette-data
This contains the C# definition of all database objects, compiled into a standalone library. To run, you will need: VS Code, DotNet Core 3 SDK (currently in preview), C# Omnisharp extension, Nuget Package Manager extension.

Necessary steps:
- dotnet restore
- dotnet build

## epaulette-read-service
This contains the C# WebApi services responsible for serving up content for the web client to view. To run, you will need: VS Code, DotNet Core 3 SDK (currently in preview), C# Omnisharp extension, Nuget Package Manager extension.

Necessary steps:
- dotnet restore
- dotnet build
- dotnet run

## epaulette-service-lib
This contains the TypeScript wrapper used to make calls from the web client to the C# WebApi services. To run, you will need: VS Code, Node.js.

Necessary steps:
- npm i
- npm run build
- npm pack

## epaulette-write-service
This contains the C# WebApi services responsible for consuming content from the web client editor. To run, you will need: VS Code, DotNet Core 3 SDK (currently in preview), C# Omnisharp extension, Nuget Package Manager extension.

Necessary steps:
- dotnet restore
- dotnet build
- dotnet run

### Notes:
- Be sure to clone this repository into the *C:\Build* directory.
- Currently we are not publishing the epaulette-service-lib package. In order to reference this dependency, execute **npm pack** from within the epaulette-service-lib directory to build a local version.
- For first time setup, it may be necessary to execute **npm link typescript** from within the epaulette-engine directory, in order to successfully reference the epaulette-service-lib dependency.