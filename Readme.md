# Demo code von CQRS + Event Sourcing .NET User Group Meetup Karlsruhe

### How to use

- Repo klonen.
- SQL server connection string in der akka.hokon editieren falls man nicht das image https://hub.docker.com/r/microsoft/mssql-server-linux/ mit default-settings verwendet
- in SQL eine Datenbank "demo" von hand anlegen.
- Jetzt sollte entweder in VS oder mit der dotnet cli das Programm bauen und starten können
- über HTTP POST auf http://localhost:5000/api/books/addbook kann man bücher anlegen und über HTTP GET auf http://localhost:5000/api/books kann man die Liste der Bücher sehen