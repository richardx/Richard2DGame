namespace Richard2DGameFramework.Logging
{
    /// <summary>
    /// Interface for logningsfunktionalitet.
    /// </summary>
    public interface ILogger
    {
        void LogInfo(string message);
        void LogWarning(string message);
        void LogError(string message);
    }
}
