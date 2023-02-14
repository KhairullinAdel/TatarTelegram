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
using System.Windows.Shapes;
using TatarTelegram.Data;

namespace TatarTelegram
{
    /// <summary>
    /// Логика взаимодействия для ChangeName.xaml
    /// </summary>
    public partial class ChangeName : Window
    {
        public Chat Chatroom { get; set; }
        public ChangeName(Chat chatroom)
        {
            InitializeComponent();
            Chatroom = chatroom;
            DataContext = Chatroom;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            DataAccess.SaveChat(Chatroom);
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
