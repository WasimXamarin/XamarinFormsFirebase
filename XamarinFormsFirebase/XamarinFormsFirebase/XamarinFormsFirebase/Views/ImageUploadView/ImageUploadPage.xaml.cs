using System;
using System.Collections.Generic;

using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels.ImageUploadViewModel;

namespace XamarinFormsFirebase.Views.ImageUploadView
{
    public partial class ImageUploadPage : ContentPage
    {
        ImageUploadViewModel imageUploadViewModel;
        public ImageUploadPage()
        {
            InitializeComponent();
            BindingContext = imageUploadViewModel = new ImageUploadViewModel();
        }
    }
}
