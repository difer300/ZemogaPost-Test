# Zemoga Posts
## Task

Build a .Net web app that allows to create, edit and publish text-based blog posts, with a comments feature.

### Installing

Clone the source repository from Github.

```
git clone https://github.com/difer300/ZemogaPost-Test.git
```

Click in "Zemoga.sln" to open the Solution.

In the Solution Explorer window go to the solution name.

```
right-click and select the option "Restore NuGet Packages"
```
```
right-click "Clean Solution" and then "Rebuild Solution"
```
To restore the data base using migrations
```
right-click in the Zemoga.Service.Data project and select "Set as StartUp project"
```
Open the Package Manager Console and update the data base.
```
View => Other Windows => Package Mannager Console
```
```
In the Default project select "Zemoga.Service.Data"
```
```
Execute the command "update-database"
```
Go to the solution "Zemoga". 
```
right-click and "Properties" and in the "Startup Project" select "multiple startup projects" and choose "Zemoga.Service" and "Zemoga.Web" as Start projects
```
Run the application
```
right-click "Clean Solution"
```
```
F5
```

Go to https://localhost:44381/

## Running the tests

The run the unit test cases go to the project "Zemoga.Service.Test". 
```
right-click "Run Tests"
```

The program automatically run all the unit test included and show the results in the Unit Tests window. 

## Solution

The Solution include five projects: 
### Zemoga.Service.Data
Inlcude the Data layer with the access to the Data Base.

### Zemoga.Service
Inlcude the methods to expose in the API. 

### Zemoga.Service.Models
Inlcude the Model layer that support the process.

### Zemoga.Web
Inlcude the Web layer that containts the web application to show all the data. 

### Zemoga.Web.Service
Inlcude the layer to call the methos in the API. 

### Zemoga.Service.Test
Inlcude the unit test cases for the API. 

## Business Diagram
![alt text](https://github.com/difer300/ZemogaPost-Test/blob/master/Zemoga.Diagrams/Business%20Diagram.JPG)

## ER Diagram
![alt text](https://github.com/difer300/ZemogaPost-Test/blob/master/Zemoga.Diagrams/E-R%20Diagram.JPG)

## Components Diagram
![alt text](https://github.com/difer300/ZemogaPost-Test/blob/master/Zemoga.Diagrams/Components%20Diagram.JPG)

## Built With

* SDK: .Net Framework 4.6.1
* [EntityFramework 6](https://github.com/aspnet/EntityFramework6/wiki) 
* [RestSharp](http://restsharp.org/)
* [Newtonsoft](https://www.newtonsoft.com/json)
* [AutoFixture](https://github.com/AutoFixture/AutoFixture)


## Authors

* **[Diego Navarro]** - https://github.com/difer300

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details
