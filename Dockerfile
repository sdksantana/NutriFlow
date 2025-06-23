# Usa imagem oficial do .NET 8 SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia tudo
COPY . .

# Vai para o projeto da API
WORKDIR /app

# Restaura dependências
RUN dotnet restore

# Builda o projeto
RUN dotnet publish -c Release -o /app/publish

# Imagem final: runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .

# Força ambiente Production
ENV ASPNETCORE_ENVIRONMENT=Production

# 👇 ESSAS DUAS LINHAS SÃO ESSENCIAIS NO RAILWAY
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

ENTRYPOINT ["dotnet", "NutriFlow.WebApi.dll"]
