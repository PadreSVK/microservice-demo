# Overview

## Producers

* PLC service A
  * data
* PLC service B

## Consumers

* Regulator
  * PLC service A => BL => PLC service B
  * Endpoint
  * Persistence

## Project Tye

> dotnet tool restore

run in "dry mode" - without docker
> dotnet tye run

run in docker
> dotnet tye --docker

