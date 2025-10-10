public interface ILogger
{
    /// <summary>
    /// Writes an informational message to the configured sink.
    /// </summary>
    /// <param name="message">The message to log.</param>
    void Log(string message);
}
