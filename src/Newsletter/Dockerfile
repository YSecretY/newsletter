FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Newsletter/Newsletter.csproj", "src/Newsletter/"]
COPY ["src/modules/shared/Newsletter.Shared.Domain/Newsletter.Shared.Domain.csproj", "src/modules/shared/Newsletter.Shared.Domain/"]
COPY ["src/modules/articles/Newsletter.Articles.Infrastructure/Newsletter.Articles.Infrastructure.csproj", "src/modules/articles/Newsletter.Articles.Infrastructure/"]
COPY ["src/modules/articles/Newsletter.Articles.Application/Newsletter.Articles.Application.csproj", "src/modules/articles/Newsletter.Articles.Application/"]
COPY ["src/modules/articles/Newsletter.Articles.Domain/Newsletter.Articles.Domain.csproj", "src/modules/articles/Newsletter.Articles.Domain/"]
COPY ["src/modules/articles/Newsletter.Articles.Presentation/Newsletter.Articles.Presentation.csproj", "src/modules/articles/Newsletter.Articles.Presentation/"]
RUN dotnet restore "src/Newsletter/Newsletter.csproj"
COPY . .
WORKDIR "/src/src/Newsletter"
RUN dotnet build "Newsletter.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Newsletter.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Newsletter.dll"]
