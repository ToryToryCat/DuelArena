using System;
using DuelArena.Application;
using DuelArena.Infrastructure;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Executes an attack using the configured combat service.
    /// </summary>
    public sealed class PlayerAttackCommand : IInputCommand
    {
        private readonly CombatService _combatService;
        private readonly CombatantBehaviour _attacker;
        private readonly CombatantBehaviour _defender;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerAttackCommand"/> class.
        /// </summary>
        /// <param name="combatService">The combat service used to resolve attacks.</param>
        /// <param name="attacker">The attacking combatant.</param>
        /// <param name="defender">The defending combatant.</param>
        /// <param name="logger">The logger used to record combat events.</param>
        public PlayerAttackCommand(CombatService combatService, CombatantBehaviour attacker, CombatantBehaviour defender, ILogger logger)
        {
            _combatService = combatService ?? throw new ArgumentNullException(nameof(combatService));
            _attacker = attacker ?? throw new ArgumentNullException(nameof(attacker));
            _defender = defender ?? throw new ArgumentNullException(nameof(defender));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public void Execute()
        {
            var result = _combatService.ExecuteAttack(_attacker, _defender);
            _logger.Log($"{_attacker.Identifier} dealt {result.DamageApplied} damage to {_defender.Identifier}.");

            if (result.DefenderDefeated)
            {
                _logger.Log($"{_defender.Identifier} has been defeated.");
            }
        }
    }
}
