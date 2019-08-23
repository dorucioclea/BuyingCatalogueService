# BuyingCatalogueService - Service architecture for the NHS Digital Buying Catalogue
.Net Core application, based on a service architecture.

## IMPORTANT NOTES!
**You can use either the latest version of Visual Studio or .NET CLI for Windows, Mac and Linux**.

### Architecture overview
This application uses <b>.NET core</b> to provide an API capable of running on Linux or Windows.

> For the frontend web application see <a>https://github.com/nhs-digital-gp-it-futures/public-browse</a>.
> 
> For the data model see <a>https://github.com/nhs-digital-gp-it-futures/DataModel</a>

### Overview of the application code
This repo consists of one service to provide multiple resource endpoints for the NHS Digitial Buying Catalogue application using <b>.NET Core</b> and <b>Docker</a>.

The application is broken down into the following project libraries:

- API - Defines and exposes the available Buying Catalogue resources to the frontend
- Application - Provides the different use cases and functionality for the Buying Catalogue
- Domain - Defines the entities and business logic for the application
- Persistence Layer - Provides access and storage for the Buying Catalogue data

## Setting up your development environment for the Buying Catalogue

### Requirements
- .NET Core Version 2.2
- Docker
- Data Model repository

> Before you begin please install <b>Docker</b> on your machine.
> Also download and store the Buying Catalogue Data Model repository along side this repository.

<p>

To run the application in a container in development mode, double click the batch file <b>run-docker-compose-up-for-development.cmd</b>.

You can now access the API in your browser at 'http://localhost:8080/swagger/index.html'

> To stop and delete the application from running in a container, double click the batch file <b>run-docker-compose-down-for-development.cmd</b>.

</p>

