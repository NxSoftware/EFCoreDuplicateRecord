PORT=${1:-51257}
ASPNETCORE_URLS="http://*:$PORT" dotnet run
