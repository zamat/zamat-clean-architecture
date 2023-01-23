# zamat-clean-architecture

[![.NET build and tests](https://github.com/zamat/zamat-clean-architecture/actions/workflows/dotnet-build-and-tests.yml/badge.svg?branch=main)](https://github.com/zamat/zamat-clean-architecture/actions/workflows/dotnet-build-and-tests.yml)

<br/>

This is a solution template for creating a application following the principles of **Clean Architecture**. 

## Overview
<img align="left" src="https://raw.githubusercontent.com/zamat/zamat-clean-architecture/main/docs/clean-architecture-overview.png" />

## Solution view
<img align="left" src="https://raw.githubusercontent.com/zamat/zamat-clean-architecture/main/docs/logical-view.png" />

## Give a Star
If you like or are using this project to learn or start your solution, please give it a star. Thanks!

## Projects list

|Project|Description|
|:------|:-- |
|EFCore.PostgreSQL|Contains PostgreSQL db migrations (Only referenced by Cli tool).|
|Zamat.Sample.ApiGateway|ApiGateway based on Ocelot library.|
|Zamat.Sample.BuildingBlocks.Core|Common core building blocks for the entire solution.|
|Zamat.Sample.BuildingBlocks.Infrastructure|Common infrastructure building blocks for the entire solution.|
|Zamat.Sample.Cli|Cli tools. In Kubernetes environment can be used as db migration init container.|
|Zamat.Sample.Services.Audit.Worker|Sample Worker service. Consuming integration events published by User Service API.|
|Zamat.Sample.Services.Users.Api.Grpc|Sample Grpc Api service|
|Zamat.Sample.Services.Users.Api.Grpc.Client|Standalone GRPC client package. Contains .proto definition. Can be distributed as nuget package.|
|Zamat.Sample.Services.Users.Api.Rest|Sample REST Api service|
|Zamat.Sample.Services.Users.Api.Rest.Client|Standalone REST api client package based on NSwag code generator. Can be distributed as nuget package.|
|Zamat.Sample.Services.Users.Core|Application Core package. Contains CQRS handlers.|
|Zamat.Sample.Services.Users.Core.Domain|Domain model package. Contains business logic.|
|Zamat.Sample.Services.Users.Core.IntegrationEvents|Integration events package. Should be distributed as nuget package.|
|Zamat.Sample.Services.Users.Core.Infrastructure|Infrastructure package. Contains EFCore dbContext, application services implementation and message broker configuration.|

## Technologies
* [ASP.NET Core 7](https://docs.microsoft.com/en-us/aspnet/core/introduction-to-aspnet-core) (.net7)
* [Entity Framework Core 7](https://docs.microsoft.com/en-us/ef/core/) (EFCore with PostgreSQL)
* [MassTransit](https://masstransit-project.com/) (MessageBroker, Integration Events, Outbox pattern)
* [OpenTelemetry](https://opentelemetry.io/) (observability (tracing, logging, metrics) with grafana backend)
* [Ocelot](https://ocelot.readthedocs.io/en/latest/) (Ocelot api gateway)
* [OpenApiReference](https://learn.microsoft.com/en-us/aspnet/core/web-api/microsoft.dotnet-openapi?view=aspnetcore-7.0) (Sample web api client based on OpenAPI tool which can be distributed via nuget packages)

## Architectural principles
* [Clean Architecture](https://blog.cleancoder.com/uncle-bob/2012/08/13/the-clean-architecture.html) (Clean Architecture application style)
* [Domain Driven Design](https://learn.microsoft.com/en-us/azure/architecture/microservices/model/tactical-ddd) (Tactical DDD patterns)
* [CQRS](https://martinfowler.com/bliki/CQRS.html) (Separate commands from queries with dedicated bus)

## Design patterns 
* [UnitOfWork](https://martinfowler.com/eaaCatalog/unitOfWork.html) (UnitOfWork pattern with Repository pattern)
* [Outbox pattern](https://microservices.io/patterns/data/transactional-outbox.html) (EFCore SaveChanges and publish integration event in one transaction)

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
