using XamarinFirebaseMessaging.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseMessaging.Services.Interfaces
{
    public interface IMessageService
    {
        /// <summary>
        /// Send a message with the given text to storage
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        Task SendMessage(string text);
        /// <summary>
        /// Retrieve all messages from storage
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<FirebaseObject<Message>>> GetAllMessages();
        /// <summary>
        /// Listen for incoming messages from storage, performing onMessageUpdate when retrieved
        /// </summary>
        /// <param name="onMessageUpdate"></param>
        void ListenForMessages(Action<FirebaseObject<Message>> onMessageUpdate);
    }
}
