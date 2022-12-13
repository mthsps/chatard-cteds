using chatard.DataAccess;
using chatard.Models;
using chatard.Views;
using Microsoft.Win32;
using System.Threading;
using System.Windows;

namespace chatard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            Login loginView = new Login();
            loginView.Show();
            loginView.IsVisibleChanged += (s, ev) =>
            {
                /*if (loginView.IsVisible == false && loginView.Register ==)
                {
                    RegisterView registerview = new RegisterView();
                }*/
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    ChatView chatView = new ChatView();
                    chatView.Show();
                    loginView.Close();
                    foreach (User user in chatView.context.Users)
                    {
                        if (user.Username == Thread.CurrentPrincipal.Identity.Name)
                        {
                            User currentUser = user;
                            MessageBox.Show($"Welcome {user.Username}");
                        }
                        
                    }
                } 
            };
        }
    }

}

