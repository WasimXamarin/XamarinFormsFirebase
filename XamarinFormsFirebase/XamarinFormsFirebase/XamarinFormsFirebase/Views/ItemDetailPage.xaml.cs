using System.ComponentModel;
using Xamarin.Forms;
using XamarinFormsFirebase.ViewModels;

namespace XamarinFormsFirebase.Views
{
    public partial class ItemDetailPage : ContentPage
    {
        public ItemDetailPage()
        {
            InitializeComponent();
            BindingContext = new ItemDetailViewModel();
        }
    }
}
