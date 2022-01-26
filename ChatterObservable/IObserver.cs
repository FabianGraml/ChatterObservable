namespace ChatterObservable;

public interface IObserver
{
    public string? ClientName { get; }

    public void Update();
    public void ClientAttach(string name);
    public void ClientDetach(string name);

}
