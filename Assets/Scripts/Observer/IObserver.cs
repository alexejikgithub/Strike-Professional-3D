namespace Scripts.Observer
{
    public interface IObserver<T>
    {
        void UpdateObservableData(T variable);
    }
}