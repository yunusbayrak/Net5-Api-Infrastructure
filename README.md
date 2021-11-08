# Hepsiorada Web Api's

This project is a User lists of products POC.

## Installation

Go to the project directory, open the [Powershell](https://docs.microsoft.com/en-us/powershell/scripting/overview?view=powershell-7.1) and run command:

```bash
docker-compose up
```
You need to initialize database. Go to Hepsiorada.Infrastructure folder run this command:
```bash
dotnet ef --startup-project ..\Hepsiorada.Api\ database update
```
You can add test data automaticaly by using  `GET /api/v{version}/Tests/addtestdata` and  `GET /api/v{version}/Tests/lists` endpoints
## Usage

This Application runs on `http://localhost:1881/swagger/index.html`

## Technologies
* [.NET 5](https://docs.microsoft.com/en-us/dotnet/core/dotnet-five)
* [Asp.Net Core Web Api](https://docs.microsoft.com/en-us/aspnet/core/tutorials/first-web-api?view=aspnetcore-5.0&tabs=visual-studio)
* [PostgreSQL](https://www.postgresql.org/)
* [Mongo DB](https://www.mongodb.com/)
* [Redis](https://redis.io/)
* [Hangfire](https://www.hangfire.io/)
* [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
* [Dapper](https://dapper-tutorial.net/)
* [Docker](https://www.docker.com/)

## Views
#### Database Diagram

> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/db.jpg"/>

#### Architecture

> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/Architecture.png"/>

#### Web API
> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/AuthApi.jpg"/>
> 
> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/ListsApi.jpg"/>

> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/ProductsApi.jpg"/>

> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/UsersApi.jpg"/>

> <img src="https://github.com/Hepsiburada-Backend-Bootcamp/yunus-bayrak-project/blob/main/Images/ReportsApi.jpg"/>

## Contributing
Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to update tests as appropriate.

## License
[MIT](https://choosealicense.com/licenses/mit/)
