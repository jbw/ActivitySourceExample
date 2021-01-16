using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{

    public class InstrumentationExample : IDisposable
    {
        internal static string ActivitySourceName = typeof(InstrumentationExample).Assembly.GetName().Name;
        internal static string ActivitySourceVersion = typeof(InstrumentationExample).Assembly.GetName().Version.ToString();
        internal static readonly string ActivityName = ActivitySourceName + ".Execute";
        internal static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

        public InstrumentationExample()
        {
            //ActivitySource.AddActivityListener(new ActivityListener
            //{
            //    ShouldListenTo = _ =>
            //    {
            //        return true;
            //    },
            //    Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllDataAndRecorded,
            //    ActivityStarted = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Start"),
            //    ActivityStopped = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Stop")
            //});

            DiagnosticListener.AllListeners.Subscribe(new DiagnosticListenerObserver());
        }

        public void Dispose()
        {
            ActivitySource.Dispose();
        }
    }
}
