# Pub Finder General

A simple List of Pubs in leeds with details from [Leeds Beer Quest](https://datamillnorth.org/dataset/leeds-beer-quest).

## Documentation

The project is a simple API that reads in a CSV file and out puts a custom formatted object list. 
The FE is a simple React FE whcih currently just adds the pubs to a list of display cards and shows the Name, the review Except and a button for more info. 

Both projects are hosted in Docker containers, that shoudl beable to talk to each other. 

To install download the solution onto a machine with docker installed and run the following 

in the PubFinderGeneral.Data folder please run 
```
>docker build -t pubfindergeneraldataapi . --no-cache
```
once that completes run

```
>docker run -p 3000:80 -t pubfindergeneraldataapi
```

This will get the data APi up and running which you can test by navigating to 

[Pubs](http://localhost:3000/pubs)

