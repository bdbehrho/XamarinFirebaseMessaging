using Firebase.Auth;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace XamarinFirebaseMessaging.Services.Interfaces
{
    public interface IFirebaseAuthService
    {
        /// <summary>
        /// Get the firebase authentication object
        /// </summary>
        /// <returns></returns>
        Task<FirebaseAuth> GetAuth();
        /// <summary>
        /// Sign in to firebase with given email and password. Returns the firebase auth object
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        Task<FirebaseAuth> SignInWithEmailAndPassword(string email, string password);
    }
}
