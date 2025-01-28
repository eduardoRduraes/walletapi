# Usar a imagem base do .NET SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

# Diretório de trabalho dentro do container
WORKDIR /app

# Copiar os arquivos de projeto
COPY *.csproj ./
RUN dotnet restore

# Copiar o restante do código-fonte
COPY . ./

# Construir o projeto
RUN dotnet publish -c Release -o out

# Imagem runtime para execução da aplicação
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build /app/out .

# Porta que será exposta
EXPOSE 5000
EXPOSE 5001

# Iniciar a aplicação
ENTRYPOINT ["dotnet", "WalletAPI.dll"]
