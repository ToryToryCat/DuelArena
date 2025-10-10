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

        [SerializeField]
        private float moveSpeed = 5f;

        [SerializeField]
        private float rotationSpeed = 720f;

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

            var movement = ReadMovementInput();
            if (movement.sqrMagnitude > 0f)
            {
                MovePlayer(movement);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                _attackCommand?.Execute();
            }
        }

        private Vector3 ReadMovementInput()
        {
            var horizontal = Input.GetAxisRaw("Horizontal");
            var vertical = Input.GetAxisRaw("Vertical");
            return new Vector3(horizontal, 0f, vertical);
        }

        private void MovePlayer(Vector3 inputDirection)
        {
            var movement = inputDirection.normalized * moveSpeed * Time.deltaTime;
            var currentPosition = player.transform.position;
            var targetPosition = currentPosition + movement;
            targetPosition.y = currentPosition.y;
            player.transform.position = targetPosition;

            var lookDirection = movement;
            lookDirection.y = 0f;
            if (lookDirection.sqrMagnitude <= 0f)
            {
                return;
            }

            var targetRotation = Quaternion.LookRotation(lookDirection, Vector3.up);
            player.transform.rotation = Quaternion.RotateTowards(
                player.transform.rotation,
                targetRotation,
                rotationSpeed * Time.deltaTime);
        }
    }
}
