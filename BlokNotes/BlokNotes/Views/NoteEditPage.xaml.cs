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
        public event EventHandler ClosePage;
        public NoteEditPage()
        {
            InitializeComponent();
        }

        public NoteEditPage(ref EventHandler _event) : this()
        {
            ClosePage = _event;
        }

        public NoteEditPage(ViewModels.NoteViewModel model, ref EventHandler _event) : this(model)
        {
            ClosePage = _event;
        }

        public NoteEditPage(ViewModels.NoteViewModel model) : this()
        {
            this.BindingContext = model;
        }

        private void Note_TextChanged(object sender, TextChangedEventArgs e)
        {
            var vm = (ViewModels.NoteViewModel)this.BindingContext;

            if (vm.NoteTitle == string.Empty && vm.NoteText == string.Empty)
                return;

            string text = vm.ToSerializeModel().ConvertToJson();
            string path = vm.FileName;
            GlobalSettings.OnSave_Invoke(path, text);
        }

        private void DeleteButton_Clicked(object sender, EventArgs e)
        {
            var vm = (ViewModels.NoteViewModel)BindingContext;
            vm.NoteTitle = string.Empty;
            vm.NoteText = string.Empty;
            GlobalSettings.DeleteFile(Path.Combine(GlobalSettings.SaveNotesPath, vm.FileName));
            ClosePage?.Invoke(sender, e);
            this.Navigation.PopAsync();
        }
    }
}