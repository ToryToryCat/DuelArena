namespace DuelArena.Presentation
{
    /// <summary>
    /// Represents an executable input action.
    /// </summary>
    public interface IInputCommand
    {
        /// <summary>
        /// Executes the command behavior.
        /// </summary>
        void Execute();
    }
}
