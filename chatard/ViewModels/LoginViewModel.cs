﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

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
        public string Error 
        {
            get
            {
                return _error;
            }
            set
            {
                _error = value;
                NotifyPropertyChanged(nameof(Error));
            } 
        }
        public bool IsVisible {
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

        public ICommand LoginCommand { get; }
        public ICommand ShowPasswordCommand { get; }
        public ICommand RememberPasswordCommand { get; }
        public ICommand RecoverPasswordCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand, CanExecuteLoginCommand);
        }

        private void ExecuteLoginCommand(object obj)
        {

        }
            
        private bool CanExecuteLoginCommand(object obj)
        {
            bool isValid;
            if (string.IsNullOrWhiteSpace(Username) || Password == null)
                isValid = false;
            else 
                isValid = true;
            return isValid;
        }
    }
}