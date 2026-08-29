[![](https://img.shields.io/nuget/v/soenneker.blazor.utils.eventlisteninginterop.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.eventlisteninginterop/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.eventlisteninginterop/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.eventlisteninginterop/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.blazor.utils.eventlisteninginterop.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.blazor.utils.eventlisteninginterop/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.blazor.utils.eventlisteninginterop/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.blazor.utils.eventlisteninginterop/actions/workflows/codeql.yml)

# Soenneker.Blazor.Utils.EventListeningInterop

A small base contract for Blazor interop classes that delegate DOM event-listener registration to JavaScript.

This is infrastructure for library authors, not a standalone event-listener service. It standardizes the call shape used by higher-level packages such as `Soenneker.Blazor.Utils.InteropEventListener`.

## Installation

```bash
dotnet add package Soenneker.Blazor.Utils.EventListeningInterop
```

No DI registrar is included. Derive an interop class from `EventListeningInterop`, or implement `IEventListeningInterop` when listener registration happens through an imported JavaScript module.

## Derive from the base class

The base implementation calls a JavaScript function through `IJSRuntime` with these arguments, in order: element ID, event name, and callback object.

```csharp
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.EventListeningInterop;

public sealed class WidgetInterop : EventListeningInterop
{
    public WidgetInterop(IJSRuntime jsRuntime) : base(jsRuntime)
    {
    }

    public ValueTask Listen(
        string elementId,
        string eventName,
        object callback,
        CancellationToken cancellationToken = default)
    {
        return AddEventListener(
            "widgetInterop.addEventListener",
            elementId,
            eventName,
            callback,
            cancellationToken);
    }
}
```

The corresponding JavaScript function must accept that shape:

```javascript
window.widgetInterop = {
    addEventListener(elementId, eventName, callback) {
        const element = document.getElementById(elementId);
        if (!element)
            throw new Error(`Element '${elementId}' was not found.`);

        element.addEventListener(eventName, event => {
            callback.invokeMethodAsync("Invoke", event.type);
        });
    }
};
```

If your JavaScript lives in an imported ES module, implement `IEventListeningInterop` and invoke the function on the module reference instead of using the base class, because the base class resolves a global `IJSRuntime` identifier.

## Listener and callback lifetime

`AddEventListener` only waits for the JavaScript registration call. Cancelling its token can cancel that pending interop call; it does not remove a listener that JavaScript already attached.

The consuming interop must define a matching JavaScript removal operation and call it during component or service disposal. It must also keep any `DotNetObjectReference<T>` alive for as long as JavaScript can invoke it, then dispose the reference only after the listener can no longer fire. Adding the same listener more than once is not deduplicated here.

Treat `functionName` as a trusted constant owned by the library. Do not accept it from URL, markup, or other untrusted input. Callback payloads originate in the browser and should be validated before they affect privileged application behavior.
