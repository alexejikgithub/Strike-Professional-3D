public interface IObservable<T>
{
    void AddObserver(IObserver<T> o);
    void RemoveObserver(IObserver<T> o);
    void NotifyObservers();
}
