using UnityContext.Interfaces;
using UnityEngine;

namespace CodeBase.GameFlow.GameTimer
{
    public class Timer
    {
        public float FixedTickCount => _fixedTickCount;
        public float TickCount => _tickCount;
        public float Seconds => _seconds;
        
        private float _fixedTickCount;
        private float _tickCount;
        private float _seconds;
        
        private readonly IFixedUpdateCallback _fixedUpdateCallback;
        private readonly IUpdateCallback _updateCallback;

        public Timer(IFixedUpdateCallback fixedUpdateCallback, IUpdateCallback updateCallback)
        {
            _fixedUpdateCallback = fixedUpdateCallback;
            _updateCallback = updateCallback;
        }

        public void Start()
        {
            _fixedUpdateCallback.OnFixedUpdate += OnFixedUpdate;
            _updateCallback.OnUpdate += OnUpdate;
        }
        
        public void Stop()
        {
            _fixedUpdateCallback.OnFixedUpdate -= OnFixedUpdate;
            _updateCallback.OnUpdate -= OnUpdate;
        }

        private void OnFixedUpdate()
        {
            _fixedTickCount++;
        }

        private void OnUpdate()
        {
            _tickCount++;
            _seconds += Time.deltaTime;
        }
    }
}