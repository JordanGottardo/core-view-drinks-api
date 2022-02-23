This is a dockerised ASP.NET Core 6.0 Web-Api application which lets you manage a drinks basket.

# Running the application
After you have cloned the repository, you can choose between several options to run the application
## Docker
Open a shell inside the _DrinksApi/DrinksApi_ directory (where the Dockerfile is) and launch the following commands:

```
docker build -t drinks-api .
docker run -p 5000:80 drinks-api
```

## Standalone executable (on Windows)
Unzip the _drinks-api-self-contained_ contained in the root of this repository and launch the _DrinksApi.exe_ executable

## Visual Studio
Open _DrinksApi/DrinksApi.sln_ using Visual Studio and run the DrinksApi project

# Testing the application
You can test the application by importing the _DrinksApi.postman_collection.json_ included in the root of this repository into your local Postman.

No matter which method you chose to launch the application, the server listens to requests on localhost:5000. The correct request path is already defined in the Postman requests.

Then you can invoke the APIs:
* Get all items  (http://localhost:5000/basket/items, using GET) will list all items in the basket
* Add item (http://localhost:5000/basket/items/{id}, using POST) will add +1 to the quantity of the requested item
* Modify quantity (http://localhost:5000/basket/items/{id}, using PATCH) will set the quantity of the requested item
* Apply discount (http://localhost:5000/basket/discount, using POST) will apply a discount to the basket
* Get total (http://localhost:5000/basket/total, using GET) will return the basket total
* Get total (http://localhost:5000/basket/total/pay, using POST) will pay for the basket (method:0 for cash, method:1 for credit card)

The drink IDs are the following:
* 0 for italian coffee
* 1 for american coffee
* 2 for tea
* 3 for chocolate


# Executing unit tests
I wrote some units tests for this application. You can find them inside the _DrinksApi.Test_ project.


I used the following libraries to write tests:
* _NUnit_
* _FakeItEasy_, for mocking and stubbing
* _FluentAssertions_, to make assertions in a fluent way

You can launch the tests by opening a shell inside the _DrinksApi_ directory and executing the following commands (requires .NET 6.0 SDK installed on your machine):

```
dotnet build DrinksApi.sln --configuration Debug

dotnet test .\DrinksApi.Test\bin\Debug\net6.0\DrinksApi.Test.dll
```
