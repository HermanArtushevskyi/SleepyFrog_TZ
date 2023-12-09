using UnityEngine;

namespace CodeBase.Factories.Interfaces
{
    public interface IFactory<TProduct>
    {
        public TProduct Create();
    }
    
    public interface IFactory<TProduct, in TParam>
    {
        public TProduct Create(TParam param);
    }
    
    public interface IFactory<TProduct, in TParam1, in TParam2>
    {
        public TProduct Create(TParam1 param1, TParam2 param2);
    }
    
    public interface IFactory<TProduct, in TParam1, in TParam2, in TParam3>
    {
        public TProduct Create(TParam1 param1, TParam2 param2, TParam3 param3);
    }
    
    public interface IGameObjectFactory<T, TParam1, TParam2> : IFactory<T, TParam1, TParam2>
    {
        public void AddPool(GameObject prefab, int initialSize);
    }
}