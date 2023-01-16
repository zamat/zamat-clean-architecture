# zamat-clean-architecture

[![.NET build and tests](https://github.com/zamat/zamat-clean-architecture/actions/workflows/dotnet-build-and-tests.yml/badge.svg?branch=main)](https://github.com/zamat/zamat-clean-architecture/actions/workflows/dotnet-build-and-tests.yml)

<br/>

This is a solution template for creating a application following the principles of **Clean Architecture**. 

## Overview
<img align="left" src="https://raw.githubusercontent.com/zamat/zamat-clean-architecture/main/docs/clean-architecture-overview.png" />

## Give a Star
If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Technologies
* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core) (.net7)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/) (EFCore with PostgreSQL)
* [MassTransit](https://masstransit-project.com/) (MessageBroker, Integration Events, Outbox pattern)
* [OpenTelemetry](https://opentelemetry.io/) (observability (tracing, logging, metrics) with grafana backend)
* [Ocelot](https://ocelot.readthedocs.io/en/latest/) (Ocelot api gateway)
* [OpenApiReference](https://learn.microsoft.com/en-us/aspnet/core/web-api/microsoft.dotnet-openapi?view=aspnetcore-7.0) (Sample web api client based on OpenAPI tool which can be distributed via nuget packages)

## Design patterns & Architectural principals
* [UnitOfWork](https://martinfowler.com/eaaCatalog/unitOfWork.html) (Using UnitOfWork pattern )
* [Outbox pattern](https://microservices.io/patterns/data/transactional-outbox.html) (Outbox pattern, Using save to db and publish integration event in one transaction )

## Getting Started
To use this template, there are a few options:
- Create a new project based on this template by clicking the above **Use this template** .
- Clone solution and install using `dotnet new` <br/>
`git clone https://github.com/zamat/zamat-clean-architecture ./workdir` <br/>
`cd workdir` <br/>
`dotnet new install .` <br/>
`dotnet new zamat-clean-architecture -n Your.Namespace`

## Versions
The master branch is now using .NET 7

## License
This project is licensed with the [MIT license](LICENSE).
