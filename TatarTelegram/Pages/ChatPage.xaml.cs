using System;
using System.Collections.Generic;
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
using TatarTelegram.Data;

namespace TatarTelegram.Pages
{
    /// <summary>
    /// Interaction logic for ChatPage.xaml
    /// </summary>
    public partial class ChatPage : Page
    {
        public Chat Chatroom { get; set; }
        public ChatPage(Chat chatroom)
        {
            InitializeComponent();
            Chatroom = chatroom;
            DataAccess.RefreshListEvent += DataAccess_RefreshListEvent;
            DataContext = this;
        }

        private void DataAccess_RefreshListEvent()
        {
            lvMembers.ItemsSource = Chatroom.Chat_Chatter;
            lvMembers.Items.Refresh();

            lvMessages.ItemsSource = DataAccess.GetChatMessages(Chatroom);
            lvMessages.Items.Refresh();
        }

        private void btnAddUser_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Pages.FilterPage(Chatroom));
        }

        private void btnChangeTopic_Click(object sender, RoutedEventArgs e)
        {
            new ChangeName(Chatroom).ShowDialog();
        }

        private void btnLeaveChat_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.LeaveChat(App.chatter, Chatroom);
            NavigationService.GoBack();
        }

        private void btnSendMessage_Click(object sender, RoutedEventArgs e)
        {
            var message = new Message 
            { 
                Chat1 = Chatroom, 
                Chatter = App.chatter,
                Message1 = tbMessage.Text,
                Date = DateTime.Now,
            };

            DataAccess.SaveChatMessage(message);
        }
    }
}
