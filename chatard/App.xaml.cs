using chatard.Views;
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
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    ChatView chatView = new ChatView();
                    chatView.Show();
                    loginView.Close();
                }
            };
        }
    }

}

