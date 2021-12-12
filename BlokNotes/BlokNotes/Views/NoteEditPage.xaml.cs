using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlokNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NoteEditPage : ContentPage
    {
        public NoteEditPage()
        {
            InitializeComponent();
        }

        public NoteEditPage(ViewModels.NoteViewModel model) : this()
        {
            this.BindingContext = model;
        }

        private void Note_TextChanged(object sender, TextChangedEventArgs e)
        {
            var nvm = (ViewModels.NoteViewModel)this.BindingContext;
            string text = nvm.ToSerializeModel().ConvertToJson();
            string path = nvm.FileName;
            GlobalSettings.OnSave_Invoke(path, text);
        }
    }
}