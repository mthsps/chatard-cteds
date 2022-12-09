using chatard.DataAccess;
using chatard.Models;
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

namespace chatard.Views
{
    /// <summary>
    /// Interaction logic for ChatView.xaml
    /// </summary>
    public partial class ChatView : Window
    {

        Context context = new Context();
        public ChatView()
        {
            InitializeComponent();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_1(object sender, TextChangedEventArgs e)
        {

        }

        private void TextBox_TextChanged_2(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            User user = new User();
            user.Username = "Alas";

            User user2 = new User();
            user2.Username = "Alas2";

            UserContacts userContacts = new UserContacts();

            userContacts.User = user;
            userContacts.Contact = user2;
            

            Message message = new Message();
            message.Content = "Hello";
            message.Sender = user;
            message.Receiver = user2;


            context.Users.Add(user);
            context.Users.Add(user2);
            context.UserContacts.Add(userContacts);
            context.Messages.Add(message);


            context.SaveChanges();

            List<User> usersAfterSave = context.Users.ToList();

            List<Message> messagesAfterSave = context.Messages.ToList();

            List<UserContacts> userContactsAfterSave = context.UserContacts.ToList();

            MessageBox.Show(usersAfterSave[0].UserId.ToString());
            MessageBox.Show(messagesAfterSave[0].Content);
            MessageBox.Show(userContactsAfterSave[0].User.UserId.ToString()); ;
            


            MessageBox.Show("Test finished");

        }
    }
}
