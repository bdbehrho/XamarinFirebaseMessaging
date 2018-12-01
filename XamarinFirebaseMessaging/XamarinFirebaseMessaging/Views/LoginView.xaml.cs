using XamarinFirebaseMessaging.Services.Firebase;
using XamarinFirebaseMessaging.Services.Interfaces;
using CommonServiceLocator;
using Firebase.Auth;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseMessaging.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LoginView : ContentPage
	{
        private Exception _exception = null;
        private string _exceptionTitle = null;
        private IFirebaseAuthService _firebaseAuthService;

		public LoginView ()
		{
			InitializeComponent ();
            _firebaseAuthService = ServiceLocator.Current.GetInstance<IFirebaseAuthService>();
		}

        public LoginView(Exception ex, string title) : this()
        {
            _exception = ex;
            _exceptionTitle = title;
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();
            if (_exception != null)
            {
                await Task.Delay(1000);
                PageUtil.DisplayExceptionAlert(this, _exception, _exceptionTitle);
            }
        }

        public async void SignIn(object sender, EventArgs e)
        {
            SignInButton.Text = "Logging in...";
            SignInButton.IsEnabled = false;
            bool loginSuccessful = true;
            FirebaseAuth auth = null;
            try
            {
                auth = await _firebaseAuthService.SignInWithEmailAndPassword(UsernameEntry.Text, PasswordEntry.Text);
            } catch(Exception ex)
            {
                loginSuccessful = false;
                Debug.WriteLine("Failed to login with exception: " + ex.Message);
            }
            loginSuccessful = loginSuccessful && auth != null;
            if(loginSuccessful)
            {
                WarningText.Text = "";
                await Navigation.PushModalAsync(new MessageView());
            } else
            {
                SignInButton.IsEnabled = true;
                SignInButton.Text = "Log In";
                WarningText.Text = "Login failed. Please try again.";
            }
        }
	}
}