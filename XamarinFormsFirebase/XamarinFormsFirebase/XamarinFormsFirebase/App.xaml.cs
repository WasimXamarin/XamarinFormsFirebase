using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinFormsFirebase.Services;
using XamarinFormsFirebase.Views;
using Xamarin.Essentials;
using XamarinFormsFirebase.Views.RegistrationView;
using XamarinFormsFirebase.Views.MobileNumberView;
using XamarinFormsFirebase.Views.GmailLoginView;
using XamarinFormsFirebase.Views.GoogleLoginView;
using XamarinFormsFirebase.Views.ImageUploadView;

namespace XamarinFormsFirebase
{
    public partial class App : Application
    {
        private IFirebaseAuth firebaseAuth;
        public App ()
        {
            InitializeComponent();

            firebaseAuth = DependencyService.Get<IFirebaseAuth>();
            DependencyService.Register<MockDataStore>();

            //MainPage = new ImageUploadPage();

            if (firebaseAuth.IsSignIn())
            {
                MainPage = new AppShell();
            }
            else
            {
                MainPage = new LogInPage();
            }
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                MainPage = new LoginPage();
            }
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
                if (firebaseAuth.IsSignIn())
                {
                    MainPage = new AppShell();
                }
                else
                {
                    MainPage = new LogInPage();
                }
            }
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

