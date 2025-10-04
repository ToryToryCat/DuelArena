namespace DuelArena.Core
{
    /// <summary>
    /// Provides a strategy for converting raw attack power into applied damage.
    /// </summary>
    public interface IDamagePolicy
    {
        /// <summary>
        /// Calculates the effective damage for the supplied base amount.
        /// </summary>
        /// <param name="baseAmount">The unmodified damage amount.</param>
        /// <returns>The damage value that should be applied to a target.</returns>
        int CalculateDamage(int baseAmount);
    }
}
