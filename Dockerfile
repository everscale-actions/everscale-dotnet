FROM mcr.microsoft.com/dotnet/sdk:5.0 AS sdk

WORKDIR /app
COPY . .

RUN dotnet build -v n
RUN dotnet test -v n

ENTRYPOINT ["/bin/bash"]
