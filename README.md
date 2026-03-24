# Someren Camp

ASP.NET Core MVC applicatie voor het beheren van studenten, docenten en kamers voor Camp Someren.

## Hoe te starten

1. Zet je connection string in `src/SomerenWeb/appsettings.Development.json`
2. Draai het SQL-schema: `sql/someren_sql_schema.sql`
3. Start de applicatie:

```bash
cd src/SomerenWeb
dotnet run
```

## Structuur

- `src/SomerenWeb/` - de webapplicatie
- `sql/` - database schema met testdata
- `docs/` - ERD en relationeel model
