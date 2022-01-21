using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.RegistrationViewModel;

namespace XamarinFormsFirebase.Views.RegistrationView
{	
	public partial class LogInPage : ContentPage
	{
		LogInViewModel logInViewModel;
		public LogInPage ()
		{
			InitializeComponent ();
			BindingContext = logInViewModel = new LogInViewModel();
		}
	}
}

