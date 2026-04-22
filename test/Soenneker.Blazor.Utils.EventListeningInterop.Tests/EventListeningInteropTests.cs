using Soenneker.Blazor.Utils.EventListeningInterop.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Blazor.Utils.EventListeningInterop.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public class EventListeningInteropTests : HostedUnitTest
{
    public EventListeningInteropTests(Host host) : base(host)
    {

    }

    [Test]
    public void Default()
    {

    }
}
