using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Firebase.Database;
using Firebase.Database.Query;
using XamarinFormsFirebase.Models;

namespace XamarinFormsFirebase.AuthHelper
{
    public class FirebaseHelperAddData
    {
        private readonly string ChildName = "Reords";
        readonly FirebaseClient firebase = new FirebaseClient("https://fir-xanarinforms-default-rtdb.firebaseio.com/");

        public async Task<List<MyDatabaseRecord>> GetAllRecords()
        {
            return (await firebase
              .Child(ChildName)
              .OnceAsync<MyDatabaseRecord>()).Select(item => new MyDatabaseRecord
              {
                  MyProperty = item.Object.MyProperty,
                  UserIdValue = item.Object.UserIdValue
              }).ToList();
        }

        public async Task AddReord(string myProperty, string userIdValue)
        {
            await firebase
              .Child(ChildName)
              .PostAsync(new MyDatabaseRecord()
              {
                  MyProperty = myProperty,
                  UserIdValue = userIdValue
              });
        }

        public async Task<MyDatabaseRecord> GetReord(string userIdValue)
        {
            var allRecords = await GetAllRecords();
            await firebase
              .Child(ChildName)
              .OnceAsync<MyDatabaseRecord>();
            return allRecords.Where(a => a.UserIdValue == userIdValue).FirstOrDefault();
        }

        public async Task UpdateReord(string userIdValue, string myProperty)
        {
            var toUpdateRecords = (await firebase
              .Child(ChildName)
              .OnceAsync<MyDatabaseRecord>()).Where(a => a.Object.UserIdValue == userIdValue).FirstOrDefault();

            await firebase
              .Child(ChildName)
              .Child(toUpdateRecords.Key)
              .PutAsync(new MyDatabaseRecord()
              {
                  UserIdValue = userIdValue,
                  MyProperty = myProperty
              });
        }

        public async Task DeleteReord(string userIdValue)
        {
            var toDeleteRecord = (await firebase
              .Child(ChildName)
              .OnceAsync<MyDatabaseRecord>()).Where(a => a.Object.UserIdValue == userIdValue).FirstOrDefault();
            await firebase.Child(ChildName).Child(toDeleteRecord.Key).DeleteAsync();
        }
    }
}
