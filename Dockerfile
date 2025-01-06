# Utiliza la imagen base de .NET SDK para compilar la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copia los archivos de proyecto y restaura las dependencias
COPY *.csproj ./
RUN dotnet restore

# Copia el resto de los archivos y compila la aplicación
COPY . ./
RUN dotnet publish -c Release -o out

# Utiliza la imagen base de .NET Runtime para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Exponer el puerto en el que la aplicación escuchará
EXPOSE 80

# Configurar variables de entorno
ENV ASPNETCORE_URLS=http://+:80

# Comando para ejecutar la aplicación
ENTRYPOINT ["dotnet", "24dex-backend-comercial.dll"]