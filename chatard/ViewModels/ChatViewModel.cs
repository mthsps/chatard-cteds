using chatard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Input;
using static System.Net.WebRequestMethods;

namespace chatard.ViewModels
{
    public class ChatViewModel : ViewModelBase
    {

        public User LoggedUser;
        private List<User> _contacts;
        private List<Message> _messagesWithSelectedContact;
        private User _selectedContact;

        public ChatViewModel()
        {

            LoggedUser = context.Users
                .Where(u => u.Username == Thread.CurrentPrincipal.Identity.Name)
                .FirstOrDefault();

            var userContacts = context.UserContacts
                 .Where(b => b.Contact.UserId == LoggedUser.UserId || b.User.UserId == LoggedUser.UserId)
                 .ToList();

            _contacts = ConvertContactsToUsers(userContacts);

        }

        private List<User> ConvertContactsToUsers(List<UserContacts> userContacts)
        {
            List<User> contacts = new List<User>();
            foreach (UserContacts contact in userContacts)
            {
                if (contact.User == null && contact.Contact.UserId != LoggedUser.UserId)
                {
                    contacts.Add(contact.Contact);
                }
                else if (contact.Contact == null && contact.User.UserId != LoggedUser.UserId)
                {
                    contacts.Add(contact.User);
                }
            }
            return contacts;
        }

        public List<User> Contacts
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

        public List<Message> MessagesWithSelectedContact
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
            }
        }


        public ICommand SendMessageCommand
        {
            get
            {
                return new ViewModelCommand(SendMessage);
            }
        }

        public ICommand SelectContactCommand
        {
            get
            {
                return new ViewModelCommand(SelectContact);
            }
        }

        private void SelectContact(object obj)
        {
            MessagesWithSelectedContact = context.Messages
                .Where(m => m.Sender.UserId == LoggedUser.UserId && m.Receiver.UserId == SelectedContact.UserId ||
                m.Sender.UserId == SelectedContact.UserId && m.Receiver.UserId == LoggedUser.UserId)
                .ToList();
        }


        private void SendMessage(object obj)
        {
            Message message = new Message();
            message.Sender = LoggedUser;
            message.Receiver = SelectedContact;
            message.Content = obj.ToString();
            context.Messages.Add(message);
            context.SaveChanges();
            MessagesWithSelectedContact.Add(message);
        }










    }
}