using System;

namespace DuelArena.Core
{
    /// <summary>
    /// Applies a minimal damage calculation that ensures non-negative values.
    /// </summary>
    public sealed class BasicDamagePolicy : IDamagePolicy
    {
        /// <inheritdoc />
        public int CalculateDamage(int baseAmount)
        {
            if (baseAmount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(baseAmount), "Damage amount must be non-negative.");
            }

            return baseAmount;
        }
    }
}
