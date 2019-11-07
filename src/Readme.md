# The project

This is an exercise project made with C#.

## **Project goal:**

The goal of this project is to provide 2 http endpoints that accepts JSON base64 encoded binary data on both
endpoints:
```json
<host>/v1/diff/<ID>/left
```
 and 
 ```json
 <host>/v1/diff/<ID>/right
 ```
The provided data needs to be diff-ed and the results shall be available on a third end
point:
```json
 <host>/v1/diff/<ID>
```
The results shall provide the following info in JSON format:

o If equal return that

o If not of equal size just return that

o If of same size provide insight in where the diffs are, actual diffs are not needed.

## **How it works**

The project solution was built using Dotnet Core framework to create a **RESTful API** that offers two **POST** endpoints to receive the *left* and *right* entries, respectively; as well as a **GET** endpoint that points out the entries differences.

In order to map the right and left entries when calling the **POST** endpoints, a request body contract model was defined as follows:

```json
{
  "name": "Anna",
  "age": 30,
  "city": "Denver",
  "profession": "Teacher"
}
```

The **GET** endpoint returns the differences between the right and left entries. It is mapped as shown in the bellow structure:
```json
{
    "areEqual": false,
    "areSameSize": true,
    "differences": [
        "name",
        "age"
    ]
}
```


## **Testing the application**
In order to test the application, go to the Assignment.API folder inside your terminal and type:
> dotnet run

Then, use a tool such as Postman to test the POST endpoints by sending a POST call to:
> http://localhost:5000/api/v1/diff/1/left

The POST **must** contain a JSON body that should follow the structure:
```json
{
  "name": "Anna",
  "age": 28,
  "city": "Denver",
  "profession": "Doctor"
}
```

Then send a post to the endpoint of the right side as well:

> http://localhost:5000/api/v1/diff/1/right

sendind a similar JSON body, as shown bellow:
```json
{
  "name": "Marc",
  "age": 28,
  "city": "Denver",
  "profession": "Doctor"
}
```

Both POST calls should return as a Response a JSON representation of the sent Person containing its Id info.

After posting the left and right entries, it is possible to check the differences between the two people by sending a GET request to the endpoint:
>http://localhost:5000/api/v1/diff/1

and the service should return a response like this:
```json
{
    "areEqual": false,
    "areSameSize": true,
    "differences": [
        "name"
    ]
}
```
## **Code structure**
The solution consists of three projects that will be explained bellow:


## Asignment.API project
The core implementation project, contains all the logic of processing the Requests and Responses of the service.
The code is organized in the following structure:

## <em>Startup.cs</em>

The Startup class on the root folder of the application is the responsible for configuring the app service.

## <em>Controllers layer</em>
The **Controllers** layer is where the ***DiffController*** class is placed. This class maps out the methods to be called when the API receives requests through the specified routes. 

## <em>Domain layer</em>
This layer contains:
- The Models classes, which represent the Person and PersonDiff models;
- The Services classes, that have the logic to call the Repositories classes to Save or List Persons;
- The Helpers folder, that has the PeopleComparerHelper class, which is where the main comparison logic is placed.

## <em>Persistence layer</em>
This layer contains:
- The Contexts folder, which has a class named <em>AppDbContext</em>, responsible for configuring the database and creating the tables.
- The Repositories folder, that contains the Repository classes. The repository classes encapsulate all the logic to handle data access. 

## <em>Resources layer</em>
The Resources layer is where the Resources classes are placed. These classes map only the basic information that will be exchanged between client applications and API endpoints. They are needed because there are properties on the models that do not need to be present in the service Response, like the Id property.

## <em>Mapping layer</em>
Has the classes to map Resources to Models and vice-versa. In order to do so, it uses the AutoMapper library.

## <em>Extensions layer</em>
Place for adding extension methods. It contains the GetErrorMessages() extension method, that converts the validation errors into a more simple message to return to the client.


## Asignment.API.Tests project
On the API.Tests project there are tests used to validate internal logic of the service.
## Asignment.API.IntegrationTests project
On the API.IntegrationTests project there are tests used to validate the whole functionality of the service.