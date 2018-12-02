using XamarinFirebaseMessaging.Services;
using XamarinFirebaseMessaging.Services.Firebase;
using XamarinFirebaseMessaging.Services.Interfaces;
using XamarinFirebaseMessaging.Views;
using CommonServiceLocator;
using Firebase.Database;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Unity;
using Unity.ServiceLocation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace XamarinFirebaseMessaging
{
    public partial class App : Application
    {
        private IFirebaseAuthService _firebaseAuthService;

        public App()
        {
            InitializeComponent();
            SetUpUnity();

            _firebaseAuthService = ServiceLocator.Current.GetInstance<IFirebaseAuthService>();
        }

        private void StartAppStore()
        {
            AppCenter.Start($"android={Constants.ANDROID_APP_CENTER_KEY};ios={Constants.IOS_APP_CENTER_KEY}",
                  typeof(Analytics), typeof(Crashes));
        }

        private void SetUpUnity()
        {
            try
            {
                var unityContainer = new UnityContainer();

                // Register Firebase Singletons
                unityContainer.RegisterSingleton<IFirebaseAuthService, FirebaseAuthService>();
                unityContainer.RegisterSingleton<FirebaseClient, FirebaseClientSingleton>();

                // Register Services
                unityContainer.RegisterType<IMessageService, FirebaseMessageService>();
                unityContainer.RegisterSingleton<IAccountStoreService, AccountStoreService>();

                ServiceLocator.SetLocatorProvider(() => new UnityServiceLocator(unityContainer));
            }
            catch (Exception ex)
            {
                HandleStartupException(ex, "Error setting up Unity dependency injection");
            }
        }

        private void HandleStartupException(Exception ex, string title)
        {
            MainPage = new NavigationPage(new ExceptionView(title, ex));
        }

        private async void CheckAuthAndRedirect()
        {
            try
            {
                var auth = await _firebaseAuthService.GetAuth();
                if (auth == null)
                {
                    MainPage = new NavigationPage(new LoginView());
                }
                else
                {
                    MainPage = new NavigationPage(new MessageView());
                }
            }
            catch (Exception ex)
            {
                HandleStartupException(ex, "Error refreshing authentication. Please log in again.");
            }
        }

        protected override void OnStart()
        {
            StartAppStore();
            CheckAuthAndRedirect();
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
