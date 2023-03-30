# Books Demo

## Running Books Demo using docker compose

### Prerequisites

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

### Add new database migration

* Update Entity Framework DB context in the code
* In command shell run this command (with changed {MigrationName} to the actual name):
```
dotnet ef migrations add {MigrationName}
```

