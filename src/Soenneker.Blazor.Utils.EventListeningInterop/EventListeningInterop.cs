using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using Soenneker.Blazor.Utils.EventListeningInterop.Abstract;

namespace Soenneker.Blazor.Utils.EventListeningInterop;

/// <inheritdoc cref="IEventListeningInterop"/>
public abstract class EventListeningInterop : IEventListeningInterop
{
    protected IJSRuntime JsRuntime { get; }

    protected EventListeningInterop(IJSRuntime jsRuntime)
    {
        JsRuntime = jsRuntime ?? throw new ArgumentNullException(nameof(jsRuntime));
    }

    public ValueTask AddEventListener(string functionName, string elementId, string eventName, object dotNetCallback, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(functionName);
        ArgumentException.ThrowIfNullOrWhiteSpace(elementId);
        ArgumentException.ThrowIfNullOrWhiteSpace(eventName);
        ArgumentNullException.ThrowIfNull(dotNetCallback);

        return JsRuntime.InvokeVoidAsync(functionName, cancellationToken, elementId, eventName, dotNetCallback);
    }
}
