using System.Collections;
using CodeBase.Common;
using CodeBase.ObjectPooling;
using CodeBase.Settings;
using UnityEngine;
using Zenject;

namespace CodeBase.Audio
{
    public class AudioPlayer
    {
        private readonly IPool<AudioSource> _audioPool;
        private readonly SettingsStorage _settingsStorage;

        public AudioPlayer(
            [InjectOptional(Id = PrefabId.AudioSource)] GameObject audioSourcePrefab,
            Factories.Interfaces.IFactory<GameObject, GameObject> gameObjectFactory,
            SettingsStorage settingsStorage)
        {
            _audioPool = new AudioSourcePool(audioSourcePrefab, 10, gameObjectFactory);
            _settingsStorage = settingsStorage;
        }
        
        public void Play(AudioClip audioClip)
        {
            if (!_settingsStorage.SoundEnabled.Value)
                return;
            
            AudioSource source = _audioPool.Get();
            source.clip = audioClip;
            source.Play();
        }

        private IEnumerator ReturnCoroutine(AudioSource source)
        {
            yield return new WaitForSeconds(source.clip.length);
            _audioPool.Return(source);
        }
    }
}