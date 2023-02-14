namespace ChatterObservable;
public class MessageSubject : Subject
{
    private MessageModel message = new();

    public MessageModel Message
    {
        get => message;
        set
        {
            message = value;
            Notify();
        }
    }
}