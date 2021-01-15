using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample
{
    public static class Listener
    {
        public static ActivityListener CreateActivityListener()
        {
            using var listener = new ActivityListener
            {
                ShouldListenTo = _ => true,
                Sample = (ref ActivityCreationOptions<ActivityContext> _) => ActivitySamplingResult.AllData,
                ActivityStarted = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Start"),
                ActivityStopped = activity => Console.WriteLine($"{activity.ParentId}:{activity.Id} - Stop")
            };

            ActivitySource.AddActivityListener(listener);

            return listener;
        }
    }

    public static class ActivityCreator
    {
        public static string ActivitySourceName = typeof(ActivityCreator).Assembly.GetName().Name;
        public static string ActivitySourceVersion = typeof(ActivityCreator).Assembly.GetName().Version.ToString();
        private static string ActivityName = ActivitySourceName + ".Execute";

        public static void CreateActivity()
        {
            Task.Run(() =>
            {
                using var activitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

                var activity = StartActivity(activitySource);

                PrintActivity(activity);

                StopActivity(activity);

            });
        }

        static Activity StartActivity(ActivitySource source)
        {
            var activity = source.StartActivity(ActivityName, ActivityKind.Client);

            activity.SetParentId(Guid.NewGuid().ToString());

            activity.Start();

            return activity;
        }

        static void StopActivity(Activity activity)
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

    }
}
