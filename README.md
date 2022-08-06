# Pub Finder General

A simple List of Pubs in leeds with details from [Leeds Beer Quest](https://datamillnorth.org/dataset/leeds-beer-quest).


## Prerequisits 
A machine that can connect to the internet and has Docker and the .net6.0 SDK installed will run the app. 

To download docker please go to [Docker](https://docs.docker.com/get-docker/)
To download the .Net 6.0 SDK please go to [.Net 6.0 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/6.0)

follow the instructions on these sites to ensure the machine is set up correctly. 

## Documentation

The project is a simple API that reads in a CSV file and out puts a custom formatted object list. 
The FE is a simple React FE whcih currently just adds the pubs to a list of display cards and shows the Name, the review Except and a button for more info. 

Both projects are hosted in Docker containers, that shoudl beable to talk to each other. 

To install download the solution onto a machine with docker installed and run the following 

open a cmd window and nivigate to the PubFinderGeneral.Data folder then please run 
```
>docker build -t pubfindergeneraldataapi . --no-cache
```
once that completes run

```
>docker run -p 3001:80 -t pubfindergeneraldataapi
```

This will get the data APi up and running which you can test by navigating to

[Pubs](http://localhost:3001/pubs)

next open another cmd window and navigate to pubfindergeneral.client Folder and please run 

```
docker build -t pubfindergeneralclient .
```

once that completes please run 

```
docker run -it --rm -v /app -v /app/node_modules -p 3026:3006 -e CHOKIDAR_USEPOLLING=true pubfindergeneralclient
```

now if you navigate to [Pub Finder General](http://localhost:3026)
you should see the list of pubs. 

Please not this is a basic implementation and not all aspects are functional. 