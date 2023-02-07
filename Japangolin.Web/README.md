# Japangolin web development
Japangolin server is configured so that:
- HTTPS on port 7443
- HTTP on port 7080 (though basically useless)

The following commands assume:
- working directory is `Japangolin/Japangolin.Web/`
- using PowerShell

This guide covers how to run backend and frontend projects, both locally and in Docker containers.

## Dev certificates
Self-signed certificates enable HTTPS use in development, and makes backend code much easier to work with.
Docker environments will not work without them.
Local environments technically can work without them, but they reduce warnings from the browser when attempting to access Swagger documentation - might as well use them.
Ideally, replace `<PASSWORD>` with an actual password whenever it appears.

```shell
dotnet dev-certs https -ep .\Backend\https\aspnetapp.pfx -p "<PASSWORD>"
dotnet dev-certs https --trust
```

## Backend
The server application can be run as either locally or within a Docker container. Swagger is available in development environments.

### Run locally
1. Update the [_Japangolin.Web.Backend_ launch profile](Backend/Properties/launchSettings.json): environment variable `ASPNETCORE_Kestrel__Certificates__Default__Password` = `<PASSWORD>`.
2. Run the backend project using that profile. Typically done via IDE, but to run as a terminal command:
    ```shell
    dotnet run --project .\Backend\Japangolin.Web.Backend.csproj --launch-profile Japangolin.Web.Backend
    ```
3. Navigate to https://localhost:7443/swagger/index.html to interact with the server API (browser may complain about invalid certificate authority if dev certs not setup or trusted).

### Run as Docker container
1. Build the [Docker image](Backend/Dockerfile), which builds and publishes the backend project in a Docker environment. Note that the docker build context (`..`) is the root of the repository, so that the docker image can replicate the solution folder structure and access dependencies such as `Japangolin.Core`.
    ```shell
    docker build -t japangolin-backend -f .\Backend\Dockerfile ..
    ```
2. Run a container from the Docker image. Here the `Backend\https` folder containing the dev cert is mounted as a volume to avoid directly copying the .pfx file. Remember to update `<PASSWORD>` as appropriate.
    ```shell
    docker run --rm -p 7080:80 -p 7443:443 -v ${pwd}\Backend\https:/https/ -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_Kestrel__Certificates__Default__Password="<PASSWORD>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -e ASPNETCORE_ENVIRONMENT="Development" japangolin-backend
    ```
3. Navigate to https://localhost:7443/swagger/index.html to interact with the server API.

## Frontend
The client application can be run in its own development server.
Currently a backend server is required to show data.
_(Eventually there will be the option of developing against the frontend with dummy data...)_

1. Start the development server.
    ```shell
    npm start --prefix .\Frontend\
    ```
2. Navigate to http://localhost:3000 to interact with the client-side application.

## Full Stack Production Demo
Sometimes it's useful to be see how the entire system might behave in a production environment, or perform a dry run of a deployment.
As with the backend, this can be done locally or in a Docker container.

### Run locally
1. Publish the backend so that it is production ready.
   ```shell
   dotnet publish ".\Backend\Japangolin.Web.Backend.csproj" -c Release -o .\publish
   ```
2. Build the frontend so that it is production ready and move it to `\wwwroot` inside the publish folder
   ```shell
   npm run build --prefix .\Frontend\
   mv .\Frontend\build\ .\publish\wwwroot\
   ```
3. Run the server. (Explicit content root is required when not executing from the directory containing the .exe)
   ```shell
   .\publish\Wacton.Japangolin.Web.Backend.exe --contentRoot .
   ```
4. Navigate to https://localhost:5001 to use the application. (Alternatively, run the server with `--urls "https://localhost:7443;http://localhost:7080"` to use the same ports as used in development)

### Run as Docker container
1. Build the [Docker image](Dockerfile), which builds and publishes the fullstack project in a Docker environment. Note that the docker build context (`..`) is the root of the repository, so that the docker image can replicate the solution folder structure and access dependencies such as `Japangolin.Core`.
    ```shell
    docker build -t japangolin-fullstack -f .\Dockerfile ..
    ```
2. Run a container from the Docker image. Here the `Backend\https` folder containing the dev cert is mounted as a volume to avoid directly copying the .pfx file. Remember to update `<PASSWORD>` as appropriate.
    ```shell
    docker run --rm -p 7080:80 -p 7443:443 -v ${pwd}\Backend\https:/https/ -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_Kestrel__Certificates__Default__Password="<PASSWORD>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx japangolin-fullstack
    ```
3. Navigate to https://localhost:7443 to use the application.
---

Handy resources:
- https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs
- https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0
- https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis
