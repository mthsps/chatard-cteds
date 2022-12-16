using chatard.Models;
using chatard.Views;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace chatard.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
       
        public User? LoggedUser { get; set; }
        private ObservableCollection<User> _contacts;
        private ObservableCollection<Message> _messagesWithSelectedContact;
        private User _selectedContact;
        private string _messageToSend;
        private string _addContact;
        private bool _isVisible;

        public User SelectedContact
        {
            get
            {
                return _selectedContact;
            }
            set
            {
                _selectedContact = value;
                NotifyPropertyChanged(nameof(SelectedContact));
                GetMessagesWithSelectedContact();
            }
        }

        public string MessageToSend
        {
            get
            {
                return _messageToSend;
            }
            set
            {
                _messageToSend = value;
                NotifyPropertyChanged(nameof(MessageToSend));
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

        public string ContactToAdd
        {
            get
            {
                return _addContact;
            }
            set
            {
                _addContact = value;
                NotifyPropertyChanged(nameof(ContactToAdd));
            }
        }

        public ObservableCollection<User> Contacts
        {
            get
            {
                return _contacts;
            }
            set
            {
                _contacts = value;
                NotifyPropertyChanged(nameof(Contacts));
            }
        }

        public ObservableCollection<Message> MessagesWithSelectedContact
        {
            get
            {
                return _messagesWithSelectedContact;
            }
            set
            {
                _messagesWithSelectedContact = value;
                NotifyPropertyChanged(nameof(MessagesWithSelectedContact));
            }
        }


        public ICommand SendMessageCommand { get; }
        public ICommand LogoffCommand { get; }
        public ICommand AddCommand { get; }

        HubConnection connection;
        
        public ChatViewModel()
        {

            LoggedUser = context.Users
                .Where(u => u.Username == Thread.CurrentPrincipal.Identity.Name)
                .FirstOrDefault();



            //SignalR Server
            connection = new HubConnectionBuilder()
               .WithUrl("http://localhost:5134/ChatHub")
               .Build();

            
            connection.On<string, string>("ReceiveMessage", (sender, receiver) =>
            {
                if (receiver == LoggedUser.Username)
                {
                    GetMessagesWithSelectedContact();
                }
            });


            connection.On<string, string>("AddNewContact", (sender, receiver) =>
            {
                if (receiver == LoggedUser.Username)
                {
                    Contacts = ConvertContactsToUsers(GetUserContacts());

                }
            });

            connection.StartAsync();


            List<UserContacts> userContacts = GetUserContacts();
            

            _contacts = ConvertContactsToUsers(userContacts);

            if (_contacts.Count > 0)
            {
                _selectedContact = _contacts[0];
                GetMessagesWithSelectedContact();
            }

            SendMessageCommand = new ViewModelCommand(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);
            LogoffCommand = new ViewModelCommand(ExecuteLogoffCommand, CanExecuteLogoffCommand);
            AddCommand = new ViewModelCommand(ExecuteAddCommand, CanExecuteAddCommand);

        }


        private async void ExecuteSendMessageCommand(object obj)
        {

            if (SelectedContact == null)
            {
                MessageBox.Show("Please add or select a contact to send a message");
                return;
            }

            Message message = new Message();
            message.Content = MessageToSend;
            message.Sender = LoggedUser;
            message.Receiver = SelectedContact;

            context.Messages.Add(message);

            context.SaveChanges();

            try
            {
                await connection.InvokeAsync("SendMessage",
                    LoggedUser.Username, SelectedContact.Username);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Sorry, something went wrong. Please try again later.");

            }

            MessagesWithSelectedContact.Add(message);

            GetMessagesWithSelectedContact();
            MessageToSend = String.Empty;
        }

        private bool CanExecuteSendMessageCommand(object obj)
        {
            if (string.IsNullOrWhiteSpace(MessageToSend))
                return false;
            else
                return true;
        }

        private ObservableCollection<User> ConvertContactsToUsers(List<UserContacts> userContacts)
        {
            ObservableCollection<User> contacts = new ObservableCollection<User>();
            foreach (var contact in userContacts)
            {
                if (contact.Contact != null && contact.Contact.UserId != LoggedUser.UserId)
                {
                    contacts.Add(contact.Contact);
                }
                else if (contact.User != null && contact.User.UserId != LoggedUser.UserId)
                {
                    contacts.Add(contact.User);
                }

            }
            return contacts;
        }

        private void ExecuteLogoffCommand(object obj)
        {
            LoggedUser = null;
            Thread.CurrentPrincipal = null;
            IsVisible = false;
            Login login = new Login();
            login.Show();
        }

        private bool CanExecuteLogoffCommand(object obj)
        {
            return true;
        }


        private async void ExecuteAddCommand(object obj)
        {
          
            if (_addContact == LoggedUser.Email)
            {
                MessageBox.Show("You can't add yourself as a contact");
                return;
            }

            var user = context.Users.
                Where(u => u.Email == _addContact).FirstOrDefault();

            if (user != null)
            {
                
                UserContacts userContact = new UserContacts();
                userContact.UserId = LoggedUser.UserId;
                userContact.ContactId = user.UserId;
                userContact.User = LoggedUser;
                userContact.Contact = user;
                
                context.UserContacts.Add(userContact);
                context.SaveChanges();
                
                ContactToAdd = string.Empty;
                
                MessageBox.Show("Contact added with success");

                await connection.InvokeAsync("AddNewContact",
                    LoggedUser.Username, user.Username);

                Contacts = ConvertContactsToUsers(GetUserContacts());
            }
            else
            {
                MessageBox.Show("Contact not found! Try again...");
                return;
            }
        }

        private bool CanExecuteAddCommand(object obj)
        {
            
            if (string.IsNullOrWhiteSpace(ContactToAdd))
                return false;
            else
                return true;
        }

        private void GetMessagesWithSelectedContact()
        {

            List<Message> messagesWithSelectedContact = context.Messages.
                Where(m => (m.Sender.UserId == LoggedUser.UserId && m.Receiver.UserId == SelectedContact.UserId)
                || (m.Sender.UserId == SelectedContact.UserId && m.Receiver.UserId == LoggedUser.UserId))
                .ToList();


            messagesWithSelectedContact.Sort((x, y) => DateTime.Compare(x.SendTime, y.SendTime));

            MessagesWithSelectedContact = new ObservableCollection<Message>(messagesWithSelectedContact);

        }
        private List<UserContacts> GetUserContacts()
        {
            return context.UserContacts.
            Where(u => u.User.UserId == LoggedUser.UserId
            || u.Contact.UserId == LoggedUser.UserId
            || u.UserId == LoggedUser.UserId || u.ContactId == LoggedUser.UserId)
            .ToList();

        }

        
    }

}