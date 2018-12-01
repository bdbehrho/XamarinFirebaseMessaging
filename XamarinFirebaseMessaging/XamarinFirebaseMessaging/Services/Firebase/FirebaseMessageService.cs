using XamarinFirebaseMessaging.Models;
using XamarinFirebaseMessaging.Services.Interfaces;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseMessaging.Services.Firebase
{
    public class FirebaseMessageService : IMessageService
    {
        private readonly FirebaseClient _firebaseClient;
        private readonly IFirebaseAuthService _firebaseAuthService;

        public FirebaseMessageService(FirebaseClient firebaseClient, IFirebaseAuthService firebaseAuthService)
        {
            _firebaseClient = firebaseClient;
            _firebaseAuthService = firebaseAuthService;
        }

        public async Task SendMessage(string text)
        {
            await _firebaseClient
                .Child("messages")
                .PostAsync(new Message()
                {
                    Text = text,
                    Date = DateTime.Now,
                    UserId = (await _firebaseAuthService.GetAuth()).User.Email
                });
        }

        public async Task<IReadOnlyCollection<FirebaseObject<Message>>> GetAllMessages()
        {
            return await _firebaseClient
                .Child("messages")
                .OnceAsync<Message>();
        }

        public void ListenForMessages(Action<FirebaseObject<Message>> onMessageUpdate)
        {
            _firebaseClient
                .Child("messages")
                .AsObservable<Message>()
                .Subscribe(d => onMessageUpdate(d));
        }
    }
}
