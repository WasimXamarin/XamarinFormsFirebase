using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.RegistrationViewModel;

namespace XamarinFormsFirebase.Views.RegistrationView
{	
	public partial class SignUpPage : ContentPage
	{
		SignUpViewModel signUpViewModel;
		public SignUpPage ()
		{
			InitializeComponent ();
			BindingContext = signUpViewModel = new SignUpViewModel();
		}
	}
}

