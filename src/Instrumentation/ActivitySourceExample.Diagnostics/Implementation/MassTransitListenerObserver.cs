using ActivitySourceExample.Diagnostics;
using System;
using System.Collections.Generic;

namespace ActivitySourceExample.Diagnostics
{
    public class MassTransitListenerObserver : IObserver<KeyValuePair<string, object>>
    {
        public void OnCompleted()
        {
            
        }

        public void OnError(Exception error)
        {
            
        }

        public void OnNext(KeyValuePair<string, object> value)
        {            
            // create activity

          
        }
    }

}
