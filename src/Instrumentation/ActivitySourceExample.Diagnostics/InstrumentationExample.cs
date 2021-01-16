using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{

    public class InstrumentationExmaple : IDisposable
    {
        public static string ActivitySourceName = typeof(InstrumentationExmaple).Assembly.GetName().Name;
        public static string ActivitySourceVersion = typeof(InstrumentationExmaple).Assembly.GetName().Version.ToString();
        private static string ActivityName = ActivitySourceName + ".Execute";

        private static ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

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
