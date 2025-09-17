# Immich .NET API Client

A lightweight .NET client for interacting with the [Immich](https://immich.app/) self-hosted photo and video management solution.  
This library provides a strongly-typed wrapper around the Immich REST API for managing albums, people, assets, and shared links.

## Features

- ✅ Authentication with API key
- 📂 Manage albums (create, list, delete)
- 👥 Retrieve people metadata
- 🖼 Query assets via metadata filters
- 🔗 Create shared links
- 📝 Built-in logging with `ILogger`

## How can I use it?

The package is available via [NuGet](https://www.nuget.org/packages/Nager.Immich)
```
PM> install-package Nager.Immich
```

## Usage

```cs
using Nager.Immich;
using Nager.Immich.Models;
using Microsoft.Extensions.Logging;

var httpClient = new HttpClient();
var logger = LoggerFactory.Create(builder => builder.AddConsole()).CreateLogger<ImmichClient>();

var client = new ImmichClient(
    httpClient,
    apiKey: "<YOUR_API_KEY>",
    baseAddress: "https://your-immich-server/api/",
    logger: logger
);

// Get all albums
var albums = await client.GetAlbumAsync();

// Create a new album
var created = await client.CreateAlbumAsync(new CreateAlbumDto
{
    AlbumName = "My Holiday"
});

// Delete an album
await client.DeleteAlbumAsync("album-id");

// Get people metadata
var people = await client.GetPeoplesAsync();

// Search assets by metadata
var assets = await client.GetAssetsAsync(new MetadataSearchDto
{
    PersonIds = ['person-1']
});

// Create a shared link
var sharedLink = await client.CreateSharedLinkAsync(new SharedLinkCreateDto
{
    Type = "INDIVIDUAL",
    Description = "my collection1",
    AllowDownload = true,
    AllowUpload = false,
    ExpiresAt = DateTime.Today.AddDays(7),
    AssetIds = ['asset-1']
});
```

## Swagger Source

https://raw.githubusercontent.com/immich-app/immich/refs/heads/main/open-api/immich-openapi-specs.json
