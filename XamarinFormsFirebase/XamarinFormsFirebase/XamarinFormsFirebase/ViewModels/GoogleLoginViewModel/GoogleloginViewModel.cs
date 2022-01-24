using System;
using Xamarin.Forms;
using Xamarin.Auth;
using XamarinFormsFirebase.ConstantFunction;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using XamarinFormsFirebase.AuthHelper;

namespace XamarinFormsFirebase.ViewModels.GoogleLoginViewModel
{
    public class GoogleloginViewModel : BaseViewModel
    {
		Account account;
        [Obsolete]
        AccountStore store;
        public Command GoogleLoginCommand { get; set; }

        [Obsolete]
        public GoogleloginViewModel()
        {
            Title = "Google login";
            GoogleLoginCommand = new Command(OnGoogleLoginClicked);

			store = AccountStore.Create();
            account = store.FindAccountsForService(Constants.AppName).FirstOrDefault();
        }

        [Obsolete]
        private void OnGoogleLoginClicked(object obj)
        {
            string clientId = null;
			string redirectUri = null;

			switch (Device.RuntimePlatform)
			{
				case Device.iOS:
					clientId = Constants.iOSClientId;
					redirectUri = Constants.iOSRedirectUrl;
					break;

				case Device.Android:
					clientId = Constants.AndroidClientId;
					redirectUri = Constants.AndroidRedirectUrl;
					break;
			}

            try
            {
				var authenticator = new OAuth2Authenticator(
				clientId,
				null,
				Constants.Scope,
				new Uri(Constants.AuthorizeUrl),
				new Uri(redirectUri),
				new Uri(Constants.AccessTokenUrl),
				null,
				true);

				authenticator.Completed += OnAuthCompleted;
				authenticator.Error += OnAuthError;

				AuthenticationState.Authenticator = authenticator;

				var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
				presenter.Login(authenticator);
			}
            catch (Exception ex)
            {

            }
        }

        [Obsolete]
        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			User user = null;
			if (e.IsAuthenticated)
			{
				// If the user is authenticated, request their basic user data from Google
				// UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
				var request = new OAuth2Request("GET", new Uri(Constants.UserInfoUrl), null, e.Account);
				var response = await request.GetResponseAsync();
				if (response != null)
				{
					// Deserialize the data and store it in the account store
					// The users email address will be used to identify data in SimpleDB
					string userJson = await response.GetResponseTextAsync();
					user = JsonConvert.DeserializeObject<User>(userJson);
				}

				if(user != null)
                {
					App.Current.MainPage = new AppShell();
                }

				//            #region iOS Remove
				//            if (account != null)
				//{
				//	store.Delete(account, Constants.AppName);
				//}

				//await store.SaveAsync(account = e.Account, Constants.AppName);
				//ToastClass.RedMessageMethod(user.Email);
				//            #endregion

				ToastClass.RedMessageMethod(user.Email);
			}
        }

        [Obsolete]
        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
		{
			var authenticator = sender as OAuth2Authenticator;
			if (authenticator != null)
			{
				authenticator.Completed -= OnAuthCompleted;
				authenticator.Error -= OnAuthError;
			}

			Debug.WriteLine("Authentication error: " + e.Message);
		}

		private string _GoogleLoginText = "Google Login";
        public string GoogleLoginText
        {
            get => _GoogleLoginText;
            set => SetProperty(ref _GoogleLoginText, value);
        }
    }
}
