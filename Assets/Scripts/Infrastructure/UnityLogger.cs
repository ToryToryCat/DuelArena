using UnityEngine;

/// <summary>
/// Logs messages to the Unity console.
/// </summary>
public sealed class UnityLogger : ILogger
{
    /// <inheritdoc />
    public void Log(string message)
    {
        Debug.Log(message);
    }
}
