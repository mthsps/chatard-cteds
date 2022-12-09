using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chatard.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _error;
        bool _isVisible = true;

        public string Username 
        { 
            get
            {
                return _username;
            }  
            set => _username = value; 
        }
        public string Password { get => _password; set => _password = value; }
        public string Error { get => _error; set => _error = value; }
        public bool IsVisible { get => _isVisible; set => _isVisible = value; }
    }
}
