#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Japangolin.Web/Backend/Japangolin.Web.Backend.csproj", "Japangolin.Web/Backend/"]
RUN dotnet restore "Japangolin.Web/Backend/Japangolin.Web.Backend.csproj"
COPY . .
WORKDIR "/src/Japangolin.Web/Backend"
RUN dotnet build "Japangolin.Web.Backend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Japangolin.Web.Backend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Wacton.Japangolin.Web.Backend.dll"]