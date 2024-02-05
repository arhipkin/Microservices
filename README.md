# Microservices

# Identity Service
This service provide jwt token authentication and authorization. Project developed using DDD (Domain Driven Design) pattern. The identity service based on web api project template. Hosted on built-in Kestrel web server. For manage users, roles, claims etc used open source Microsoft.AspNetCore.Identity.EntityFrameworkCore nuget package with a little customization included additional UserDetails table and realations with Roles. Database layer implemented with Code-First approach, database will be created at first application start. PostgreSQL used as data provider and it could be easely migrating to anouther DB provider in Program.cs while registration.

In project you won't see any try catch blocks to handle exceptions. While all logic starts working with REST requests, there is embeded custom exception middleware to handle all exceptions.

For checking API methods project implements SwaggerUI interface and available on URL https://localhost:7039/swagger/index.html (you can change port in appsettings.json or in launchSettings.json for debug)

For mantaining authentication and authourization used jwt tokens with assymetric encryption algorythm. Thus anouther services don't need to keep secret key, instead they only have to get public certificate to check signing section.

Getting started to use certificates:
- You need to generate private key, private certificate, and public certificate
  
openssl genrsa -out identity_private.key 4096

openssl req -new -x509 -key identity_private.key -out identity.cer -days 3650

openssl pkcs12 -export -out identity_privatekey.pfx -inkey identity_private.key -in identity.cer
