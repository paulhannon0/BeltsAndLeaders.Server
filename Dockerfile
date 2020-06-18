FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build-env
WORKDIR /app

ENV MSBUILDSINGLELOADCONTEXT=1

COPY . ./

WORKDIR /app/src

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app
COPY --from=build-env /app/src/out .
ENTRYPOINT ["dotnet", "BeltsAndLeaders.Server.Api.dll"]
