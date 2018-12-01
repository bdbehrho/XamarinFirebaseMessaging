using XamarinFirebaseMessaging.Services.Interfaces;
using Firebase.Database;

namespace XamarinFirebaseMessaging.Services.Firebase
{
    /// <summary>
    /// Singleton Firebase client that is configured to use Firebase Realtime Database
    /// </summary>
    public class FirebaseClientSingleton : FirebaseClient
    {
        public FirebaseClientSingleton(IFirebaseAuthService firebaseAuthService) 
            : base(
                  Constants.FIREBASE_REALTIME_DATABASE_BASE_URL,
                  new FirebaseOptions()
                  {
                      AuthTokenAsyncFactory = async () => (await firebaseAuthService.GetAuth()).FirebaseToken
                  })
        {
        }
    }
}
