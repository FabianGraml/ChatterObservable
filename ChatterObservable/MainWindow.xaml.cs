using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
namespace ChatterObservable;
public partial class MainWindow : Window, IObserver
{
    private MessageSubject messageSubject = new MessageSubject();
    public ObservableCollection<MessageModel> Messages { get; set; } = new();
    public ObservableCollection<MessageModel> Connections { get; set; } = new();
    public string? ClientName => Username.Text.ToString();
    public MainWindow()
    {
        InitializeComponent();
        this.messageSubject.Attach(this);
        DataContext = this;

    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var window = new Chatter(messageSubject, ClientName!);
        window.Show();
    }
    public void Update()
    {
        Messages.Add(messageSubject.Message);
    }
    public void ClientAttach(string name)
    {
        lbl_userCount.Content = messageSubject.observers.Count();
        var res = name == "" ? "Server" : name;

        Connections.Add(new MessageModel()
        {
            Message = "attached",
            Username = res,
        });
    }
    public void ClientDetach(string name)
    {
        Connections.Add(new MessageModel()
        {
            Message = "detached",
            Username = name,
        });
        lbl_userCount.Content = messageSubject.observers.Count();
    }
}