using System;
using Xamarin.Forms;
using XamarinFormsFirebase.Views.RegistrationView;
using XamarinFormsFirebase.Services;
using XamarinFormsFirebase.ConstantFunction;
using Xamarin.Essentials;
using System.Linq;

namespace XamarinFormsFirebase.ViewModels.RegistrationViewModel
{
	public class SignUpViewModel : BaseViewModel
	{
		public Command SignUpCommand { get; }
		public Command LogInCommand { get; }
		private IFirebaseAuth firebaseAuth;
		public SignUpViewModel()
		{
			Title = "Sign Up";
			firebaseAuth = DependencyService.Get<IFirebaseAuth>();
			SignUpCommand = new Command(OnSignUpClicked, ValidateSignUp);
			LogInCommand = new Command(OnLogInClicked);

			this.PropertyChanged +=
				(_, __) => SignUpCommand.ChangeCanExecute();
		}

		private async void OnSignUpClicked()
		{
			try
			{
                var currentNetwork = Connectivity.NetworkAccess;
                var currentWifi = Connectivity.ConnectionProfiles;

				if(currentNetwork == NetworkAccess.Internet)
                {
                    if(currentWifi.Contains(ConnectionProfile.WiFi))
                    {
                        if (Password.Length > 7)
                        {
                            var user = await firebaseAuth.SignUpWithEmailAndPassword(EmailId, Password);
                            if (user == "Invalid User")
                            {
                                ToastClass.RedMessageMethod($"Email Id is not correct format.");
                            }
                            else if (user == "Invalid Auth")
                            {
                                ToastClass.RedMessageMethod($"Email Id or Password wrong.");
                            }
                            else if (user == "User Exist")
                            {
                                ToastClass.RedMessageMethod($"Email Id Already exist.");
                            }
                            else if (user == "Inertnal Error")
                            {
                                ToastClass.RedMessageMethod("Inertnal Error");
                            }
                            else if (user != "")
                            {
                                App.Current.MainPage = new LogInPage();
                            }
                            else
                            {
                                ToastClass.RedMessageMethod($"Somthings went wrong, please try again");
                            }
                        }
                        else
                        {
                            ToastClass.RedMessageMethod($"Password week");
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
			return !String.IsNullOrWhiteSpace(EmailId)
				&& !String.IsNullOrWhiteSpace(Password);
		}

		private void OnLogInClicked()
		{
            try
            {
				App.Current.MainPage = new LogInPage();
            }
            catch (Exception ex)
            {

            }
		}

		private string _EmailId;
		public string EmailId
        {
			get => _EmailId;
			set => SetProperty(ref _EmailId, value);
        }

		private string _Password;
		public string Password
		{
			get => _Password;
			set => SetProperty(ref _Password, value);
		}
	}
}

