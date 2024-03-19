You should define a new Secrets file for both Api and Worker packages containing the following fields:

```JSON
{
    "ConnectionStrings": {
        "Database": "server=host;database=dbName;user=userName;password=userPass",
        "RabbitMQ": "amqp://user:pass@localhost:5672/"
    },
    "JwtKey": "jwt_key"
}
```

More information: https://learn.microsoft.com/pt-br/aspnet/core/security/app-secrets?view=aspnetcore-7.0&tabs=linux