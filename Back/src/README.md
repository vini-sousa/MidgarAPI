# MidgarApp - Backend

This project is the backend of the **MidgarApp** application, built with **ASP.NET Core**.

## Development Server

To start the development server, run:

```bash
dotnet watch run
```

The application will be available at:

- HTTPS: `https://localhost:7204`
- HTTP: `http://localhost:5263`

Swagger UI will be automatically launched in your browser and is accessible at `https://localhost:7204/swagger`.

## Launch Settings

The project uses the `launchSettings.json` file, which defines the following profiles:

- **Midgar.API**: Runs the project locally with specified URLs and opens the Swagger UI.
- **IIS Express**: Runs the project using IIS Express, also with Swagger enabled.

## Running Unit Tests

To execute unit tests, use:

```bash
dotnet test
```

## Build

To build the project, run:

```bash
dotnet build
```

The compiled files will be located in the `bin/` directory.

## Publish

To publish the application in release mode, run:

```bash
dotnet publish -c Release -o ./publish
```

## Further Help

For more information about the .NET CLI, visit the official [.NET CLI documentation](https://docs.microsoft.com/aspnet/core/fundamentals/?view=aspnetcore-6.0).