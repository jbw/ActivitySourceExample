using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{

    public class Instrumentation : IDisposable
    {
        public static string ActivitySourceName = typeof(Instrumentation).Assembly.GetName().Name;
        public static string ActivitySourceVersion = typeof(Instrumentation).Assembly.GetName().Version.ToString();
        private static string ActivityName = ActivitySourceName + ".Execute";

        private static ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

        public Instrumentation()
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

        static void PrintActivity(Activity activity)
        {
            Console.WriteLine("================= Activity Info ===========================");
            Console.WriteLine(activity.OperationName);
            Console.WriteLine(activity.Id);
            Console.WriteLine(activity.TraceStateString);
            Console.WriteLine(activity.ParentSpanId);
            Console.WriteLine(activity.TraceId);
            Console.WriteLine(activity.SpanId);
            Console.WriteLine("===========================================================");
            Console.WriteLine();

        }

        public void Dispose()
        {
            ActivitySource.Dispose();
        }
    }
}
