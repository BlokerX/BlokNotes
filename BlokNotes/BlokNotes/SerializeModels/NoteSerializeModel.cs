using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace BlokNotes.SerializeModels
{
    public class NoteSerializeModel : ISerializeModel
    {
        public string NoteTitle;
        public string NoteText;

        public NoteSerializeModel() { }

        public NoteSerializeModel(string noteTitle, string noteText)
        {
            NoteTitle = noteTitle;
            NoteText = noteText;
        }

        public NoteSerializeModel(ViewModels.NoteViewModel model)
        {
            NoteTitle = model.NoteTitle;
            NoteText = model.NoteText;
        }

        public ViewModels.NoteViewModel ToNoteViewModel()
        {
            return new ViewModels.NoteViewModel(this);
        }

        public string ConvertToJson()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
