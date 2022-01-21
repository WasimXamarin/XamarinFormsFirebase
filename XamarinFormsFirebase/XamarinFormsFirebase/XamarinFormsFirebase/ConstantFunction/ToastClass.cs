using System;
using Acr.UserDialogs;

namespace XamarinFormsFirebase.ConstantFunction
{
	public class ToastClass
	{
        // Green Toaster Message display.
        public static void GreenMessageMethod(string YourMessage)
        {
            var toastConfig = new ToastConfig(YourMessage);
            toastConfig.SetDuration(2000);
            toastConfig.SetPosition(ToastPosition.Bottom);
            toastConfig.SetBackgroundColor(System.Drawing.Color.Green);
            UserDialogs.Instance.Toast(toastConfig);
        }

        // Red Toaster Message display.
        public static void RedMessageMethod(string YourMessage)
        {
            var toastConfig = new ToastConfig(YourMessage);
            toastConfig.SetDuration(2000);
            toastConfig.SetPosition(ToastPosition.Bottom);
            toastConfig.SetBackgroundColor(System.Drawing.Color.Red);
            UserDialogs.Instance.Toast(toastConfig);
        }
    }
}

