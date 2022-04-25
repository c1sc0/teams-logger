# teams-logger
An app to log ms teams messages.


Rename the sample config to `appsettings.json` and fill the necessary values (login in the browser find one polling request and get the values).

Docker:

Build: `docker build -t teams-logger:latest .`
Run: `docker run -v $(pwd)/appsettings.json:/app/appsettings.json --restart always -v teams-logger:/app -d teams-logger:latest`
