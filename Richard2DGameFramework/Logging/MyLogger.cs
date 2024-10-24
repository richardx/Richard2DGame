
using System.Diagnostics;

namespace Richard2DGameFramework.Logging
{
    /// <summary>
    /// En klasse, der implementerer <see cref="ILogger"/> og håndterer logging.
    /// </summary>
    public class MyLogger : ILogger
    {
        /// <summary>
        /// Initialiserer en ny instans af <see cref="MyLogger"/>.
        /// </summary>
        public MyLogger()
        {
            // Initialiser TraceListeners, hvis nødvendigt
        }

        /// <summary>
        /// Tilføjer en TraceListener til loggeren.
        /// </summary>
        /// <param name="listener">TraceListener der skal tilføjes.</param>
        public void RegisterListener(TraceListener listener)
        {
            if (listener != null && !Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Add(listener);
            }
        }

        /// <summary>
        /// Fjerner en TraceListener fra loggeren.
        /// </summary>
        /// <param name="listener">TraceListener der skal fjernes.</param>
        public void UnregisterListener(TraceListener listener)
        {
            if (listener != null && Trace.Listeners.Contains(listener))
            {
                Trace.Listeners.Remove(listener);
            }
        }

        /// <summary>
        /// Logger en informationsbesked.
        /// </summary>
        /// <param name="message">Beskeden, der skal logges.</param>
        public void LogInfo(string message)
        {
            Trace.TraceInformation(message);
        }

        /// <summary>
        /// Logger en advarselsbesked.
        /// </summary>
        /// <param name="message">Beskeden, der skal logges.</param>
        public void LogWarning(string message)
        {
            Trace.TraceWarning(message);
        }

        /// <summary>
        /// Logger en fejlbesked.
        /// </summary>
        /// <param name="message">Beskeden, der skal logges.</param>
        public void LogError(string message)
        {
            Trace.TraceError(message);
        }
    }
}
