using System;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFormsFirebase.ConstantFunction;
using XamarinFormsFirebase.Services;
using XamarinFormsFirebase.Views.RegistrationView;

namespace XamarinFormsFirebase.ViewModels
{
	public class AboutViewModel : BaseViewModel
	{
		public Command LogoutCommand { get; }
		private IFirebaseAuth firebaseAuth;
		public AboutViewModel()
		{
			Title = "About";
			firebaseAuth = DependencyService.Get<IFirebaseAuth>();
			LogoutCommand = new Command(OnLogoutClicked);
			UserIdLoad();
		}

		private void UserIdLoad()
        {
			var UserIdValue = firebaseAuth.GetUserId();
        }


		private void OnLogoutClicked(object obj)
        {
			try
			{
				var currentNetwork = Connectivity.NetworkAccess;
				var currentWifi = Connectivity.ConnectionProfiles;

				if (currentNetwork == NetworkAccess.None)
				{
					ToastClass.RedMessageMethod($"Please Connect First.");
				}
				else
				{
					if(currentNetwork == NetworkAccess.Internet)
                    {
						if (currentWifi.Contains(ConnectionProfile.Bluetooth) ||
							currentWifi.Contains(ConnectionProfile.Cellular) ||
							currentWifi.Contains(ConnectionProfile.Ethernet) ||
							currentWifi.Contains(ConnectionProfile.Unknown) ||
							currentWifi.Contains(ConnectionProfile.WiFi))
						{
							var signOutValue = firebaseAuth.SignOut();
							if (signOutValue)
							{
								App.Current.MainPage = new LogInPage();
							}
						}
						else
						{
							ToastClass.RedMessageMethod($"You are not in Wifi Connectivity");
						}
					}
					else
                    {
						ToastClass.RedMessageMethod($"Please Check Internet connection.");
					}
				}
			}
			catch (Exception ex)
			{

			}
		}
    }
}
