using System;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Blazor.Utils.EventListeningInterop.Abstract;

/// <summary>
/// A base type for use with Blazor interops that need to listen for events.
/// </summary>
public interface IEventListeningInterop
{
    /// <summary>
    /// Adds an event listener to the specified HTML element with the given ID.
    /// </summary>
    /// <param name="functionName">The trusted JavaScript function identifier that attaches the listener.</param>
    /// <param name="elementId">The ID of the HTML element to attach the event listener to.</param>
    /// <param name="eventName">Name of the event to publish or subscribe to.</param>
    /// <param name="dotNetCallback">The callback object passed to JavaScript, typically a <c>DotNetObjectReference&lt;T&gt;</c>.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the asynchronous operation.</param>
    /// <returns>A task that completes when the event listener addition is complete.</returns>
    /// <exception cref="ArgumentException">Thrown when a required string is empty or whitespace.</exception>
    /// <exception cref="ArgumentNullException">Thrown when a required argument is null.</exception>
    ValueTask AddEventListener(string functionName, string elementId, string eventName, object dotNetCallback, CancellationToken cancellationToken = default);
}
