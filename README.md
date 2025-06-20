[![CI pipeline](https://github.com/ddobbinsweb/trash-service/actions/workflows/ci.yml/badge.svg)](https://github.com/ddobbinsweb/trash-service/actions/workflows/ci.yml)
# trash-service
Trash Service application that tracks customers, service dates, and payments.

# Migrations

```shell
dotnet ef migrations add "[MigrationName]" -c AppDbContext -p ".\src\TrashService\TrashService.csproj" --framework "net9.0"
```