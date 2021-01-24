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
            var currentActivity = Activity.Current;
            if (currentActivity == null) return;

            MassTransitActivity.Translate(currentActivity, context);

        }
    }
}
