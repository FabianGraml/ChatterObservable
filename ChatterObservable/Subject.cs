using System.Collections.Generic;
namespace ChatterObservable;
public abstract class Subject
{
    public List<IObserver> observers = new();

    public void Attach(IObserver observer)
    {
        observers.Add(observer);
        observers.ForEach(x => x.ClientAttach(observer.ClientName!));
    }
    public void Detach(IObserver observer)
    {
        observers.Remove(observer);
        observers.ForEach(x => x.ClientDetach(observer.ClientName!));
    }
    public void Notify() => observers.ForEach(o => o.Update());
}