#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM microsoft/dotnet:2.1-aspnetcore-runtime-nanoserver-1803 AS base
WORKDIR /app
EXPOSE 48869
EXPOSE 44320

FROM microsoft/dotnet:2.1-sdk-nanoserver-1803 AS build
WORKDIR /src
COPY ["Portal.Web/Portal.Web.csproj", "Portal.Web/"]
RUN dotnet restore "Portal.Web/Portal.Web.csproj"
COPY . .
WORKDIR "/src/Portal.Web"
RUN dotnet build "Portal.Web.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Portal.Web.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Portal.Web.dll"]