using System;
using CodeBase.Common;

namespace CodeBase.GameFlow.HealthCounter
{
    public class HealthCounter
    {
        public int MaxHealth => _maxHealth;
        public int CurrentHealth
        {
            get => _health;
            set
            {
                HealthChanged?.Invoke(value);
                _health = value;
            }
        }
        
        public event Action<int> HealthChanged;

        private int _health;
        private int _maxHealth;

        public HealthCounter(GameSettings settings)
        {
            _maxHealth = settings.HealthAmount;
            _health = _maxHealth;
        }
        
        public void TakeDamage(int damage)
        {
            CurrentHealth -= damage;
        }
    }
}