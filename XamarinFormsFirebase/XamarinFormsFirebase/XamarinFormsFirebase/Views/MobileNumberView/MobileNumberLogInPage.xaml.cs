using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.MobileNumberViewModel;

namespace XamarinFormsFirebase.Views.MobileNumberView
{
    public partial class MobileNumberLogInPage : ContentPage
    {
        MobileNumberLogInViewModel mobileNumberLogInViewModel;
        public MobileNumberLogInPage()
        {
            InitializeComponent();
            BindingContext = mobileNumberLogInViewModel = new MobileNumberLogInViewModel();
        }
    }
}
