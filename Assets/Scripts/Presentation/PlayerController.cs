using DuelArena.Application;
using DuelArena.Infrastructure;
using UnityEngine;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Handles player-controlled actions in the duel arena.
    /// </summary>
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private CombatantBehaviour player;

        [SerializeField]
        private CombatantBehaviour enemy;

        private IInputCommand _attackCommand;

        private void Start()
        {
            if (Bootstrap.Registry == null)
            {
                Debug.LogError("Bootstrap registry has not been initialized.");
                enabled = false;
                return;
            }

            var combatService = Bootstrap.Registry.Resolve<CombatService>();
            var logger = Bootstrap.Registry.Resolve<ILogger>();
            _attackCommand = new PlayerAttackCommand(combatService, player, enemy, logger);
        }

        private void Update()
        {
            if (player == null || enemy == null)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _attackCommand?.Execute();
            }
        }
    }
}
