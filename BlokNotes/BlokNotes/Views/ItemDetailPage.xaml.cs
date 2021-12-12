using BlokNotes.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace BlokNotes.Views
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