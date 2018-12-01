using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace XamarinFirebaseMessaging
{
	public static class PageUtil
	{
        public static void DisplayExceptionAlert(Page page, Exception ex, string title)
        {
            page.DisplayAlert(title, $"{ex.Message}\n\n{ex.StackTrace}", "OK");
        }
    }
}