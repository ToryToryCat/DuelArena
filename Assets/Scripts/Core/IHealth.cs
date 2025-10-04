namespace DuelArena.Core
{
    /// <summary>
    /// Defines the mutable contract for an object's health data.
    /// </summary>
    public interface IHealth : IReadOnlyHealth
    {
        /// <summary>
        /// Sets the health to the specified maximum value and restores the current value.
        /// </summary>
        /// <param name="maximum">The desired maximum health.</param>
        void Initialize(int maximum);

        /// <summary>
        /// Applies a healing amount to the health.
        /// </summary>
        /// <param name="amount">The amount to restore.</param>
        void Heal(int amount);

        /// <summary>
        /// Applies a damage amount to the health.
        /// </summary>
        /// <param name="amount">The amount of damage dealt.</param>
        void Damage(int amount);
    }
}
