using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;
using Xamarin.Forms;
using XamarinFormsFirebase.AuthHelper;
using XamarinFormsFirebase.ConstantFunction;
using XamarinFormsFirebase.Models;
using XamarinFormsFirebase.Services;
using XamarinFormsFirebase.Views.RegistrationView;

namespace XamarinFormsFirebase.ViewModels
{
    public class AboutViewModel : BaseViewModel
	{
		public Command LogoutCommand { get; }
		public Command SaveCommand { get; }
		public Command GetCommand { get; }
		public Command DeleteCommand { get; }
		private IFirebaseAuth firebaseAuth;
		public string UserId;
		public AboutViewModel()
		{
			Title = "About";
			firebaseAuth = DependencyService.Get<IFirebaseAuth>();
			LogoutCommand = new Command(OnLogoutClicked);
			SaveCommand = new Command(OnSaveClicked);
			GetCommand = new Command(OnGetClicked);
			DeleteCommand = new Command(OnDeleteClicked);
			UserIdLoad();
			SetTextValue = "1";
			UserId = firebaseAuth.GetUserId();
		}

        private async void OnDeleteClicked(object obj)
        {
			var existRecord = await FirebaseHelperAddData.GetReord(UserId);

			if (existRecord == null)
			{
				ToastClass.RedMessageMethod("No Record Found");
			}
			else
			{
				await FirebaseHelperAddData.DeleteReord(UserId);
			}
			DisplayAllDataOnList();
		}

        private async void OnGetClicked(object obj)
        {
			var singleRecord = await FirebaseHelperAddData.GetReord(UserId);
			if(singleRecord == null)
            {
				ToastClass.RedMessageMethod("No Record Found");
			}
			else
            {
				UserName = singleRecord.MyProperty;
			}
		}

		private async void OnSaveClicked(object obj)
        {
			var existRecord = await FirebaseHelperAddData.GetReord(UserId);

			if(existRecord == null)
			{
				await FirebaseHelperAddData.AddReord(UserName, UserId);
			}
			else
			{
				await FirebaseHelperAddData.UpdateReord(UserId, UserName);
			}
			DisplayAllDataOnList();
		}

		public async void DisplayAllDataOnList()
        {
			var allPersons = await FirebaseHelperAddData.GetAllRecords();
			myDatabaseRecords = allPersons;
		}

		List<MyDatabaseRecord> _myDatabaseRecords;
		public List<MyDatabaseRecord> myDatabaseRecords
		{
			get { return _myDatabaseRecords; }
			set
			{
				if (value == _myDatabaseRecords) return;
				_myDatabaseRecords = value;
				OnPropertyChanged();
			}
		}

		private string _ButtonTextDeleteData = "Delete";
		public string ButtonTextDeleteData
		{
			get => _ButtonTextDeleteData;
			set => SetProperty(ref _ButtonTextDeleteData, value);
		}

		private string _ButtonText = "Save";
		public string ButtonText
		{
			get => _ButtonText;
			set => SetProperty(ref _ButtonText, value);
		}

		private string _ButtonTextGetData = "Get";
		public string ButtonTextGetData
		{
			get => _ButtonTextGetData;
			set => SetProperty(ref _ButtonTextGetData, value);
		}

		private string _UserName;
		public string UserName
		{
			get => _UserName;
			set => SetProperty(ref _UserName, value);
		}

		private string _SetTextValue;
		public string SetTextValue
        {
			get => _SetTextValue;
			set => SetProperty(ref _SetTextValue, value);
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
