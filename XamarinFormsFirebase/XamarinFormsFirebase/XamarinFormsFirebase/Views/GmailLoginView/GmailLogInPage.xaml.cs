using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.GmailLoginViewModel;

namespace XamarinFormsFirebase.Views.GmailLoginView
{
    public partial class GmailLogInPage : ContentPage
    {
        GmailLogInViewModel gmailLogInViewModel;
        public GmailLogInPage()
        {
            InitializeComponent();
            BindingContext = gmailLogInViewModel = new GmailLogInViewModel();
        }
    }
}
