# Story Books

## Project Purpose
This is template project that contains full stack of modern design patterns, from the DB through business logic
to the front-end with deployment setup. There are a key design patters that are used in order to create
flexible, easy understandable and reusable project. For example Domain driven design CQRS, Factories, Repositories,
Vertical slices etc. With all of that, we have DDD monolith application with the opportunity for easy separation
into micro-services. Bounded context are separated into vertical slices that can be reused across
multiple WEB services with just a simple configuration. Each vertical slice that in future will call feature has 
README file with instructions for its configuration and usage.

## Architecture Diagram
![Architecture Diagram](https://github.com/slav40o/StoryBooks/blob/main/Documentation/Architecture%20diagram.png?raw=true)

## Technology stack
Story books is based on the latest version .Net core platform. Here is list with the used platforms and libraries:

* .Net core 6
* AutoMapper
* Docker
* Entity framework core 6
* Fluent validation
* Fluid template engine
* Hangfire
* Ionic Platform with React
* MassTransit with RabbitMQ
* MediatR
* SendGrid
* Swashbuckle for Swagger


## What is 'Story Books'
Story books is application about interactive book reading. Its main goal is to make reading interesting
and entertaining for the young audience. Each person will have the opportunity not only to read about 
his favorite character but to navigate him trough his story. Each discussion will lead you to different
story branches with positive or negative effects. You are challenged to reach the end without facing 
early death or dead end. This application will give more freedom to the authors, chance to expand their
fan bases, sell or give for free their stories and receive feedback from their customers. Users will 
have the opportunity to read entertaining stories, learn new things and earn achievements.

## Application Diagram
![Application Diagram](https://github.com/slav40o/StoryBooks/blob/main/Documentation/Application%20diagram.png?raw=true)