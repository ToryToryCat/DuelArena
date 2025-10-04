using DuelArena.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Displays the outcome of the duel when a combatant is defeated.
    /// </summary>
    public sealed class GameResultController : MonoBehaviour
    {
        [SerializeField]
        private CombatantBehaviour player;

        [SerializeField]
        private CombatantBehaviour enemy;

        [SerializeField]
        private Text resultText;

        private void OnEnable()
        {
            if (resultText != null)
            {
                resultText.text = string.Empty;
            }

            Subscribe(player);
            Subscribe(enemy);
        }

        private void OnDisable()
        {
            Unsubscribe(player);
            Unsubscribe(enemy);
        }

        private void Subscribe(CombatantBehaviour combatant)
        {
            if (combatant == null)
            {
                return;
            }

            combatant.Health.HealthDepleted += OnHealthDepleted;
        }

        private void Unsubscribe(CombatantBehaviour combatant)
        {
            if (combatant == null)
            {
                return;
            }

            combatant.Health.HealthDepleted -= OnHealthDepleted;
        }

        private void OnHealthDepleted(object sender, HealthDepletedEventArgs e)
        {
            if (resultText == null)
            {
                return;
            }

            if (sender == player?.Health)
            {
                resultText.text = "Defeat";
            }
            else if (sender == enemy?.Health)
            {
                resultText.text = "Victory";
            }
        }
    }
}
