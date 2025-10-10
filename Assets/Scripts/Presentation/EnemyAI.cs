using System;
using DuelArena.Application;
using UnityEngine;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Controls enemy behaviour using a finite state machine.
    /// </summary>
    public sealed class EnemyAI : MonoBehaviour
    {
        [SerializeField]
        private CombatantBehaviour enemy;

        [SerializeField]
        private CombatantBehaviour player;

        [SerializeField]
        private float chaseRange = 6f;

        [SerializeField]
        private float attackRange = 1.5f;

        [SerializeField]
        private float moveSpeed = 3f;

        [SerializeField]
        private float attackCooldown = 1.2f;

        private EnemyStateMachine _stateMachine;
        private CombatService _combatService;
        private ILogger _logger;

        private void Start()
        {
            if (Bootstrap.Registry == null)
            {
                Debug.LogError("Bootstrap registry has not been initialized.");
                enabled = false;
                return;
            }

            _combatService = Bootstrap.Registry.Resolve<CombatService>();
            _logger = Bootstrap.Registry.Resolve<ILogger>();
            _stateMachine = new EnemyStateMachine(new IdleState(this));
        }

        private void Update()
        {
            if (enemy == null || player == null)
            {
                return;
            }

            _stateMachine?.Update(Time.deltaTime);
        }

        private float DistanceToPlayer()
        {
            return Vector3.Distance(enemy.transform.position, player.transform.position);
        }

        private void MoveTowardsPlayer(float deltaTime)
        {
            var direction = (player.transform.position - enemy.transform.position).normalized;
            enemy.transform.position += direction * moveSpeed * deltaTime;
        }

        private void PerformAttack()
        {
            var result = _combatService.ExecuteAttack(enemy, player);
            _logger.Log($"{enemy.Identifier} attacked {player.Identifier} for {result.DamageApplied} damage.");
        }

        private abstract class EnemyState
        {
            protected EnemyState(EnemyAI context)
            {
                Context = context;
            }

            protected EnemyAI Context { get; }

            public abstract void Update(float deltaTime, EnemyStateMachine stateMachine);
        }

        private sealed class IdleState : EnemyState
        {
            public IdleState(EnemyAI context) : base(context)
            {
            }

            public override void Update(float deltaTime, EnemyStateMachine stateMachine)
            {
                if (Context.DistanceToPlayer() <= Context.chaseRange)
                {
                    stateMachine.ChangeState(new ChaseState(Context));
                }
            }
        }

        private sealed class ChaseState : EnemyState
        {
            public ChaseState(EnemyAI context) : base(context)
            {
            }

            public override void Update(float deltaTime, EnemyStateMachine stateMachine)
            {
                var distance = Context.DistanceToPlayer();
                if (distance > Context.chaseRange)
                {
                    stateMachine.ChangeState(new IdleState(Context));
                    return;
                }

                if (distance <= Context.attackRange)
                {
                    stateMachine.ChangeState(new AttackState(Context));
                    return;
                }

                Context.MoveTowardsPlayer(deltaTime);
            }
        }

        private sealed class AttackState : EnemyState
        {
            private float _cooldownTimer;

            public AttackState(EnemyAI context) : base(context)
            {
                _cooldownTimer = context.attackCooldown;
            }

            public override void Update(float deltaTime, EnemyStateMachine stateMachine)
            {
                var distance = Context.DistanceToPlayer();
                if (distance > Context.attackRange)
                {
                    stateMachine.ChangeState(new ChaseState(Context));
                    return;
                }

                _cooldownTimer -= deltaTime;
                if (_cooldownTimer <= 0f)
                {
                    Context.PerformAttack();
                    _cooldownTimer = Context.attackCooldown;
                }
            }
        }

        private sealed class EnemyStateMachine
        {
            private EnemyState _currentState;

            public EnemyStateMachine(EnemyState initialState)
            {
                _currentState = initialState ?? throw new ArgumentNullException(nameof(initialState));
            }

            public void ChangeState(EnemyState newState)
            {
                _currentState = newState ?? throw new ArgumentNullException(nameof(newState));
            }

            public void Update(float deltaTime)
            {
                _currentState?.Update(deltaTime, this);
            }
        }
    }
}
