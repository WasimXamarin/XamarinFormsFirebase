using System;
using Xamarin.Forms;
using Xamarin.Essentials;
using XamarinFormsFirebase.AuthHelper;
using System.IO;
using XamarinFormsFirebase.Services;

namespace XamarinFormsFirebase.ViewModels.ImageUploadViewModel
{
    public class ImageUploadViewModel : BaseViewModel
    {
        FirebaseStorageHelper firebaseStorageHelper;
        public readonly IFirebaseAuth firebaseAuth;

        public string CurrentUserId;

        public Command PickCommand { get; set; }
        public Command UploadCommand { get; set; }
        public Command DownloadCommand { get; set; }
        public Command DeleteCommand { get; set; }
        FileResult file;
        public ImageUploadViewModel()
        {
            Title = "Image Upload";
            PickCommand = new Command(OnPickClicked);
            UploadCommand = new Command(OnUploadClicked);
            DownloadCommand = new Command(OnDownloadClicked);
            DeleteCommand = new Command(OnDeleteClicked);

            firebaseStorageHelper = new FirebaseStorageHelper();
            firebaseAuth = DependencyService.Get<IFirebaseAuth>();

            CurrentUserId = firebaseAuth.GetUserId();
        }

        private async void OnDeleteClicked(object obj)
        {
            var newFile = "images (1).jpeg";
            string path = await firebaseStorageHelper.GetFile(newFile, CurrentUserId);
            if (path != null)
            {
                FilePathDisplay = path;
            }
            await firebaseStorageHelper.DeleteFile(newFile, CurrentUserId);
            FilePathDisplay = "testing";
        }

        private async void OnDownloadClicked(object obj)
        {
            var newFile = "images (1).jpeg";
            string path = await firebaseStorageHelper.GetFile(newFile, CurrentUserId);
            if(path != null)
            {
                FilePathDisplay = path;
            }
        }

        private async void OnUploadClicked(object obj)
        {
            FileStream fileStream = File.Create(file.FullPath);
            await firebaseStorageHelper.UploadFile(fileStream, Path.GetFileName(file.FileName), CurrentUserId);
        }

        private async void OnPickClicked(object obj)
        {
            //var photo = await MediaPicker.CapturePhotoAsync();
            //if (photo != null)
            //{
            //    var streamValue = await photo.OpenReadAsync();
            //    ImageProfile = ImageSource.FromStream(() => streamValue);
            //}

            file = await MediaPicker.PickPhotoAsync();
            if (file != null)
            {
                var streamValue = await file.OpenReadAsync();

                ImageProfile = ImageSource.FromStream(() => streamValue);
            }
        }

        private ImageSource _ImageProfile;
        public ImageSource ImageProfile
        {
            get => _ImageProfile;
            set => SetProperty(ref _ImageProfile, value);
        }

        private string _FilePathDisplay;
        public string FilePathDisplay
        {
            get => _FilePathDisplay;
            set => SetProperty(ref _FilePathDisplay, value);
        }
    }
}
