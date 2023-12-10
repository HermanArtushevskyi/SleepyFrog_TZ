using CodeBase.Common;
using CodeBase.GameFlow.KillCounter;
using CodeBase.ObjectPooling;
using UnityEngine;
using Zenject;

namespace CodeBase.Factories
{
    public class ComboFactory : Interfaces.IFactory<ComboBehaviour, Vector3, int>
    {
        private readonly GameObject _comboPrefab;
        private readonly IPool<ComboBehaviour> _comboPool;
        
        public ComboFactory(
            [InjectOptional(Id = PrefabId.Combo)] GameObject comboPrefab,
            Interfaces.IFactory<GameObject, GameObject> gameObjectFactory)
        {
            _comboPrefab = comboPrefab;
            _comboPool = new ComboPool(_comboPrefab, 10, gameObjectFactory);
        }
        
        public ComboBehaviour Create(Vector3 position, int kills)
        {
            ComboBehaviour instance = _comboPool.Get();
            instance.transform.position = position;
            instance.gameObject.SetActive(true);
            instance.SetKillCounter(kills);
            instance.OnFinish += combo => _comboPool.Return(combo);
            return instance;
        }
    }
}