using XamarinFirebaseMessaging.Services.Interfaces;
using Firebase.Auth;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace XamarinFirebaseMessaging.Services.Firebase
{
    public class FirebaseAuthService : IFirebaseAuthService
    {
        private static readonly string FIREBASE_AUTH_ACCOUNT_PROPERTIES_KEY = "FirebaseAuthJson";
        private readonly IAccountStoreService _accountStoreService;

        public FirebaseAuthService(IAccountStoreService accountStoreService)
        {
            _accountStoreService = accountStoreService;
        }

        private FirebaseAuthProvider _firebaseAuthProvider = null;
        private FirebaseAuthProvider FirebaseAuthProvider => _firebaseAuthProvider ?? 
            (_firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(Constants.FIREBASE_API_KEY)));

        private FirebaseAuth _auth = null;

        public async Task<FirebaseAuth> GetAuth()
        {
            if (_auth == null)
            {
                _auth = _accountStoreService.GetValue<FirebaseAuth>(FIREBASE_AUTH_ACCOUNT_PROPERTIES_KEY);

                // if auth wasn't in storage either, just return null
                if(_auth == null)
                {
                    return null;
                }
            }
            if (_auth.IsExpired())
            {
                try
                {
                    // exception being thrown here sometimes
                    _auth = await FirebaseAuthProvider.RefreshAuthAsync(_auth);
                    SaveAuthToAccount();
                } catch(Exception ex)
                {
                    Debug.WriteLine("Encountered exception refreshing FirebaseAuth token: " + ex.Message);
                }
            }
            return _auth;
        }

        private void SaveAuthToAccount()
        {
            try
            {
                _accountStoreService.SetValue(FIREBASE_AUTH_ACCOUNT_PROPERTIES_KEY, _auth);
            }
            catch (Exception ex)
            {
                // This fails on iOS currently, just ignore and users will have to log in every time
            }
        }

        public async Task<FirebaseAuth> SignInWithEmailAndPassword(string email, string password)
        {
            // Email/Password Auth
            _auth = await FirebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
            SaveAuthToAccount();
            return _auth;
        }
    }
}
