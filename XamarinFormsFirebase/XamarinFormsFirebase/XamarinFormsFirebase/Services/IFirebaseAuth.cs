using System;
using System.Threading.Tasks;

namespace XamarinFormsFirebase.Services
{
	public interface IFirebaseAuth
	{
		Task<string> LoginWithEmailAndPassword(string email, string password);

		Task<string> SignUpWithEmailAndPassword(string email, string password);

		Task<bool> SendOTPCodeAsync(string mobileNumber);

		Task<bool> VerifyOtpCodeAsync(string code);

		bool SignOut();

		bool IsSignIn();

		string GetUserId();
	}
}

