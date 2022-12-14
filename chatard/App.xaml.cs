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
            
        }
    }

}

