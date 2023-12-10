using System.Collections.Generic;
using CodeBase.Common;
using CodeBase.Factories.Interfaces;
using CodeBase.GameFlow.HealthCounter;
using CodeBase.GameFlow.KillCounter;
using CodeBase.GameFlow.ResultManager;
using UnityEngine;
using Zenject;

namespace CodeBase.UI.Game
{
    public class GamePresenter
    {
        private readonly GameView _view;
        private readonly GameSettings _settings;
        private readonly KillCounter _killCounter;
        private readonly HealthCounter _healthCounter;
        private readonly ResultManager _resultManager;
        private readonly GameObject _healthPrefab;
        private readonly IFactory<ComboBehaviour, Vector3, int> _comboFactory;
        private readonly GameObject _playerOnScene;

        private readonly List<GameObject> _healthIcons;

        public GamePresenter(
            GameView view,
            GameSettings settings,
            KillCounter killCounter,
            HealthCounter healthCounter,
            ResultManager resultManager,
            [InjectOptional(Id = PrefabId.Health)] GameObject healthPrefab,
            IFactory<ComboBehaviour, Vector3, int> comboFactory,
            [InjectOptional(Id = SceneObjectId.Player)] GameObject playerOnScene)
        {
            _view = view;
            _settings = settings;
            _killCounter = killCounter;
            _healthCounter = healthCounter;
            _resultManager = resultManager;
            _healthPrefab = healthPrefab;
            _healthIcons = new List<GameObject>();
            _comboFactory = comboFactory;
            _playerOnScene = playerOnScene;
            InitiateUI();
            BindUI();
        }

        private void InitiateUI()
        {
            for (int i = 0; i < _healthCounter.MaxHealth; i++)
            {
                GameObject healthIcon = GameObject.Instantiate(_healthPrefab, _view.HealthPanel.transform);
                _healthIcons.Add(healthIcon);
                _view.KillsCountText.text = "x0";
            }
        }

        private void BindUI()
        {
            _killCounter.KillsChanged += UpdateKillsText;
            _killCounter.ComboPerformed += OnComboPerformed;
            _healthCounter.HealthChanged += UpdateHealthIcons;
            _resultManager.OnGameOver += OnGameOver;
        }

        private void UpdateKillsText(int kills)
        {
            _view.KillsCountText.text = $"x{kills}";
        }

        private void UpdateHealthIcons(int amount)
        {
            for (int i = 0; i < _healthIcons.Count; i++)
            {
                _healthIcons[i].SetActive(i < amount);
            }
        }

        private void OnGameOver()
        {
            _view.GameOverPanel.SetActive(true);
            _view.KillsCountGameOverText.text = $"Kills:{_killCounter.CurrentKills}";
        }

        private void OnComboPerformed(int killsAmount)
        {
            _comboFactory.Create(_playerOnScene.transform.position, killsAmount);
        }
    }
}