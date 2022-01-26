using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
namespace ChatterObservable;

public partial class Chatter : Window, INotifyPropertyChanged, IObserver
{
    private readonly MessageSubject subject;
    private MessageModel message = new();
    public ObservableCollection<MessageModel> Messages { get; set; } = new();
    public ObservableCollection<MessageModel> Connections { get; set; } = new();

    public string? ClientName { get; set; }

    public Chatter(MessageSubject subject, string clientName)
    {
        InitializeComponent();
        this.subject = subject;
        ClientName = clientName;
        UserName.Content = clientName;
        DataContext = this;
        subject.Attach(this);
    }
    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
    public void Update()
    {
        if (!CheckAccess())
        {
            Dispatcher.Invoke(Update);
        }
        Messages.Add(subject.Message);
    }
    private void Button_Click(object sender, RoutedEventArgs e)
    {
        var msg = new MessageModel()
        {
            Message = tbx_Message.Text,
            Username = ClientName
        };
        subject.Message = msg;
    }

    public void ClientAttach(string name)
    {
        Messages.Add(new MessageModel
        {
            Message = "has connected",
            Username = name
        });
    }
    public void ClientDetach(string name)
    {
        Messages.Add(new MessageModel
        {
            Message = "has disconnected",
            Username = name
        });
    }
    private void Window_Closing(object sender, CancelEventArgs e)
    {
        this.subject.Detach(this);
    }
}