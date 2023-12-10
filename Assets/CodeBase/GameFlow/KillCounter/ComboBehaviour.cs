using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CodeBase.GameFlow.KillCounter
{
    public class ComboBehaviour : MonoBehaviour
    {
        [SerializeField] private float _liveTime;
        [SerializeField] private Vector2 _movementDirection;
        [SerializeField] private TextMeshPro _killsText;

        public Action<ComboBehaviour> OnFinish;

        private void OnEnable()
        {
            Vector3 target = transform.position + (Vector3)_movementDirection;
            var animation = transform.DOMove(transform.position + target, _liveTime);
            animation.onComplete += () => OnFinish?.Invoke(this);
        }

        public void SetKillCounter(int kills)
        {
            _killsText.text = $"{kills} COMBO";
        }
    }
}