# Chatard - Real Time Chat App with WPF and SignalR

The Chatard project is a real-time chat application developed in C# using WPF and SignalR. The application allows users to register and login, add contacts and send messages to their contacts. Messages are stored in a SqlServer database and are updated in real time for all users who are logged into the application. The application was developed using the MVVM pattern.

It was built as final project for the course "Capacitação Tecnológica em Engenharia e Desenvolvimento de Software (CTEDS)", offered by the Department of Computer and Digital Systems Engineering (PCS) of the Polytechnic School of the University of Sao Paulo.

<img alt="image" src="https://user-images.githubusercontent.com/78484194/208271321-12c15597-c3ae-46df-a8f2-aeda9f847c7e.png">


## Components

The application is composed of two projects:

1. `ChatardServer` - A .NET Core 3.1 console application that hosts the SignalR server
2. `Chatard` - A WPF application that hosts the client

The server is responsible for receiving the notifications from the clients and broadcasting them to the other clients, so that they can refresh their messages.


## Database

The application uses a SqlServer database to store the users and their messages. The database is created automatically when the application is run for the first time, using the Entity Framework Core, with the following tables:

1. `Users` - Stores the users of the application
2. `Messages` - Stores the messages sent by the users
3. `UserContacts` - Stores the relationships between users, so that the application knows the contacts of each user


## How to run  

1. Clone the repository
2. Open the solution in Visual Studio
3. Build the solution
4. Add a configuration file named `App.config` to the source folder of the `chatard` project (client), with the following content (you need to change the connection string to match your database server and have a SqlServer up and running)

```xml
<?xml version="1.0" encoding="utf-8"?>
<configuration>
	<connectionStrings>
		<add name="ChatardDatabase"
			 connectionString="Server=YOUR SERVER NAME HERE;Initial Catalog= YOUR DATABASE NAME HERE ;User id= YOUR USERNAME HERE ;pwd=  YOUR PASSWORD HERE ;"
			 providerName="System.Data.SqlClient"/>
	</connectionStrings>
</configuration>
```

5. Right click the solution and select Properties. The `Startup Project` should be set to `Multiple startup projects` and the `Action` for both `chatard` and `ChatardServer` should be set to `Start`, as shown in the image below:


<img width="838" alt="Configuration" align="center" src="https://user-images.githubusercontent.com/78484194/208271452-7958194f-1140-4e92-b4d3-5f886ef10b76.png">

6. Run the solution

## How to use

<img width="454" alt="Login window" align="left" src="https://user-images.githubusercontent.com/78484194/208271477-0d348d52-53fd-4880-a5d5-3675fadd3baf.png">

1. Open the application and register a new user
2. Register another user
3. Login with the first user
4. In the Textbox at the left bottom of the window, type the email of the second user and click the `Add` button
5. Now you can send messages to the second user
6. You can verify if the messages were received by the second user by logging in with the second user


In case you want to see the messages being sent and received in real time, you can open another Visual Studio instance and run the solution again (without starting the server again).
