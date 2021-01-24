using System;
using System.Diagnostics;

namespace ActivitySourceExample.Diagnostics
{

    public class InstrumentationExample : IDisposable
    {

        private IDisposable _subscription;

        public InstrumentationExample()
        {
            Activity.DefaultIdFormat = ActivityIdFormat.W3C;

            ActivitySource.AddActivityListener(new ActivityListener
            {
                ShouldListenTo = _ =>
                {
                    return _.Name.Equals(MassTransitActivity.ActivitySourceName);
                },
                Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
                ActivityStarted = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Start"),
                ActivityStopped = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Stop")
            });

            _subscription = DiagnosticListener.AllListeners.Subscribe(new DiagnosticListenerObserver());
        }

        public void Dispose()
        {
            _subscription.Dispose();
        }
    }
}
