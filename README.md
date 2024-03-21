A quiz web app, which consists of a REST API written in ASP.NET CORE, and frontend built with Blazor SSR

## How to run

clone the repo 
```console
  git clone https://github.com/FlaviusAugustus/KahootBackend.git
```
build and run the containers
```console
  docker-compose up
```
Frontend - http://localhost:5161
Backend - http://localhost:7161
SwaggerUI - http://localhost:7161/swagger

## Implemented Features
- CRUD operations
- JWT authorization
- resource and policy based authentication
- UI written in Blazor
- Project deployment with a seeded database via Docker

## TODO
- use Signalr to implement real-time game functionality
- add forms for adding quizzes
