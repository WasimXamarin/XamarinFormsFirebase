using System;
using System.Collections.Generic;
using XamarinFormsFirebase.ViewModels;
using XamarinFormsFirebase.Views;
using Xamarin.Forms;
using XamarinFormsFirebase.Views.ImageUploadView;

namespace XamarinFormsFirebase
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            Routing.RegisterRoute(nameof(ImageUploadPage), typeof(ImageUploadPage));
        }

    }
}

