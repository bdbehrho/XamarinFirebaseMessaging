using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinFirebaseMessaging.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ExceptionView : ContentPage
	{
        public ExceptionView(string title, Exception ex)
		{
			InitializeComponent ();

            TitleLabel.Text = title;
            ExceptionDetailsLabel.Text = $"{ex.Message}\n\nHResult: {ex.HResult}\n\n{ex.StackTrace}";
		}

        public void ProceedToLogin(object sender, EventArgs e)
        {
            Navigation.PushAsync(new LoginView());
        }
	}
}