```sh
    dotnet new sln
    dotnet sln add (ls -r **/*.csproj)
    dotnet new classlib -n Library

    dotnet add Catalogue reference Library
    dotnet add Ordering reference Library
    
    dotnet sln remove Basket
    
    dotnet add package Rebus
    dotnet add package Rebus.RabbitMq
```
