namespace CodeBase.ObjectPooling
{
    public interface IPool<T>
    {
        public T Get();
        public void Return(T item);
    }
}