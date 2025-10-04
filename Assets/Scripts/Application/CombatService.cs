using System;
using DuelArena.Core;

namespace DuelArena.Application
{
    /// <summary>
    /// Coordinates combat interactions between combatants.
    /// </summary>
    public sealed class CombatService
    {
        private readonly IDamagePolicy _damagePolicy;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatService"/> class.
        /// </summary>
        /// <param name="damagePolicy">The damage policy strategy applied to attacks.</param>
        public CombatService(IDamagePolicy damagePolicy)
        {
            _damagePolicy = damagePolicy ?? throw new ArgumentNullException(nameof(damagePolicy));
        }

        /// <summary>
        /// Performs an attack from the attacker to the defender using the configured damage policy.
        /// </summary>
        /// <param name="attacker">The attacking combatant.</param>
        /// <param name="defender">The defending combatant.</param>
        /// <returns>A resolution describing the outcome of the attack.</returns>
        public CombatResolution ExecuteAttack(ICombatant attacker, ICombatant defender)
        {
            if (attacker == null)
            {
                throw new ArgumentNullException(nameof(attacker));
            }

            if (defender == null)
            {
                throw new ArgumentNullException(nameof(defender));
            }

            var damage = _damagePolicy.CalculateDamage(attacker.AttackPower);
            defender.ReceiveDamage(damage);
            return new CombatResolution(damage, defender.Health.IsDepleted);
        }
    }
}
