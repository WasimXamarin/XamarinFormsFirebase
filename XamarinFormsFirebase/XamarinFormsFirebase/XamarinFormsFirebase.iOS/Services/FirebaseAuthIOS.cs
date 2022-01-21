using System;
using Xamarin.Forms;
using System.Threading.Tasks;
using XamarinFormsFirebase.iOS.Services;
using XamarinFormsFirebase.Services;
using Firebase.Auth;
using Foundation;

[assembly: Dependency(typeof(FirebaseAuthIOS))]
namespace XamarinFormsFirebase.iOS.Services
{
    public class FirebaseAuthIOS : IFirebaseAuth
    {
        private TaskCompletionSource<bool> _phoneAuthTcs;
        private string _verificationId;

        public Task<bool> VerifyOtpCodeAsync(string code)
        {
            if (!string.IsNullOrWhiteSpace(_verificationId))
            {
                var credential = PhoneAuthProvider.DefaultInstance.GetCredential(_verificationId, code);
                var tcs = new TaskCompletionSource<bool>();
                Auth.DefaultInstance.SignInWithCredentialAsync(credential)
                    .ContinueWith((task) => OnAuthCompleted(task, tcs));
                return tcs.Task;
            }
            return Task.FromResult(false);
        }

        private void OnAuthCompleted(Task task, TaskCompletionSource<bool> tcs)
        {
            if (task.IsCanceled || task.IsFaulted)
            {
                tcs.SetResult(false);
                return;
            }
            _verificationId = null;
            tcs.SetResult(true);
        }

        public Task<bool> SendOTPCodeAsync(string mobileNumber)
        {
            _phoneAuthTcs = new TaskCompletionSource<bool>();

            PhoneAuthProvider.DefaultInstance.VerifyPhoneNumber(
                mobileNumber,
                null,
                new VerificationResultHandler(OnVerificationResult));

            return _phoneAuthTcs.Task;
        }

        private void OnVerificationResult(string verificationId, NSError error)
        {
            if(error != null)
            {
                _phoneAuthTcs?.TrySetResult(false);
                return;
            }
            _verificationId = verificationId;
            _phoneAuthTcs?.TrySetResult(true);
        }

        public string GetUserId()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user.Uid;
        }

        public bool IsSignIn()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public bool SignOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await newUser.User.GetIdTokenAsync();
            }
            catch (Exception e)
            {
                return string.Empty;
            }
        }
    }
}

