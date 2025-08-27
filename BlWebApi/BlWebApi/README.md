# BlWebApi – Web API wrapper for IBL

This project exposes your `BlApi.IBL` over HTTP so that a web front-end (WebPL) can run online on Azure.

## Requirements
- Reference your **BL** and **BO** projects (or DLLs).
  - In Visual Studio: Right click `BlWebApi` → *Add Project Reference…* → select BL/BO.

## Run locally
```
dotnet run --project BlWebApi
```
Open Swagger at `/swagger`.

## CORS
Configure allowed origins in `appsettings.json` under `AllowedOrigins`. For dev you can keep `"*"`. In production, set to your WebPL URL.
