# MumbleLink
Easily read Mumble Link data from memory

## Usage
Install the .NET Core 2 SDK from here: https://www.microsoft.com/net/download/core#/sdk

Build and run using the dotnet CLI.

```
dotnet run --project src\mumble.app
```

### Commands

#### init

`init`: initialize the MumbleLink. This is useful when you don't have a Mumble client installed. Otherwise, I recommend using that instead.

To initialize the MumbleLink, first run the init command and then start your game.

```
dotnet run --project src\mumble.app -- init
```

This opens the shared memory file and then waits until you stop the program. Restart your game to make sure that it detects the MumbleLink. Then press Ctrl+C to stop the init program.

#### identity

`identity`: prints the identity field, which is a string.

```
dotnet run --project src\mumble.app -- identity
```

#### raw

`raw`: prints the entire content of the MumbleLink memory section in binary format. Not meant to be dislayed in a terminal. Instead, you can redirect the output to a file and use a HEX viewer or other tools to view it.

```
dotnet run --project src\mumble.app -- raw > snapshot.bin
```
