using chatard.Models;
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

        public User LoggedUser;
        private ObservableCollection<User> _contacts;
        private ObservableCollection<Message> _messagesWithSelectedContact;
        private User _selectedContact;
        private string _messageToSend;
        private bool _isVisible;

        public ICommand SendMessageCommand { get; }

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

            connection.StartAsync();

            List<UserContacts> userContacts = context.UserContacts.
                Where(u => u.User.UserId == LoggedUser.UserId
                || u.Contact.UserId == LoggedUser.UserId
                || u.UserId == LoggedUser.UserId || u.ContactId == LoggedUser.UserId)
                .ToList();

            _contacts = ConvertContactsToUsers(userContacts);

            _messagesWithSelectedContact = new ObservableCollection<Message>();

            SendMessageCommand = new ViewModelCommand(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);

        }


        

        private async void ExecuteSendMessageCommand(object obj)
        {

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
                MessageBox.Show(ex.Message);
                
            }


            MessagesWithSelectedContact.Add(message);

            GetMessagesWithSelectedContact();
            MessageToSend = String.Empty;
        }


        // Listens to the server for new messages and refreshes the messages list
        public async void ListenForNewMessages()
        {


            await connection.StartAsync();
        }

        


        

        private bool CanExecuteSendMessageCommand(object obj)
        {
            bool isValid;
            if (string.IsNullOrWhiteSpace(MessageToSend))
                isValid = false;
            else
                isValid = true;
            return isValid;
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

        private void GetMessagesWithSelectedContact()
        {

            List<Message> messagesWithSelectedContact = context.Messages.
                Where(m => (m.Sender.UserId == LoggedUser.UserId && m.Receiver.UserId == SelectedContact.UserId)
                || (m.Sender.UserId == SelectedContact.UserId && m.Receiver.UserId == LoggedUser.UserId))
                .ToList();


            messagesWithSelectedContact.Sort((x, y) => DateTime.Compare(x.SendTime, y.SendTime));

            MessagesWithSelectedContact = new ObservableCollection<Message>(messagesWithSelectedContact);

        }


        
    }

}