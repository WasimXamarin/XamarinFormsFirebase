using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using XamarinFormsFirebase.Models;

namespace XamarinFormsFirebase.AuthHelper
{
    public static class FirebaseHelperAddData
    {
        static FirebaseClient firebase = new FirebaseClient("https://fir-xanarinforms-default-rtdb.firebaseio.com/");

        public static async Task<List<MyDatabaseRecord>> GetAllRecords()
        {
            return (await firebase
              .Child("Reords")
              .OnceAsync<MyDatabaseRecord>()).Select(item => new MyDatabaseRecord
              {
                  MyProperty = item.Object.MyProperty,
                  UserIdValue = item.Object.UserIdValue
              }).ToList();
        }

        public static async Task AddReord(string myProperty, string userIdValue)
        {
            await firebase
              .Child("Reords")
              .PostAsync(new MyDatabaseRecord()
              {
                  MyProperty = myProperty,
                  UserIdValue = userIdValue
              });
        }

        public static async Task<MyDatabaseRecord> GetReord(string userIdValue)
        {
            var allRecords = await GetAllRecords();
            await firebase
              .Child("Reords")
              .OnceAsync<MyDatabaseRecord>();
            return allRecords.Where(a => a.UserIdValue == userIdValue).FirstOrDefault();
        }

        public static async Task UpdateReord(string userIdValue, string myProperty)
        {
            var toUpdateRecords = (await firebase
              .Child("Reords")
              .OnceAsync<MyDatabaseRecord>()).Where(a => a.Object.UserIdValue == userIdValue).FirstOrDefault();

            await firebase
              .Child("Reords")
              .Child(toUpdateRecords.Key)
              .PutAsync(new MyDatabaseRecord()
              {
                  UserIdValue = userIdValue,
                  MyProperty = myProperty
              });
        }

        public static async Task DeleteReord(string userIdValue)
        {
            var toDeleteRecord = (await firebase
              .Child("Reords")
              .OnceAsync<MyDatabaseRecord>()).Where(a => a.Object.UserIdValue == userIdValue).FirstOrDefault();
            await firebase.Child("Reords").Child(toDeleteRecord.Key).DeleteAsync();
        }
    }
}
