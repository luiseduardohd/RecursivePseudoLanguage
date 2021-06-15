using System.ComponentModel;
using UnbabelerApp.ViewModels;
using Xamarin.Forms;

namespace UnbabelerApp.Views
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