using XamarinFirebaseMessaging.Services;
using XamarinFirebaseMessaging.Services.Firebase;
using XamarinFirebaseMessaging.Services.Interfaces;
using XamarinFirebaseMessaging.ViewModels;
using CommonServiceLocator;
using Firebase.Database;
using System;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseMessaging.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MessageView : ContentPage
	{
        private MessageViewModel _messageVm;

        public MessageView ()
		{
            InitializeComponent();
            _messageVm = ServiceLocator.Current.GetInstance<MessageViewModel>();
            RetrieveMessages();
            MessageList.ItemsSource = _messageVm.Messages;
            _messageVm.MessageAdded += ScrollToBottom;
		}

        private async void RetrieveMessages()
        {
            try
            {
                await _messageVm.Initialize();
            }
            catch (Exception ex) { PageUtil.DisplayExceptionAlert(this, ex, "Error retrieving messages"); }
        }

        private void ScrollToBottom(object sender, EventArgs e)
        {
            MessageList.ScrollTo(_messageVm.Messages.LastOrDefault(), ScrollToPosition.End, true);
        }

        public async void SendMessage(object sender, EventArgs e)
        {
            try
            {
                await _messageVm.SendMessage(MessageText.Text);
                MessageText.Text = "";
            } catch(Exception ex) { PageUtil.DisplayExceptionAlert(this, ex, "Error sending message"); }
        }
    }
}