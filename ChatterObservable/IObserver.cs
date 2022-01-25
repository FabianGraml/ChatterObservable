namespace ChatterObservable;

public interface IObserver
{
    public void Update();
    public void ClientAttach();
    public void ClientDetach();

}
