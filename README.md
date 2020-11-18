
# Introduction

This implementation is in .NET Core 3.1 framework.
There are two projects inside the `lunch_api.sln`.

`lunch_api` project has code related to the API.
`lunch_api.tests` project has code related to the tests for the API.

# Getting Started With API
From inside the `lunch_api` folder, run the web api: `dotnet run`

The command starts the api server in port `5001`.

To make the `GET` request, enter the following url in browser:
`https://localhost:5001/lunch`.

# Running Test Cases
From inside the `lunch_api.tests` folder, run the web api: `dotnet test`.
Tests can also be run individually from Visual Studio Code.

# Running Via Docker Image

From inside the `lunch_api` folder, run the following commands:
-  `docker build -t lunch_api .`
-  `docker run -d -p 8080:80 lunch_api`

You can now reach the API endpoint from: `localhost:8080/lunch`