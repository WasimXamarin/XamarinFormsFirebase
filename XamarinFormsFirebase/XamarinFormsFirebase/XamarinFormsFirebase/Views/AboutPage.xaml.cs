using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsFirebase.ViewModels;

namespace XamarinFormsFirebase.Views
{
    public partial class AboutPage : ContentPage
    {
        AboutViewModel aboutViewModel;
        public AboutPage()
        {
            InitializeComponent();
            BindingContext = aboutViewModel = new AboutViewModel();
        }
    }
}
