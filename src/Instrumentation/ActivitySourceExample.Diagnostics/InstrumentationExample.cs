using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{

    public class InstrumentationExmaple : IDisposable
    {
        internal static string ActivitySourceName = typeof(InstrumentationExmaple).Assembly.GetName().Name;
        internal static string ActivitySourceVersion = typeof(InstrumentationExmaple).Assembly.GetName().Version.ToString();
        private static readonly string ActivityName = ActivitySourceName + ".Execute";

        private static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

        public InstrumentationExmaple()
        {

        }

        public Activity StartActivity()
        {

            var activity = ActivitySource.StartActivity(ActivityName, ActivityKind.Client);

            if (activity == null) return null;

            activity.SetParentId(Guid.NewGuid().ToString());

            activity.Start();

            return activity;
        }


        public void StopActivity(Activity activity)
        {
            activity.SetEndTime(DateTime.UtcNow);

            activity.Stop();
        }


        public void Dispose()
        {
            ActivitySource.Dispose();
        }
    }
}
