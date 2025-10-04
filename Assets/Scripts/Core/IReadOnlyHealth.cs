using System;

namespace DuelArena.Core
{
    /// <summary>
    /// Represents an immutable view of a health container.
    /// </summary>
    public interface IReadOnlyHealth
    {
        /// <summary>
        /// Occurs when the health value of the owner changes.
        /// </summary>
        event EventHandler<HealthChangedEventArgs> HealthChanged;

        /// <summary>
        /// Occurs when the health is fully depleted.
        /// </summary>
        event EventHandler<HealthDepletedEventArgs> HealthDepleted;

        /// <summary>
        /// Gets the current health value.
        /// </summary>
        int Current { get; }

        /// <summary>
        /// Gets the maximum health value.
        /// </summary>
        int Maximum { get; }

        /// <summary>
        /// Gets a value indicating whether the health is depleted.
        /// </summary>
        bool IsDepleted { get; }
    }
}
