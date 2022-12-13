using chatard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace chatard.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {
        
        public User LoggedUser;
        private ObservableCollection<User> _contacts;
        private ObservableCollection<Message> _messagesWithSelectedContact;
        private User _selectedContact;
        private string _messageToSend;

        
        public ChatViewModel()
        {

            LoggedUser = context.Users
                .Where(u => u.Username == "Homer")
                .FirstOrDefault();
                

            var userContacts = context.UserContacts
                 .ToList();

            _contacts = ConvertContactsToUsers(userContacts);

            _messagesWithSelectedContact = new ObservableCollection<Message>();

            //_selectedContact = _contact

        }

        private ObservableCollection<User> ConvertContactsToUsers(List<UserContacts> userContacts)
        {
            ObservableCollection<User> contacts = new ObservableCollection<User>();
            foreach (UserContacts contact in userContacts)
            {
                if (contact.Contact != null)
                {
                    contacts.Add(contact.Contact);
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
                SendMessage();     
            }
        }

        private void GetMessagesWithSelectedContact()
        {

            List<Message> messagesWithSelectedContact = context.Messages
              .Where(m => m.Sender.UserId == LoggedUser.UserId && m.Receiver.UserId == SelectedContact.UserId ||
              m.Sender.UserId == SelectedContact.UserId && m.Receiver.UserId == LoggedUser.UserId)
              .ToList();

            messagesWithSelectedContact.Count();

            messagesWithSelectedContact.Sort((x, y) => DateTime.Compare(x.SendTime, y.SendTime));

            MessagesWithSelectedContact = new ObservableCollection<Message>(messagesWithSelectedContact);


        }

        private void SendMessage()
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Sender = LoggedUser,
                Receiver = SelectedContact,
                SendTime = DateTime.Now,
               // Content = txtMessage.Text
            };

            context.Messages.Add(message);
            context.SaveChanges();
   
            MessagesWithSelectedContact.Add(message);

            _messageToSend = string.Empty;
        }



    }
}