using System;
using CodeBase.DataSaver.Interfaces;
using CodeBase.Input.Interfaces;
using CodeBase.SceneManagement.Common;
using CodeBase.SceneManagement.Interfaces;
using UnityContext.Interfaces;

namespace CodeBase.GameFlow.ResultManager
{
    public class ResultManager
    {
        public event Action OnGameOver;

        private readonly KillCounter.KillCounter _killCounter;
        private readonly ISaver _saver;
        private readonly ILoader _loader;
        private readonly ISceneSwitcher _sceneSwitcher;
        private readonly IUpdateCallback _updateCallback;
        private readonly IInputProvider _inputProvider;

        public ResultManager(
            HealthCounter.HealthCounter healthCounter,
            KillCounter.KillCounter killCounter,
            ISaver saver,
            ILoader loader,
            ISceneSwitcher sceneSwitcher,
            IUpdateCallback updateCallback,
            IInputProvider inputProvider)
        {
            healthCounter.HealthChanged += OnHealthChanged;
            _killCounter = killCounter;
            _saver = saver;
            _loader = loader;
            _sceneSwitcher = sceneSwitcher;
            _updateCallback = updateCallback;
            _inputProvider = inputProvider;
        }

        private void OnHealthChanged(int obj)
        {
            if (obj > 0) return;
            GameOver();
        }

        private void GameOver()
        {
            OnGameOver?.Invoke();
            Result result = null; 

            if (_loader.Load<Result>(out result))
            {
                if (_killCounter.CurrentKills > result.Kills)
                {
                    result.Kills = _killCounter.CurrentKills;
                    _saver.Save(result);
                }
            }
            else
            {
                _saver.Save(new Result {Kills = _killCounter.CurrentKills});
            }
            
            _updateCallback.OnUpdate += OnUpdate;
        }

        private void OnUpdate()
        {
            if (_inputProvider.GetInput().Spacebar)
            {
                _updateCallback.OnUpdate -= OnUpdate;
                _sceneSwitcher.LoadScene(SceneNames.Menu);
            }
        }
    }
}