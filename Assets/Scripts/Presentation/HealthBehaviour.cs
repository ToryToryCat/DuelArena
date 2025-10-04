using DuelArena.Core;
using UnityEngine;
using UnityEngine.UI;

namespace DuelArena.Presentation
{
    /// <summary>
    /// Synchronizes a UI slider with a combatant's health value.
    /// </summary>
    public sealed class HealthBehaviour : MonoBehaviour
    {
        [SerializeField]
        private CombatantBehaviour combatant;

        [SerializeField]
        private Slider slider;

        private void OnEnable()
        {
            if (combatant != null && slider != null)
            {
                Subscribe(combatant.Health);
            }
        }

        private void OnDisable()
        {
            if (combatant != null && slider != null)
            {
                Unsubscribe(combatant.Health);
            }
        }

        private void Subscribe(IReadOnlyHealth health)
        {
            slider.maxValue = health.Maximum;
            slider.value = health.Current;
            health.HealthChanged += OnHealthChanged;
        }

        private void Unsubscribe(IReadOnlyHealth health)
        {
            health.HealthChanged -= OnHealthChanged;
        }

        private void OnHealthChanged(object sender, HealthChangedEventArgs e)
        {
            if (slider == null)
            {
                return;
            }

            slider.maxValue = e.Maximum;
            slider.value = e.Current;
        }
    }
}
