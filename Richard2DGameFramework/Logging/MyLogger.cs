using System.Diagnostics;

namespace Richard2DGameFramework.Logging
{

    public class MyLogger : ILogger
    {


        public void RegisterListener(TraceListener listener) // Bruges til at styre at log beskeder skal bruges i console
        {
            if (listener != null && !Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Add(listener);
            }
        }

        public void UnregisterListener(TraceListener listener) // Bruges til at styre at log beskeder ikke skal bruges i console
        {
            if (listener != null && Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Remove(listener);
            }
        }

        public void LogInfo(string message)
        {
            Trace.TraceInformation(message);
        }


        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        public void LogError(string message)
        {
            Trace.TraceError(message);
        }
    }
}
