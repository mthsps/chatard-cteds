using chatard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace chatard.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<User> Users { get; set; }
        public ObservableCollection<Message> Messages { get; set; }

        public ViewModelBase()
        {
            Users = new ObservableCollection<User>();
            Messages = new ObservableCollection<Message>();

            for (int i = 0; i < 10; i++)
            {
                Users.Add(new User() { 
                    Username = "User " + i, 
                    Password = "Password " + i,
                    Email = "Email " + i, 
                    ProfilePicture = "ProfilePicture " + i });
            } 
        }
                        


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
