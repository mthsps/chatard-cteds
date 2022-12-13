using chatard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace chatard.ViewModels
{
    public class RegisterViewModel : ViewModelBase
    {
        private string _username;
        private string _password;
        private string _confirmpassword;
        private string _email;
        private string _passworderror;
        private string _emailorusernameerror;
        bool _isVisible = true;

        public string Username
        {
            get
            {
                return _username;
            }
            set
            {
                _username = value;
                NotifyPropertyChanged(nameof(Username));
            }
        }
        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
                NotifyPropertyChanged(nameof(Password));
            }
        }
        public string ConfirmPassword
        {
            get
            {
                return _confirmpassword;
            }
            set
            {
                _confirmpassword = value;
                NotifyPropertyChanged(nameof(ConfirmPassword));
            }
        }
        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
                NotifyPropertyChanged(nameof(Email));
            }
        }
        public string PasswordError
        {
            get
            {
                return _passworderror;
            }
            set
            {
                _passworderror = value;
                NotifyPropertyChanged(nameof(PasswordError));
            }
        }
        public string EmailOrUsernameError
        {
            get
            {
                return _email;
            }
            set
            {
                _emailorusernameerror = value;
                NotifyPropertyChanged(nameof(EmailOrUsernameError));
            }
        }
        public bool IsVisible
        {
            get
            {
                return _isVisible;
            }
            set
            {
                _isVisible = value;
                NotifyPropertyChanged(nameof(IsVisible));
            }
        }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            RegisterCommand = new ViewModelCommand(ExecuteRegisterCommand, CanExecuteRegisterCommand);
        }

        private void ExecuteRegisterCommand(object obj)
        {
            bool isValidUser = true;
            foreach (var user in context.Users)
            {
                if (user.Username == _username && user.Email == _email)
                {
                    isValidUser = false;
                    EmailOrUsernameError = "* Username or email profile already exists";
                }
            }
            if(_password != _confirmpassword)
                isValidUser = false;

            if (isValidUser)
            {
                User user = new User();
                user.Username = Username;
                user.Email = Email;
                user.Password = Password;
                context.Users.Add(user);
                context.SaveChanges();
                IsVisible = false;
            }
        }

        private bool CanExecuteRegisterCommand(object obj)
        {
            bool isValid;
            if (string.IsNullOrWhiteSpace(Username) || Password == null || ConfirmPassword == null || string.IsNullOrWhiteSpace(Email) )
                isValid = false;
            else
                isValid = true;
            return isValid;
        }
    }
}
