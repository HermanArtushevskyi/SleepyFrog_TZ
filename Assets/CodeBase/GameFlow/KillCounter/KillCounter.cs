using System;
using CodeBase.Common;
using UnityContext.Interfaces;
using UnityEngine;

namespace CodeBase.GameFlow.KillCounter
{
    public class KillCounter
    {
        public int CurrentKills
        {
            get => _kills;
            set
            {
                KillsChanged?.Invoke(value);
                CheckCombo();
                _secondsSinceLastKill = 0;
                _kills = value;
            }
        }
        
        public event Action<int> ComboPerformed;

        public event Action<int> KillsChanged;

        private int _kills;
        private float _secondsSinceLastKill;
        private float _maxComboDelay;
        private float _comboKills;
        
        public KillCounter(IUpdateCallback updateCallback, GameSettings settings)
        {
            updateCallback.OnUpdate += OnUpdate;
            _maxComboDelay = settings.MaxComboDelay;
        }

        private void OnUpdate()
        {
            _secondsSinceLastKill += Time.deltaTime;
        }

        private void CheckCombo()
        {
            if (_secondsSinceLastKill > _maxComboDelay)
            {
                _comboKills = 1;
                return;
            }
            
            _comboKills++;
            ComboPerformed?.Invoke((int)_comboKills);
        }
    }
}