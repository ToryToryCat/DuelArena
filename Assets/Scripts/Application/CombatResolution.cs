namespace DuelArena.Application
{
    /// <summary>
    /// Represents the outcome of a combat interaction.
    /// </summary>
    public sealed class CombatResolution
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CombatResolution"/> class.
        /// </summary>
        /// <param name="damageApplied">The amount of damage applied to the defender.</param>
        /// <param name="defenderDefeated">Indicates whether the defender has been defeated.</param>
        public CombatResolution(int damageApplied, bool defenderDefeated)
        {
            DamageApplied = damageApplied;
            DefenderDefeated = defenderDefeated;
        }

        /// <summary>
        /// Gets the amount of damage applied to the defender.
        /// </summary>
        public int DamageApplied { get; }

        /// <summary>
        /// Gets a value indicating whether the defender has been defeated.
        /// </summary>
        public bool DefenderDefeated { get; }
    }
}
