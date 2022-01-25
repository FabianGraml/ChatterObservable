using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatterObservable
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IObserver
    {
        private MessageSubject messageSubject = new MessageSubject();
        public ObservableCollection<MessageModel> Messages { get; set; } = new();
        public ObservableCollection<MessageModel> Connections { get; set; } = new();

        public MainWindow()
        {
            InitializeComponent();
            this.messageSubject.Attach(this);
            DataContext = this;

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = Username.Text;
            var window = new Chatter(messageSubject, userName);
            window.Show();
        }

        public void Update()
        {
            Messages.Add(messageSubject.Message);
        }

        public void ClientAttach()
        {
            lbl_userCount.Content = messageSubject.observers.Count();
            var res = Username.Text == "" ? "Server" : Username.Text.ToString();

            Connections.Add(new MessageModel()
            {
                Message = "attached",
                Username = res,
            });
            
        }
        public void ClientDetach()
        {
            lbl_userCount.Content = messageSubject.observers.Count();
        }

    }
}
