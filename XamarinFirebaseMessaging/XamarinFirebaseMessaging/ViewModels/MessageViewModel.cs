using XamarinFirebaseMessaging.Models;
using XamarinFirebaseMessaging.Services.Interfaces;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseMessaging.ViewModels
{
    public class MessageViewModel
    {
        private readonly IMessageService _messageService;
        private Dictionary<string, Message> _messageDictionary = new Dictionary<string, Message>();

        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        public ObservableCollection<Message> Messages => _messages;

        public event EventHandler MessageAdded;

        public MessageViewModel(IMessageService messageService)
        {
            _messageService = messageService;
        }

        public async Task Initialize()
        {
            var messages = await _messageService.GetAllMessages();
            foreach(var message in messages)
            {
                HandleNewMessage(message);
            }
            _messageService.ListenForMessages(message => HandleNewMessage(message));
        }

        private void HandleNewMessage(FirebaseObject<Message> message)
        {
            // if Object is null, that is the Firebase.Database way of telling us the collection is empty, so ignore here
            if (message.Object != null)
            {
                if (_messageDictionary.ContainsKey(message.Key))
                {
                    _messageDictionary[message.Key].Text = message.Object.Text;
                }
                else
                {
                    _messageDictionary[message.Key] = message.Object;
                    _messages.Add(message.Object);
                    MessageAdded?.Invoke(this, null);
                }
            }
        }

        public async Task SendMessage(string text)
        {
            await _messageService.SendMessage(text);
        }
    }
}
