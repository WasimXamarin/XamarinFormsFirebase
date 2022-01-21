using System;
using XamarinFormsFirebase.Models;

namespace XamarinFormsFirebase.Services
{
    public interface IGoogleManager
    {
        void Login(Action<GoogleUser, string> OnLoginComplete);

        void Logout();
    }
}
