using System;
using System.Threading.Tasks;
using XamarinFormsFirebase.Services;

using Xamarin.Forms;
using Firebase.Auth;
using Android.Gms.Extensions;
using XamarinFormsFirebase.Droid.Services;
using Java.Util.Concurrent;
using Xamarin.Essentials;
using Firebase;
using XamarinFormsFirebase.ConstantFunction;

[assembly: Dependency(typeof(FirebaseAuthAndroid))]
namespace XamarinFormsFirebase.Droid.Services
{
    public class FirebaseAuthAndroid : PhoneAuthProvider.OnVerificationStateChangedCallbacks, IFirebaseAuth
    {
        const int OTP_TIMEOUT = 30;
        private TaskCompletionSource<bool> _phoneAuthTcs;

        public string _verificationId { get; private set; }

        public Task<bool> VerifyOtpCodeAsync(string code)
        {
            if(!string.IsNullOrWhiteSpace(_verificationId))
            {
                var crediential = PhoneAuthProvider.GetCredential(_verificationId, code);
                var tcs = new TaskCompletionSource<bool>();
                FirebaseAuth.Instance.SignInWithCredentialAsync(crediential)
                    .ContinueWith((task) => OnAuthCompleted(task, tcs));
                return tcs.Task;
            }
            return Task.FromResult(false);
        }

        private void OnAuthCompleted(Task task, TaskCompletionSource<bool> tcs)
        {
            if(task.IsCanceled || task.IsFaulted)
            {
                tcs.SetResult(false);
                return;
            }
            _verificationId = null;
            tcs.SetResult(true);
        }

        [Obsolete]
        public Task<bool> SendOTPCodeAsync(string mobileNumber)
        {
            try
            {
                _phoneAuthTcs = new TaskCompletionSource<bool>();
                PhoneAuthProvider.Instance.VerifyPhoneNumber(mobileNumber, OTP_TIMEOUT, TimeUnit.Seconds, Platform.CurrentActivity, this);
                return _phoneAuthTcs.Task;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public override void OnVerificationCompleted(PhoneAuthCredential credential)
        {
            System.Diagnostics.Debug.WriteLine("Phone Auth Credentail created Automatically.");
        }

        public override void OnVerificationFailed(FirebaseException exception)
        {
            System.Diagnostics.Debug.WriteLine("Verification Failed: " + exception.Message);
            _phoneAuthTcs?.TrySetResult(false);
        }

        public override void OnCodeSent(string verificationId, PhoneAuthProvider.ForceResendingToken forceResendingToken)
        {
            base.OnCodeSent(verificationId, forceResendingToken);
            _verificationId = verificationId;
            _phoneAuthTcs?.TrySetResult(true);
        }




        public string GetUserId()
        {
            var userId = FirebaseAuth.GetInstance(MainActivity.app).CurrentUser;
            return userId.Uid;
        }
        public bool IsSignIn()
        {
            var user = FirebaseAuth.Instance.CurrentUser;
            return user != null;
        }
        public bool SignOut()
        {
            try
            {
                FirebaseAuth.Instance.SignOut();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<string> LoginWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await FirebaseAuth.Instance.SignInWithEmailAndPasswordAsync(email, password);
                return newUser.ToString();
            }
            catch (FirebaseAuthInvalidUserException e)
            {
                e.PrintStackTrace();
                return "Invalid Use";
            }
            catch (FirebaseAuthInvalidCredentialsException e)
            {
                e.PrintStackTrace();
                return "Invalid Auth";
            }
        }

        public async Task<string> SignUpWithEmailAndPassword(string email, string password)
        {
            try
            {
                var newUser = await FirebaseAuth.Instance.CreateUserWithEmailAndPasswordAsync(email, password);
                return newUser.ToString();
            }
            catch(FirebaseAuthInvalidUserException ex)
            {
                ex.PrintStackTrace();
                return "Invalid User";
            }
            catch(FirebaseAuthInvalidCredentialsException ex)
            {
                ex.PrintStackTrace();
                return "Invalid Auth";
            }
            catch (FirebaseAuthUserCollisionException ex)
            {
                ex.PrintStackTrace();
                return "User Exist";
            }
            catch (Exception ex)
            {
                return "Inertnal Error";
            }
        }
    }
}

