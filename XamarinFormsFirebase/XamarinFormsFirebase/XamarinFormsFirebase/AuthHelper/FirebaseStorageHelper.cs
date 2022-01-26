using System;
using System.IO;
using System.Threading.Tasks;
using Firebase.Storage;

namespace XamarinFormsFirebase.AuthHelper
{
    public class FirebaseStorageHelper
    {
        //  FirebaseStorage firebaseStorage = new FirebaseStorage("gs://fir-xanarinforms.appspot.com");

       // FirebaseStorage firebaseStorage = new FirebaseStorage("gs://fir-xanarinforms.appspot.com");

        FirebaseStorage firebaseStorage = new FirebaseStorage("fir-xanarinforms.appspot.com");

        public async Task<string> UploadFile(Stream fileStream, string fileName, string CurrentUserId)
        {
            try
            {
                var imageUrl = await firebaseStorage
               .Child(CurrentUserId)
               .Child(fileName)
               .PutAsync(fileStream);
                return imageUrl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<string> GetFile(string fileName, string CurrentUserId)
        {
            try
            {
                var imagePath = await firebaseStorage
                .Child(CurrentUserId)
                .Child(fileName)
                .GetDownloadUrlAsync();
                return imagePath;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task DeleteFile(string fileName, string CurrentUserId)
        {
            try
            {
                await firebaseStorage
                .Child(CurrentUserId)
                .Child(fileName)
                .DeleteAsync();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
