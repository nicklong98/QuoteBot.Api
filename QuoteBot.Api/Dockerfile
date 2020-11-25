FROM mcr.microsoft.com/dotnet/sdk AS build-env
WORKDIR /app
COPY ./QuoteBot.Api.sln .
COPY ./*/*.csproj ./
RUN for file in $(ls *csproj); do mkdir -p ./${file%.*} && mv $file ./${file%.*}/; done
RUN dotnet restore
COPY ./ .
WORKDIR /app/QuoteBot.Api
RUN dotnet build -c Release 
RUN dotnet publish --no-build -c Release -o /app/out

FROM mcr.microsoft.com/dotnet/aspnet
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "QuoteBot.Api.dll"]