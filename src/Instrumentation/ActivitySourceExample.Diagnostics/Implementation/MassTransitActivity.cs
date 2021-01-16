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

        public static Activity Translate(Activity parent, KeyValuePair<string, object> messageContext)
        {
            var activityName = messageContext.Key;

            var activity = ActivitySource.StartActivity(
                name: activityName,
                kind: parent.Kind,
                parentContext: parent == null ? parent.Context : default,
                links: parent.Links,
                startTime: parent.StartTimeUtc
            );

            foreach (var bag in parent.Baggage)
            {
                activity.AddBaggage(bag.Key, bag.Value);
            }

            foreach (var tag in parent.Tags)
            {
                activity.AddTag(tag.Key, tag.Value.ToString());
            }

            return activity;
        }
    }
}
