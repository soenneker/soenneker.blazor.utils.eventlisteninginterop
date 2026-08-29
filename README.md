[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.eventlisteninginterop.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.eventlisteninginterop/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.eventlisteninginterop/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.eventlisteninginterop/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.eventlisteninginterop.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.eventlisteninginterop/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.eventlisteninginterop/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.eventlisteninginterop/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.EventListeningInterop

A base type for use with Blazor interops that need to listen for events.

## Install

```bash
dotnet add package Soenneker.Blazor.Utils.EventListeningInterop
```

## Quick start

```csharp
using Soenneker.Blazor.Utils.EventListeningInterop.Abstract;

IEventListeningInterop eventListeningInterop = /* resolve from DI */;
await eventListeningInterop.AddEventListener("value", "value", "value", /* supply dotNetCallback */ default!, default);
```

Adds an event listener to the specified HTML element with the given ID.

## What you get

- `IEventListeningInterop` — A base type for use with Blazor interops that need to listen for events.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IEventListeningInterop.AddEventListener(functionName, elementId, eventName, dotNetCallback, cancellationToken)` | Adds an event listener to the specified HTML element with the given ID. | A task that completes when the event listener addition is complete. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
