namespace CodeBase.DataSaver.Interfaces
{
    public interface ILoader
    {
        public bool Load<T>(out T result);
    }
}