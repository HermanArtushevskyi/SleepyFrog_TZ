using System;

namespace UnityContext.Interfaces
{
    public interface IUpdateCallback
    {
        public event Action OnUpdate;
    }
}