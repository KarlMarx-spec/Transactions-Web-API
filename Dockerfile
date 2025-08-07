# Базовый образ для запуска
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 5010
EXPOSE 5011

# Этап сборки
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# 1. Копируем только файл решения
COPY ["Transactions-Web-API.sln", "."]

# 2. Копируем ВСЕ файлы проектов (.csproj) с сохранением структуры
COPY */*.csproj ./
RUN for file in $(ls *.csproj); do mkdir -p ${file%.*}; mv $file ${file%.*}; done

# 3. Восстанавливаем зависимости для всего решения
RUN dotnet restore "Transactions-Web-API.sln"

# 4. Копируем все остальные файлы
COPY . .

# 5. Переходим в директорию основного проекта
WORKDIR "/src/Web-API"

# 6. Собираем проект
RUN dotnet build "Transactions-Web-API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этап публикации
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Transactions-Web-API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Финальный этап
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Transactions-Web-API.dll"]