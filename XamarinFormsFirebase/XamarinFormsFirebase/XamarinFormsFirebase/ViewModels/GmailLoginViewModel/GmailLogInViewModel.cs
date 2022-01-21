using System;
using Xamarin.Forms;
using XamarinFormsFirebase.ConstantFunction;
using XamarinFormsFirebase.Models;
using XamarinFormsFirebase.Services;

namespace XamarinFormsFirebase.ViewModels.GmailLoginViewModel
{
    public class GmailLogInViewModel : BaseViewModel
    {
        public Command LogInEmailIdCommand { get; }
        public Command LogOutEmailIdCommand { get; }
        private readonly IGoogleManager googleManager;
        public GmailLogInViewModel()
        {
            Title = "Gmail LogIn";
            googleManager = DependencyService.Get<IGoogleManager>();
            LogInEmailIdCommand = new Command(OnLogInEmailIdClicked);
            LogOutEmailIdCommand = new Command(OnLogOutEmailIdClicked);
            //CheckUserLoggedIn();
        }

        private void CheckUserLoggedIn()
        {
            googleManager.Login(OnLoginComplete);
        }

        private void OnLogOutEmailIdClicked(object obj)
        {
            googleManager.Logout();
            Name = "w";
            EmailId = "w@gmail.com";
            ImageProfile = "";
        }

        private void OnLogInEmailIdClicked(object obj)
        {
            googleManager.Login(OnLoginComplete);
        }

        private void OnLoginComplete(GoogleUser googleUser, string message)
        {
            if(googleUser != null)
            {
                Name = googleUser.Name;
                EmailId = googleUser.Email;
                ImageProfile = googleUser.Picture;
            }
            else
            {
                ToastClass.RedMessageMethod(message);
            }
        }

        private ImageSource _ImageProfile;
        public ImageSource ImageProfile
        {
            get => _ImageProfile;
            set => SetProperty(ref _ImageProfile, value);
        }

        private string _LogInButtonText = "Log In With Email Id";
        public string LogInButtonText
        {
            get => _LogInButtonText;
            set => SetProperty(ref _LogInButtonText, value);
        }

        private string _LogOutButtonText = "Log Out From Email Id";
        public string LogOutButtonText
        {
            get => _LogOutButtonText;
            set => SetProperty(ref _LogOutButtonText, value);
        }

        private string _EmailId;
        public string EmailId
        {
            get => _EmailId;
            set => SetProperty(ref _EmailId, value);
        }

        private string _Name;
        public string Name
        {
            get => _Name;
            set => SetProperty(ref _Name, value);
        }
    }
}
