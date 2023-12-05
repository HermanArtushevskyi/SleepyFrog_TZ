using System;
using System.Collections;
using UnityContext.Interfaces;
using UnityEngine;

namespace UnityContext
{
    public class MonoContext : MonoBehaviour, ICoroutineRunner, IUpdateCallback, IFixedUpdateCallback
    {
        public void RunCoroutine(IEnumerator coroutine) => StartCoroutine(coroutine);
        
        public event Action OnFixedUpdate;
        public event Action OnUpdate;
        
        private void Update() => OnUpdate?.Invoke();
        private void FixedUpdate() => OnFixedUpdate?.Invoke();
    }
}