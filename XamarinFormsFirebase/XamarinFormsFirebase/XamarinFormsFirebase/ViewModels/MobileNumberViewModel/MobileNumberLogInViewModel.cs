using System;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFormsFirebase.ConstantFunction;
using XamarinFormsFirebase.Services;
using XamarinFormsFirebase.Views.MobileNumberView;

namespace XamarinFormsFirebase.ViewModels.MobileNumberViewModel
{
    public class MobileNumberLogInViewModel : BaseViewModel
    {
        public Command SignUpCommand { get; }
        public Command NextCommand { get; }
        private IFirebaseAuth firebaseAuth;
        public MobileNumberLogInViewModel()
        {
            Title = "Mobile Number LogIn";
            firebaseAuth = DependencyService.Get<IFirebaseAuth>();
            NextCommand = new Command(OnNextClicked, ValidateSignUp);
            this.PropertyChanged +=
                (_, __) => NextCommand.ChangeCanExecute();
        }

        private async void OnNextClicked()
        {
            try
            {
                var currentNetwork = Connectivity.NetworkAccess;
                var currentWifi = Connectivity.ConnectionProfiles;

                if (currentNetwork == NetworkAccess.Internet)
                {
                    if (currentWifi.Contains(ConnectionProfile.WiFi))
                    {
                        if(_CodeRequested)
                        {
                            var loginAttempt = await firebaseAuth.VerifyOtpCodeAsync(Code);
                            if(loginAttempt)
                            {
                                App.Current.MainPage = new AppShell();
                            }
                            else
                            {
                                ToastClass.RedMessageMethod("Somethings wrong in the Mobile number and OTP");
                            }
                        }
                        else
                        {
                            CodeSent = await firebaseAuth.SendOTPCodeAsync(MobileNumber);
                            if (!CodeSent)
                                return;

                            _CodeRequested = true;
                            ButtonText = "verify Code";
                        }

                    }
                    else
                    {
                        ToastClass.RedMessageMethod($"You are not in Wifi");
                    }
                }
                else
                {
                    ToastClass.RedMessageMethod($"Please Check Internet connection.");
                }
            }
            catch (Exception ex)
            {

            }
        }


        private bool ValidateSignUp()
        {
            return !String.IsNullOrWhiteSpace(MobileNumber);
        }

        private string _ButtonText = "Send Code";
        public string ButtonText
        {
            get => _ButtonText;
            set => SetProperty(ref _ButtonText, value);
        }

        private bool _CodeSent;
        public bool CodeSent
        {
            get => _CodeSent;
            set => SetProperty(ref _CodeSent, value);
        }

        private string _Code;
        public string Code
        {
            get => _Code;
            set => SetProperty(ref _Code, value);
        }

        private string _MobileNumber;
        public string MobileNumber
        {
            get => _MobileNumber;
            set => SetProperty(ref _MobileNumber, value);
        }

        private bool _CodeRequested;
        public bool CodeRequested
        {
            get => _CodeRequested;
            set => SetProperty(ref _CodeRequested, value);
        }
    }
}
