# Personal Finance Tracker

Welcome to the **Personal Finance Tracker**, a comprehensive software application designed to help you manage your personal finances effortlessly. Whether you want to keep track of your expenses, monitor your incomes, check your balance, or stay updated on the latest exchange rates, this application has got you covered.

## Features
- **Expense Management:** Add your expenses with ease, categorize them, and keep a detailed record of your spending habits.

- **Income Tracking:** Log your incomes, helping you maintain a clear overview of your financial inflows.

- **Balance Overview:** Quickly check your current balance to stay informed about your overall financial status.

- **Exchange Rates:** Utilize a proxy API to obtain the most recent exchange rates, providing you with up-to-date currency conversion information.

- **Analyzing Service:** Seamlessly analyze your financial data and generate insights to make informed decisions based on detailed reports and statistics.
  
- **Recurring Background Jobs (Hangfire):** Automate monthly salary addition, ensuring financial data stays up-to-date without manual intervention.

## Technologies Used
- **.NET Core:** The foundation of the application, ensuring cross-platform compatibility and high-performance execution.

- **Entity Framework Core:** A powerful and flexible Object-Relational Mapping (ORM) framework for working with databases in .NET applications.

- **Refit:** Simplifying the creation of HTTP requests to external APIs, making integration with the proxy API seamless.

- **Mediator Design Pattern:** Employing the Mediator pattern to enhance modularity and maintainability within the application.

- **CQRS (Command Query Responsibility Segregation):** Implementing CQRS to separate the read and write operations, enhancing system performance and scalability.
  
- **RabbitMQ**: Utilizing RabbitMQ as a message broker to facilitate asynchronous communication between microservices, ensuring decoupling of services, reliable message delivery, and efficient handling of high-volume transactions.
  
- **gRPC**: Enables high-performance, cross-platform communication between microservices using a lightweight and efficient Remote Procedure Call (RPC) framework. Ideal for real-time, low-latency, and high-throughput interactions between distributed systems.
  
- **Hangfire**: Background job scheduling for automated recurring tasks like adding salaries, updating balances, or sending notifications.

## Client Side
The client-side development is currently in progress, bringing a user-friendly interface to interact with the Personal Finance Tracker API.

## Authorization and Authentication
Enhancements in the form of authorization and authentication are underway to ensure secure access to your financial data.

## Architecture
<img width="884" height="645" alt="{9DAF7231-C83D-4BC3-A4EF-D72C5BDA118D}" src="https://github.com/user-attachments/assets/55053f7c-ea81-4c01-a5d7-e3c1aba93031" />






