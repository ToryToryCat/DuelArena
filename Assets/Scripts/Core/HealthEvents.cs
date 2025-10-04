using System;

namespace DuelArena.Core
{
    /// <summary>
    /// Provides event data for a change in health value.
    /// </summary>
    public sealed class HealthChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthChangedEventArgs"/> class.
        /// </summary>
        /// <param name="previous">The previous health value.</param>
        /// <param name="current">The new health value.</param>
        /// <param name="maximum">The maximum health value.</param>
        public HealthChangedEventArgs(int previous, int current, int maximum)
        {
            Previous = previous;
            Current = current;
            Maximum = maximum;
        }

        /// <summary>
        /// Gets the previous health value.
        /// </summary>
        public int Previous { get; }

        /// <summary>
        /// Gets the current health value.
        /// </summary>
        public int Current { get; }

        /// <summary>
        /// Gets the maximum health value.
        /// </summary>
        public int Maximum { get; }
    }

    /// <summary>
    /// Provides event data for when health reaches zero.
    /// </summary>
    public sealed class HealthDepletedEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HealthDepletedEventArgs"/> class.
        /// </summary>
        /// <param name="current">The current health value at depletion.</param>
        public HealthDepletedEventArgs(int current)
        {
            Current = current;
        }

        /// <summary>
        /// Gets the health value at depletion.
        /// </summary>
        public int Current { get; }
    }
}
