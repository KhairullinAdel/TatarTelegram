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
    /// Interaction logic for FilterPage.xaml
    /// </summary>
    public partial class FilterPage : Page
    {
        public List<Company> Departments { get; set; }
        public List<Chatter> Employees { get; set; }
        public Chat Chatroom { get; set; }
        public FilterPage()
        {
            InitializeComponent();
            Departments = DataAccess.GetCompanys();
            Employees = new List<Chatter>();
            DataContext = this;
        }

        public FilterPage(Chat chatroom)
        {
            Chatroom = chatroom;
            Departments = DataAccess.GetCompanys();
            Employees = new List<Chatter>();
            DataContext = this;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }


        private void cbDepartment_Click(object sender, RoutedEventArgs e)
        {
            Employees = new List<Chatter>();
            foreach (Company comp in Departments)
            {
                if (comp.IsChecked)
                    Employees.AddRange(comp.Chatter);
            }
            lvEmployees.ItemsSource = Employees;
            lvEmployees.Items.Refresh();
        }

        private void lvEmployees_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var selectedMember = (sender as ListView).SelectedItem as Chatter;
            if (Chatroom != null)
            {
                Chatroom.Chat_Chatter.Add(new Chat_Chatter() { Chat = Chatroom.Id, Chatter = selectedMember.Id });
                NavigationService.Navigate(new ChatPage(Chatroom));
            }
            else
            {
                NavigationService.Navigate(new ChatPage(
                    new Chat
                    {
                        Chat_Chatter = new List<Chat_Chatter>
                    {
                        new Chat_Chatter{Chatter1 = App.chatter},
                        new Chat_Chatter{Chatter1 = selectedMember},
                    }
                    }));
            }
        }
    }
}
