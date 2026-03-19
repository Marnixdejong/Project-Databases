# Someren Camp Beheer

ASP.NET Core MVC applicatie voor het beheren van studenten, docenten en kamers voor Camp Someren.
Gebouwd met Entity Framework Core en Azure SQL Server.

## Vereisten

- .NET 8.0 SDK of hoger
- Azure SQL Server (of lokaal SQL Server)

## Installatie

1. Clone de repository
2. Zet je connection string in `src/SomerenWeb/appsettings.Development.json`
3. Draai het SQL-schema op de database: `sql/someren_sql_schema.sql`
4. Start de applicatie:

```bash
cd src/SomerenWeb
dotnet run
```

De applicatie draait standaard op `http://localhost:5000`.

## Structuur

```
src/SomerenWeb/   - de webapplicatie
sql/              - database schema
docs/             - ERD diagram
```

## Database

Het schema staat in `sql/someren_sql_schema.sql`. Draai dit script één keer op je SQL Server database. De tabellen en foreign keys worden aangemaakt, inclusief een voorbeeld-building bij de eerste start (alleen in Development).
