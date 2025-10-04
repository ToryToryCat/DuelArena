using DuelArena.Application;
using DuelArena.Core;
using DuelArena.Infrastructure;
using UnityEngine;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Initializes application services for the duel arena scene.
    /// </summary>
    public sealed class Bootstrap : MonoBehaviour
    {
        /// <summary>
        /// Gets the global service registry instance.
        /// </summary>
        public static ServiceRegistry Registry { get; private set; }

        private void Awake()
        {
            if (Registry != null)
            {
                return;
            }

            Registry = new ServiceRegistry();
            var logger = new UnityLogger();
            Registry.Register<ILogger>(logger);

            var damagePolicy = new BasicDamagePolicy();
            Registry.Register<IDamagePolicy>(damagePolicy);

            var combatService = new CombatService(damagePolicy);
            Registry.Register(combatService);
        }
    }
}
