# Displayarr

Displayarr is a simple Windows application that shows recently added movie posters from a Plex server in fullscreen. Each poster is displayed for a configurable amount of time.

## Building

This project targets **.NET 6** and uses **WPF**. To build and run it you need the .NET 6 SDK installed on Windows.

```
dotnet build src/Displayarr/Displayarr.csproj
```

Run the application after building:

```
dotnet run --project src/Displayarr/Displayarr.csproj
```

## Configuration

Edit `MainWindow.xaml.cs` to set your Plex server URL and token. Adjust the timer interval to control how long each poster is shown.

```csharp
_client = new PlexClient("http://localhost:32400", "YOUR_TOKEN_HERE");
_timer.Interval = TimeSpan.FromMinutes(1); // duration per poster
```

## How it works

On startup the application fetches recently added items from Plex using the API, downloads the poster images and cycles through them fullscreen.
