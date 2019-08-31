# epaulette-project
### Current as of 8/30/2019

This repository contains all the code necessary to launch the Epaulette application, including the web client, backend services, and database.

### Note:
Be sure to clone this repository into the *C:\Build* directory.

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

## epaulette-write-service
This contains the C# WebApi services responsible for consuming content from the web client for eventual viewing. To run, you will need: VS Code, DotNet Core 3 SDK (currently in preview), C# Omnisharp extension, Nuget Package Manager extension.

Necessary steps:
- dotnet restore
- dotnet build
- dotnet run