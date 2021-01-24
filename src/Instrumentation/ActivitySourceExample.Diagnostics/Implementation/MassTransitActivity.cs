using System.Collections.Generic;
using System.Diagnostics;

namespace ActivitySourceExample.Diagnostics
{
    public static class MassTransitActivity
    {
        internal static string ActivitySourceName = typeof(InstrumentationExample).Assembly.GetName().Name;
        internal static string ActivitySourceVersion = typeof(InstrumentationExample).Assembly.GetName().Version.ToString();
        internal static readonly string ActivityName = ActivitySourceName + ".Execute";
        internal static readonly ActivitySource ActivitySource = new ActivitySource(ActivitySourceName, ActivitySourceVersion);

        public static void Translate(Activity activity, KeyValuePair<string, object> messageContext)
        {
            var activityName = messageContext.Key;

            var newActivity = ActivitySource.StartActivity(
                name: activityName,
                kind: GetActivityKind(activity),
                parentContext: default,
                startTime: activity.StartTimeUtc
            );

            CopyBaggage(activity, newActivity);
            CopyTags(activity, newActivity);

            newActivity.Stop();
        }

        private static void CopyTags(Activity activity, Activity newActivity)
        {
            foreach (var tag in activity.Tags)
            {
                newActivity.AddTag(tag.Key, tag.Value.ToString());
            }
        }

        private static void CopyBaggage(Activity activity, Activity newActivity)
        {
            foreach (var bag in activity.Baggage)
            {
                newActivity.AddBaggage(bag.Key, bag.Value);
            }
        }

        private static ActivityKind GetActivityKind(Activity activity)
        {
            switch (activity.OperationName)
            {
                case "MassTransit.Transport.Send":
                    return ActivityKind.Producer;
                case "MassTransit.Transport.Receive":
                    return ActivityKind.Consumer;
                case "MassTransit.Consumer.Consume":
                    return ActivityKind.Internal;
                case "MassTransit.Consumer.Handle":
                    return ActivityKind.Internal;
                default:
                    return activity.Kind;
            };
        }
    }
}
