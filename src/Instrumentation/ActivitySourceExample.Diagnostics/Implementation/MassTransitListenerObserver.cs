using System;
using System.Collections.Generic;
using System.Diagnostics;

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

        public void OnNext(KeyValuePair<string, object> context)
        {
            var parent = Activity.Current;
            var activity = MassTransitActivity.Translate(parent, context);

            if (activity == null) return;

            activity.Start();
            activity.Stop();
        }
    }
}
