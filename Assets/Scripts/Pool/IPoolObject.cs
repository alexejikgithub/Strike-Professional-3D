namespace Scripts.Pool
{
    public interface IPoolObject
    {
        public void SetPool(ObjectPoolController pool);
        public void RemoveObject();
    }
}