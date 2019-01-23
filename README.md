## Run PostgreSQL (Docker)

[![NuGet](https://img.shields.io/nuget/v/wk.RunPostgres.svg)](https://www.nuget.org/packages/wk.RunPostgres)
[![NuGet](https://img.shields.io/nuget/v/wk.RunMySql.svg)](https://www.nuget.org/packages/wk.RunMySql)

```bash
dotnet tool install -g wk.RunPostgres
wk-run-postgres
wk-run-postgres --user wk --password wk --port 5555
```

## Connection

```bash
Host=localhost; User Id=postgres; Password=1234; Database=postgres
Host=localhost; User Id=root; Password=1234; Database=mysql
```

## Build

```bash
dotnet cake build-postgres.cake -target=Install
dotnet cake build-mysql.cake -target=Install

dotnet cake build-postgres.cake -target-Publish-NuGet
dotnet cake build-mysql.cake -target=Publish-NuGet
```
