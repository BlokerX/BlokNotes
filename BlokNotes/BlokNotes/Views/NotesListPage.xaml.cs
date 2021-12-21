using BlokNotes.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BlokNotes.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotesListPage : ContentPage
    {
        public ObservableCollection<NoteViewModel> Items { get; set; }

        public event EventHandler BackLoad;

        public NotesListPage()
        {
            InitializeComponent();
            LoadItems();
            BackLoad += NotesListPage_BackLoad;
        }

        private void NotesListPage_BackLoad(object sender, EventArgs e)
        {
            LoadItems();
        }

        private void LoadItems()
        {
            Items?.Clear();
            Items = new ObservableCollection<NoteViewModel>();

            var files = GlobalSettings.GetFiles(GlobalSettings.SaveNotesPath);
            foreach (var file in files)
            {
                string tmp = "";
                for (int i = file.LastIndexOf(Path.DirectorySeparatorChar) + 1; i < file.Length; i++)
                {
                    tmp += file[i];
                }
                Items.Add(new NoteViewModel(JsonConvert.DeserializeObject<SerializeModels.NoteSerializeModel>(GlobalSettings.ReadFromFile(file)))
                {
                    FileName = tmp
                });
            }

            MyListView.ItemsSource = Items;
        }

        async void Handle_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (e.Item == null)
                return;

#if DEBUG
            Debug.WriteLine("An item was Tapped");
#endif

            await Navigation.PushAsync(new NoteEditPage(e.Item as NoteViewModel, ref BackLoad));

            //Deselect Item
            ((ListView)sender).SelectedItem = null;
        }

        private async void AddNoteButton_Clicked(object sender, EventArgs e)
        {
            var fn = GlobalSettings.GetFiles(GlobalSettings.SaveNotesPath);
            string nextName = "Note.json";
            int index = 1;
        PETLA:
            foreach (var item in fn)
            {
                if (Path.Combine(GlobalSettings.SaveNotesPath, nextName) == item)
                {
                    nextName = "Note " + ++index + ".json";
                    goto PETLA;
                }
            }

            Items.Add(new NoteViewModel()
            {
                FileName = nextName
            });

#if DEBUG
            Debug.WriteLine("A note was Added");
#endif

            await Navigation.PushAsync(new NoteEditPage(Items.Last(), ref BackLoad));
        }

        private void DeleteNoteButton_Clicked(object sender, EventArgs e)
        {
            var nvm = (NoteViewModel)((Button)sender).BindingContext;
            GlobalSettings.DeleteFile(Path.Combine(GlobalSettings.SaveNotesPath, nvm.FileName));
            LoadItems();
        }
    }
}
