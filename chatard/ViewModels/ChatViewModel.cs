using chatard.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Principal;
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
        private bool _isVisible;

        public ICommand SendMessageCommand { get; }


        public ChatViewModel()
        {
            //SeedDatabase();

            LoggedUser = context.Users
                .Where(u => u.Username == "Homer")
                .FirstOrDefault();


            List<UserContacts> userContacts = context.UserContacts.ToList();

            _contacts = ConvertContactsToUsers(userContacts);

            _messagesWithSelectedContact = new ObservableCollection<Message>();

            //_selectedContact = _contact

            SendMessageCommand = new ViewModelCommand(ExecuteSendMessageCommand, CanExecuteSendMessageCommand);

        }

        private void ExecuteSendMessageCommand(object obj)
        {
            Message message = new Message();
            message.Content = MessageToSend;
            message.Sender = LoggedUser;
            message.Receiver = SelectedContact;

            context.Messages.Add(message);

            context.SaveChanges();

            MessagesWithSelectedContact.Add(message);

            GetMessagesWithSelectedContact();
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
                //SendMessage();     
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

            List<Message> messagesWithSelectedContact = context.Messages
              .Where(m => m.Sender.UserId == LoggedUser.UserId && m.Receiver.UserId == SelectedContact.UserId ||
              m.Sender.UserId == SelectedContact.UserId && m.Receiver.UserId == LoggedUser.UserId)
              .ToList();

            messagesWithSelectedContact.Count();

            messagesWithSelectedContact.Sort((x, y) => DateTime.Compare(x.SendTime, y.SendTime));

            MessagesWithSelectedContact = new ObservableCollection<Message>(messagesWithSelectedContact);


        }

        /*private void SendMessage()
        {
            Message message = new Message()
            {
                Id = Guid.NewGuid(),
                Sender = LoggedUser,
                Receiver = SelectedContact,
                SendTime = DateTime.Now,
                //Content = txtMessage.Text
            };

            context.Messages.Add(message);
            context.SaveChanges();
   
            MessagesWithSelectedContact.Add(message);

            _messageToSend = string.Empty;
        }*/


        private void SeedDatabase()
        {

            List<User> users = new();

            User alro = new User();
            alro.Username = "Alro";
            alro.Email = "alro@email.com";
            alro.Password = "1234";
            alro.ProfilePicture = "https://i.pravatar.cc/300";

            User homer = new User();
            homer.Username = "Homer";
            homer.Email = "homer@email.com";
            homer.Password = "1234";
            homer.ProfilePicture = "https://i.pravatar.cc/300";

            User gaia = new User();
            gaia.Username = "Gaia";
            gaia.Email = "alro@email.com";
            gaia.Password = "1234";
            gaia.ProfilePicture = "https://i.pravatar.cc/300";

            User leor = new User();
            leor.Username = "Leor";
            leor.Email = "leor@email.com";
            leor.Password = "1234";
            leor.ProfilePicture = "https://i.pravatar.cc/300";

            User ores = new User();
            ores.Username = "Ores";
            ores.Email = "ores@email.com";
            ores.Password = "1234";
            ores.ProfilePicture = "https://i.pravatar.cc/300";


            users.Add(alro);
            users.Add(homer);
            users.Add(gaia);
            users.Add(leor);
            users.Add(ores);

            context.Users.AddRange(users);

            context.SaveChanges();

            List<UserContacts> userContacts = new();

            UserContacts homerAlro = new UserContacts();
            homerAlro.User = homer;
            homerAlro.Contact = alro;

            UserContacts homerGaia = new UserContacts();
            homerGaia.User = homer;
            homerGaia.Contact = gaia;

            UserContacts homerLisa = new UserContacts();
            homerLisa.User = homer;
            homerLisa.Contact = leor;

            UserContacts homerOres = new UserContacts();
            homerOres.User = homer;
            homerOres.Contact = ores;

            userContacts.Add(homerAlro);
            userContacts.Add(homerGaia);
            userContacts.Add(homerLisa);
            userContacts.Add(homerOres);

            context.UserContacts.AddRange(userContacts);

            context.SaveChanges();

            List<Message> messages = new();

            Message message1 = new Message();
            message1.Sender = homer;
            message1.Receiver = alro;
            message1.SendTime = DateTime.Now;
            message1.Content = "Hello Alro!";

            Message message2 = new Message();
            message2.Sender = alro;
            message2.Receiver = homer;
            message2.SendTime = DateTime.Now.AddMinutes(1);
            message2.Content = "Hello Homer!";

            Message message3 = new Message();
            message3.Sender = homer;
            message3.Receiver = alro;
            message3.SendTime = DateTime.Now.AddMinutes(2);
            message3.Content = "How are you?";

            Message message4 = new Message();
            message4.Sender = alro;
            message4.Receiver = homer;
            message4.SendTime = DateTime.Now.AddMinutes(3);
            message4.Content = "I'm fine, thanks!";

            Message message5 = new Message();
            message5.Sender = homer;
            message5.Receiver = alro;
            message5.SendTime = DateTime.Now.AddMinutes(4);
            message5.Content = "What are you doing?";

            Message message6 = new Message();
            message6.Sender = alro;
            message6.Receiver = homer;
            message6.SendTime = DateTime.Now.AddMinutes(5);
            message6.Content = "I'm working on a project.";

            Message message7 = new Message();
            message7.Sender = homer;
            message7.Receiver = alro;
            message7.SendTime = DateTime.Now.AddMinutes(6);
            message7.Content = "What project?";

            messages.Add(message1);
            messages.Add(message2);
            messages.Add(message3);
            messages.Add(message4);
            messages.Add(message5);
            messages.Add(message6);
            messages.Add(message7);

            context.Messages.AddRange(messages);

            context.SaveChanges();

        }

    }



}