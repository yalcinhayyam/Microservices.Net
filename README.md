```sh
    dotnet new sln
    dotnet sln add (ls -r **/*.csproj)
    dotnet sln add $(ls -r **/*.csproj) for linux mac
    dotnet new classlib -n Library

    dotnet add Catalogue reference Library
    dotnet add Ordering reference Library

    dotnet sln remove Basket

    dotnet add package Rebus
    dotnet add package Rebus.RabbitMq

    dotnet ef migrations add InitialCreate
    dotnet ef database update

    dotnet ef database drop
    dotnet ef migrations remove


    docker run --cap-add SYS_PTRACE -e 'ACCEPT_EULA=1' -e 'MSSQL_SA_PASSWORD=Secret-Cat' -p 1433:1433 --name azuresqledge -d mcr.microsoft.com/azure-sql-edge

    Microservices.Net\Services\Catalogue\Catalogue.Api> dotnet ef migrations add InitialCreate --project ../Catalogue.Persistence
    Microservices.Net\Services\Catalogue\Catalogue.Api> dotnet ef database update

    dotnet ef migrations add InitialCreate --project ./Services/Catalogue/Catalogue.Persistence --startup-project ./Services/Catalogue/Catalogue.Api

    dotnet ef database update --project ./Services/Catalogue/Catalogue.Persistence/ --startup-project ./Services/Catalogue/Catalogue.Api/


    dotnet ef migrations add InitialCreate --project ./Services/Ordering/Ordering.Api/ --startup-project ./Services/Ordering/Ordering.Api/

    dotnet ef database update --project ./Services/Ordering/Ordering.Api/ --startup-project ./Services/Ordering/Ordering.Api/
    dotnet ef database drop --project ./Services/Ordering/Ordering.Api/ --startup-project ./Services/Ordering/Ordering.Api/ 
    dotnet ef migrations add InitialCreate --project ./Services/Ordering/Ordering.Api/ --startup-project ./Services/Ordering/Ordering.Api/


    dotnet build ./Services/Ordering/Ordering.Api/
    dotnet build ./Services/Catalogue/Catalogue.Api/

    https://www.atlassian.com/git/tutorials/using-branches/git-merge


        # Start a new feature
    git checkout -b new-feature main
    # Edit some files
    git add <file>
    git commit -m "Start a feature"
    # Edit some files
    git add <file>
    git commit -m "Finish a feature"
    # Merge in the new-feature branch
    git checkout main
    git merge new-feature
    git branch -d new-feature

```
