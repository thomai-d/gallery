# Gallery

## What is this?

This is a small web application to inspect images for timelapses.

Just throw your images into a folder, fire up the container and view your images:

![Demo](https://github.com/thomai-d/gallery/blob/master/media/demo.webp)

## Technologies used

- ASP.NET Core 8 (minimal api), xUnit, NSubstitute
- React

## How to...

### ... build docker container
```
docker build -t gallery .
```

### ... run container with docker-compose
```
version: '3.3'

services:
  gallery:
    image: gallery
    container_name: gallery
    restart: always
    volumes:
      - "/home/someuser/galleries:/galleries:ro"
    environment:
      - GalleryOptions:GalleryPath=/galleries
    ports:
      - 8080:8080
```
