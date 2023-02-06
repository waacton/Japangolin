# Japangolin web development
Japangolin server is configured so that:
- HTTPS on port 7443
- HTTP on port 7080 (though basically useless)

The following commands assume:
- working directory is`/Japangolin/Japangolin.Web/`
- using PowerShell

## Backend
The server application can be run as either locally or within a Docker container. Swagger is available in development environments.

### Dev certificates
Dev certificates are self-signed certificates that enable HTTPS use in development, and are required for the Docker approach.
Technically they are not required when running locally but the browser will show warnings when attempting to access Swagger, so we may as well use them.
Ideally, replace `<PASSWORD>` with an actual password whenever it appears.

```shell
dotnet dev-certs https -ep .\Backend\https\aspnetapp.pfx -p "<PASSWORD>"
dotnet dev-certs https --trust
```

### Run locally
1. Update the `Japangolin.Web.Backend` [launch profile](Backend/Properties/launchSettings.json) so that the environment variable `ASPNETCORE_Kestrel__Certificates__Default__Password` contains the dev cert `<PASSWORD>`.
2. Run the backend project using the `Japangolin.Web.Backend` [launch profile](Backend/Properties/launchSettings.json). Typically done via IDE, but to run as a terminal command:
```shell
dotnet run --project .\Backend\Japangolin.Web.Backend.csproj --launch-profile Japangolin.Web.Backend
```
3. Navigate to [`https://localhost:7443/swagger/index.html`]() to interact with the API (browser may complain about invalid certificate authority if dev certs not setup or trusted).

### Run as Docker container
1. Build the [Docker image](Backend/Dockerfile), which builds and publishes the backend project in a Docker environment. Note that the docker build context (`..`) references the root of the repository, so that the docker image can replicate the solution folder structure and access dependencies such as `Japangolin.Core`.
```shell
docker build -t japangolin-backend -f .\Backend\Dockerfile ..
```
2. Run a container from the Docker image. Here the `\Backend\https\` folder containing the dev cert is mounted as a volume to avoid directly copying the .pfx file.
```shell
docker run --rm -p 7080:80 -p 7443:443 -v ${pwd}\Backend\https:/https/ -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_Kestrel__Certificates__Default__Password="<PASSWORD>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -e ASPNETCORE_ENVIRONMENT="Development" japangolin-backend
```
3. Navigate to [`https://localhost:7443/swagger/index.html`]() to interact with the API.

## Frontend
The client application can be run in its own development server:
```shell
npm start --prefix .\Frontend\
```

## Both?
TODO: see how much pain is involved in creating a Docker image that contains published backend with production-ready frontend (via `npm run build`)

---

Handy resources:
- https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs
- https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0
