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

namespace ChatterObservable
{
    /// <summary>
    /// Interaction logic for Chatter.xaml
    /// </summary>
    public partial class Chatter : Window, INotifyPropertyChanged, IObserver
    {
        private readonly MessageSubject subject;
        private MessageModel message = new();
        private string userName;
        public ObservableCollection<MessageModel> Messages { get; set; } = new();
        public ObservableCollection<MessageModel> Connections { get; set; } = new();

        public Chatter(MessageSubject subject, string userName)
        {
            InitializeComponent();
            this.userName = userName;
            this.subject = subject;
            UserName.Content = userName;
            DataContext = this; 
            subject.Attach(this);
        }
        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
      //public MessageModel Message { get => message; set { message = value ?? null!; OnPropertyChanged(nameof(message)); } }
        public void Update()
        {
            if (!CheckAccess())
            {
                Dispatcher.Invoke(Update);
            }
            // Message = subject.Message;
            Messages.Add(subject.Message);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var msg = new MessageModel()
            {
                Message = tbx_Message.Text,
                Username = userName
            };
            subject.Message = msg;
        }

        public void ClientAttach()
        {
            Messages.Add(new MessageModel
            {
                Message = "has connected",
                Username = userName
            });
        }
        public void ClientDetach()
        {
            Messages.Add(new MessageModel
            {
                Message = "has disconnected",
                Username = userName
            });
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            this.subject.Detach(this);
        }
    }
}
