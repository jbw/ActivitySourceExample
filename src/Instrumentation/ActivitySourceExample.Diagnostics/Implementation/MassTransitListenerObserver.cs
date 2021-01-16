using ActivitySourceExample.Diagnostics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ActivitySourceExample.Diagnostics
{
    public class MassTransitListenerObserver : IObserver<KeyValuePair<string, object>>
    {

        public MassTransitListenerObserver()
        {
        }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(KeyValuePair<string, object> value)
        {
            var producer = new MassTransitActivityProducer();
            var activity = producer.StartActivity();

            // Something is listening else nothing is listening...
            if (activity != null)
            {
                producer.StopActivity(activity);
            }
        }
    }

    public class MassTransitActivityProducer
    {

        public Activity StartActivity()
        {

            var activity = InstrumentationExample.ActivitySource.StartActivity(InstrumentationExample.ActivityName, ActivityKind.Client);

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

    }


}
