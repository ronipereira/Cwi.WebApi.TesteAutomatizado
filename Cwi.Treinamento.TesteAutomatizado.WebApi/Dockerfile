FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build-stage
ENV ASPNETCORE_ENVIRONMENT=Development
WORKDIR /app
COPY ./ ./
EXPOSE 80

RUN dotnet restore Cwi.Treinamento.TesteAutomatizado.WebApi/Cwi.Treinamento.TesteAutomatizado.WebApi.csproj
RUN dotnet publish -c Release -o out Cwi.Treinamento.TesteAutomatizado.WebApi/Cwi.Treinamento.TesteAutomatizado.WebApi.csproj

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS production-stage
WORKDIR /app
COPY --from=build-stage /app/out/ .

ENTRYPOINT ["dotnet", "Cwi.Treinamento.TesteAutomatizado.WebApi.dll"]