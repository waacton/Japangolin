// assuming powershell + top-level folder is working directory
// https://learn.microsoft.com/en-us/aspnet/core/security/docker-https?view=aspnetcore-7.0
// https://learn.microsoft.com/en-us/dotnet/core/tools/dotnet-dev-certs
// todo: reconfigure to be from web folder

// dev certs required to allow dev docker container to handle SSL
dotnet dev-certs https -ep .\https\aspnetapp.pfx -p <PASSWORD>
dotnet dev-certs https --trust

docker build -t japangolin-backend -f .\Japangolin.Web\Backend\Dockerfile .

// running docker container will be accessible via 7080 (HTTP) & 7443 (HTTPS)
// will mount .\https\ as a volume so the dev cert can be used
docker run --rm -p 7080:80 -p 7443:443 -v ${pwd}\https:/https/ -e ASPNETCORE_URLS="https://+;http://+" -e ASPNETCORE_Kestrel__Certificates__Default__Password="<PASSWORD>" -e ASPNETCORE_Kestrel__Certificates__Default__Path=/https/aspnetapp.pfx -e ASPNETCORE_ENVIRONMENT="Development" japangolin-backend