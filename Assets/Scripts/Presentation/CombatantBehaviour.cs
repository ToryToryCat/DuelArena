using DuelArena.Core;
using UnityEngine;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Exposes a <see cref="MonoBehaviour"/> as a combatant in the duel arena.
    /// </summary>
    public sealed class CombatantBehaviour : MonoBehaviour, ICombatant
    {
        [SerializeField]
        private string identifier = "Combatant";

        [SerializeField]
        private int maximumHealth = 100;

        [SerializeField]
        private int attackPower = 10;

        private readonly Health _health = new Health();

        /// <summary>
        /// Gets the underlying health component.
        /// </summary>
        public IHealth Health => _health;

        /// <summary>
        /// Gets the maximum health configured for the combatant.
        /// </summary>
        public int MaximumHealth => maximumHealth;

        /// <inheritdoc />
        public string Identifier => identifier;

        /// <inheritdoc />
        public int AttackPower => attackPower;

        private void Awake()
        {
            _health.Initialize(maximumHealth);
        }

        /// <inheritdoc />
        public void ReceiveDamage(int amount)
        {
            _health.Damage(amount);
        }

        /// <summary>
        /// Restores the combatant to full health.
        /// </summary>
        public void ResetHealth()
        {
            _health.Initialize(maximumHealth);
        }
    }
}
