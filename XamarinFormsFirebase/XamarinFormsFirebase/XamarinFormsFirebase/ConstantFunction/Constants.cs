using System;
namespace XamarinFormsFirebase.ConstantFunction
{
    public class Constants
    {
        public static string AppName = "OAuthNativeFlow";

        // OAuth
        // For Google login, configure at https://console.developers.google.com/
        public static string iOSClientId = "249836833607-mksm0eil4699ljcl57ccp1ivrpv3dq7g.apps.googleusercontent.com";
        public static string AndroidClientId = "249836833607-b88hao168qol7535m5fvbiirp7g79vvs.apps.googleusercontent.com";

        // These values do not need changing
        public static string Scope = "com.intuit.quickbooks.accounting";
        public static string AuthorizeUrl = "https://appcenter.intuit.com/connect/oauth2";
        public static string AccessTokenUrl = "https://oauth.platform.intuit.com/oauth2/v1/tokens/bearer";
        public static string UserInfoUrl = "https://accounts.intuit.com/v1/openid_connect/userinfo";


        //public static string Scope = "https://www.googleapis.com/auth/userinfo.email";
        //public static string AuthorizeUrl = "https://accounts.google.com/connect/oauth2";
        //public static string AccessTokenUrl = "https://www.googleapis.com/oauth2/v4/token";
        //public static string UserInfoUrl = "https://www.googleapis.com/oauth2/v2/userinfo";

        //public static string Scope = "http://www.googleapis.com/auth/userinfo.email";
        //public static string AuthorizeUrl = "https://accounts.google.com/o/oauth2/auth2";
        //public static string AccessTokenUrl = "https://oauth2.googleapis.com/token";
        //public static string UserInfoUrl = "http://www.googleapis.com/oauth2/v2/userinfo";


        // Set these to reversed iOS/Android client ids, with :/oauth2redirect appended
        public static string iOSRedirectUrl = "com.googleusercontent.apps.249836833607-mksm0eil4699ljcl57ccp1ivrpv3dq7g:/oauth2redirect";
        public static string AndroidRedirectUrl = "com.googleusercontent.apps.249836833607-b88hao168qol7535m5fvbiirp7g79vvs";
    }
}
