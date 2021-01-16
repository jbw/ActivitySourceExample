using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace ActivitySourceExample.Diagnostics
{
    public class DiagnosticListenerObserver : IObserver<DiagnosticListener>
    {
        public IObserver<KeyValuePair<string, object>> EventSubscription { get; private set; }

        public void OnCompleted()
        {

        }

        public void OnError(Exception error)
        {

        }

        public void OnNext(DiagnosticListener listener)
        {
            if (listener.Name == "MassTransit")
            {
                // Subscribe to Massstransit diagnostic sources.
                listener.Subscribe(new MassTransitListenerObserver());
            }

        }
    }

}
