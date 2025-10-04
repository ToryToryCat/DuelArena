namespace DuelArena.Core
{
    /// <summary>
    /// Represents an entity that can participate in combat interactions.
    /// </summary>
    public interface ICombatant
    {
        /// <summary>
        /// Gets a display-friendly identifier for the combatant.
        /// </summary>
        string Identifier { get; }

        /// <summary>
        /// Gets the mutable health component associated with the combatant.
        /// </summary>
        IHealth Health { get; }

        /// <summary>
        /// Gets the attack power used as the base damage during combat.
        /// </summary>
        int AttackPower { get; }

        /// <summary>
        /// Applies the supplied damage to the combatant.
        /// </summary>
        /// <param name="amount">The amount of damage dealt.</param>
        void ReceiveDamage(int amount);
    }
}
