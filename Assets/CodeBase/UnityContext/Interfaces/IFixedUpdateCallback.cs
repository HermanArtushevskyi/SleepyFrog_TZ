using System;

namespace UnityContext.Interfaces
{
    public interface IFixedUpdateCallback
    {
        public event Action OnFixedUpdate;
    }
}