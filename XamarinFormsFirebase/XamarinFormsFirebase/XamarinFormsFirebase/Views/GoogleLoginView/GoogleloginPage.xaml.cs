using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.GoogleLoginViewModel;

namespace XamarinFormsFirebase.Views.GoogleLoginView
{
    public partial class GoogleloginPage : ContentPage
    {
        GoogleloginViewModel googleloginViewModel;
        public GoogleloginPage()
        {
            InitializeComponent();
            BindingContext = googleloginViewModel = new GoogleloginViewModel();
        }
    }
}
