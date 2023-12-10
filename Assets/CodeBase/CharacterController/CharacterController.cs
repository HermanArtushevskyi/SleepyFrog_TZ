using System.Collections;
using CodeBase.Audio;
using CodeBase.Common;
using CodeBase.Input.Common;
using CodeBase.Input.Interfaces;
using UnityContext.Interfaces;
using UnityEngine;
using Zenject;

namespace CodeBase.CharacterController
{
    public class CharacterController : MonoBehaviour
    {
        public bool AttackedThisFrame => _secondsFromLastAttack <= ATTACK_DURATION;

        private IInputProvider _inputProvider;
        private IUpdateCallback _update;
        private InputData _inputData;
        private GameObject _sword;
        private Animator _swordAnimator;
        private float _secondsFromLastAttack;
        private AudioPlayer _audioPlayer;
        private AudioClip _swordAudioClip;

        private const string SWORD_HIT = "weapon_sword_side";
        private const float ATTACK_DURATION = 0.5f;
        
        [Inject]
        private void Construct(IInputProvider inputProvider, IUpdateCallback update,
            [InjectOptional(Id = PrefabId.SwordSwingAudioClip)] AudioClip swordAudioClip,
            AudioPlayer audioPlayer)
        {
            _inputProvider = inputProvider;
            _update = update;
            _sword = gameObject.transform.GetChild(0).gameObject;
            _swordAnimator = _sword.GetComponent<Animator>();
            _audioPlayer = audioPlayer;
            _swordAudioClip = swordAudioClip;
        }

        private void Start()
        {
            _update.OnUpdate += OnFixedUpdate;
        }
        
        private void OnDestroy()
        {
            _update.OnUpdate -= OnFixedUpdate;
        }

        private void OnFixedUpdate()
        {
            _inputData = _inputProvider.GetInput();
            Look();
            Hit();
        }

        private void Look()
        {
            if (_inputData.Horizontal != 0)
            {
                transform.rotation = Quaternion.Euler(0, _inputData.Horizontal > 0 ? 180 : 0, 0);
            }
        }

        private void Hit()
        {
            if (!_inputData.Spacebar)
            {
                _secondsFromLastAttack += Time.deltaTime;
                return;
            }
            _audioPlayer.Play(_swordAudioClip);
            _secondsFromLastAttack = 0;
            
            _sword.SetActive(true);
            _swordAnimator.Play(SWORD_HIT);
        }
    }
}