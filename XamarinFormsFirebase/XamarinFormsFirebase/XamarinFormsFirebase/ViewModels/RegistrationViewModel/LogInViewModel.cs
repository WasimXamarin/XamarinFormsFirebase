using System;
using Xamarin.Forms;
using XamarinFormsFirebase.Views.RegistrationView;
using XamarinFormsFirebase.Services;
using Xamarin.Essentials;
using XamarinFormsFirebase.ConstantFunction;
using System.Linq;

namespace XamarinFormsFirebase.ViewModels.RegistrationViewModel
{
	public class LogInViewModel : BaseViewModel
	{
		public Command SignUpCommand { get; }
		public Command LogInCommand { get; }
		private IFirebaseAuth firebaseAuth;
		public LogInViewModel()
		{
			Title = "Log In";
			firebaseAuth = DependencyService.Get<IFirebaseAuth>();
			LogInCommand = new Command(OnLogInClicked, ValidateSignUp);
			SignUpCommand = new Command(OnSignUpClicked);

			this.PropertyChanged +=
				(_, __) => LogInCommand.ChangeCanExecute();
		}

		private async void OnLogInClicked()
		{
			try
			{
				var currentNetwork = Connectivity.NetworkAccess;
				var currentWifi = Connectivity.ConnectionProfiles;

				if (currentNetwork == NetworkAccess.Internet)
				{
					if (currentWifi.Contains(ConnectionProfile.WiFi))
					{
						var user = await firebaseAuth.LoginWithEmailAndPassword(EmailId, Password);
						if (user == "Invalid User")
						{
							ToastClass.RedMessageMethod($"Email Id is not correct format.");
						}
						else if (user == "Invalid Auth")
						{
							ToastClass.RedMessageMethod($"Email Id or Password wrong.");
						}
						else if (user == "Inertnal Error")
						{
							ToastClass.RedMessageMethod("Inertnal Error");
						}
						else if (user != "")
						{
							App.Current.MainPage = new AppShell();
						}
						else
						{
							ToastClass.RedMessageMethod($"Somthings went wrong, please try again");
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

		private void OnSignUpClicked()
		{
			try
			{
				App.Current.MainPage = new SignUpPage();
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

