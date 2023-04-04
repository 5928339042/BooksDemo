# Books Demo

## Running Books Demo using docker compose

### Prerequisites

* It's required to have [.NET Core 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) installed locally<br>
```
dotnet --version
```
* To add/update database migrations it's required to have Entity Framework Core CLI https://docs.microsoft.com/en-us/ef/core/cli/dotnet installed locally
* Rancher Desktop: https://rancherdesktop.io/<br>
  Container runtime must be set to `dockerd`

  Note: Docker Desktop may be used too if one contains the license for it.

### Run containers


To start postgress database container run the following command:

```
docker compose up -d
```

### Other useful commands

Stop services and remove containers:
```
docker compose down
```

View logs from all containers:
```
docker compose logs
```

### Setting up

```shell
git clone https://github.com/5928339042/BooksDemo.git
cd BooksDemo
dotnet build
dotnet run
```

### Add new database migration

* Update Entity Framework DB context in the code
* In command shell run this command (with changed {MigrationName} to the actual name):
```
dotnet ef migrations add {MigrationName}
```

### Update the database to the last migration

```
dotnet ef database update
```

