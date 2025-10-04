using System;

namespace DuelArena.Core
{
    /// <summary>
    /// Represents a mutable health container that raises domain events on change.
    /// </summary>
    public sealed class Health : IHealth
    {
        private int _maximum;
        private int _current;

        /// <inheritdoc />
        public event EventHandler<HealthChangedEventArgs> HealthChanged;

        /// <inheritdoc />
        public event EventHandler<HealthDepletedEventArgs> HealthDepleted;

        /// <inheritdoc />
        public int Current => _current;

        /// <inheritdoc />
        public int Maximum => _maximum;

        /// <inheritdoc />
        public bool IsDepleted => _current <= 0;

        /// <inheritdoc />
        public void Initialize(int maximum)
        {
            if (maximum <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(maximum), "Maximum health must be positive.");
            }

            _maximum = maximum;
            _current = maximum;
            OnHealthChanged(_current, _current);
        }

        /// <inheritdoc />
        public void Heal(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Heal amount must be non-negative.");
            }

            if (_maximum == 0)
            {
                throw new InvalidOperationException("Health must be initialized before use.");
            }

            var previous = _current;
            _current = Math.Min(_maximum, _current + amount);
            OnHealthChanged(previous, _current);
        }

        /// <inheritdoc />
        public void Damage(int amount)
        {
            if (amount < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(amount), "Damage amount must be non-negative.");
            }

            if (_maximum == 0)
            {
                throw new InvalidOperationException("Health must be initialized before use.");
            }

            var previous = _current;
            _current = Math.Max(0, _current - amount);
            OnHealthChanged(previous, _current);

            if (_current == 0)
            {
                OnHealthDepleted(_current);
            }
        }

        private void OnHealthChanged(int previous, int current)
        {
            HealthChanged?.Invoke(this, new HealthChangedEventArgs(previous, current, _maximum));
        }

        private void OnHealthDepleted(int current)
        {
            HealthDepleted?.Invoke(this, new HealthDepletedEventArgs(current));
        }
    }
}
