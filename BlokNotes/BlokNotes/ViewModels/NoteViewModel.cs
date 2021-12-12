using System;
using System.Collections.Generic;
using System.Text;

namespace BlokNotes.ViewModels
{
    public class NoteViewModel : BaseViewModel
    {
        private string _noteTitle;
        public string NoteTitle
        {
            get { return _noteTitle; }
            set { SetProperty(ref _noteTitle, value); }
        }

        private string _noteText;
        public string NoteText
        {
            get { return _noteText; }
            set { SetProperty(ref _noteText, value); }
        }

        //todo test.txt
        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { SetProperty(ref _fileName, value); }
        }


        public NoteViewModel()
        {

        }

        public NoteViewModel(string noteTitle, string noteText)
        {
            NoteTitle = noteTitle;
            NoteText = noteText;
        }

        public NoteViewModel(SerializeModels.NoteSerializeModel serializeModel)
        {
            NoteTitle = serializeModel.NoteTitle;
            NoteText = serializeModel.NoteText;
        }

        public NoteViewModel(string noteTitle, string noteText, string fileSavePath) : this(noteTitle, noteText)
        {
            FileName = fileSavePath;
        }

        public SerializeModels.NoteSerializeModel ToSerializeModel()
        {
            return new SerializeModels.NoteSerializeModel(this);
        }

        public string NoteTextShort
        {
            get
            {
                if (string.IsNullOrEmpty(NoteText))
                    return string.Empty;

                string tmp = string.Empty;

                int stringLength = 25;
                if (stringLength > NoteText.Length)
                    stringLength = NoteText.Length;

                for (int i = 0; i < stringLength; i++)
                {
                    if (NoteText[i] == '\n')
                        break;

                    tmp += NoteText[i];
                }

                if (tmp.Length > 0 && tmp.Length != NoteText.Length)
                    tmp += "...";

                return tmp;
            }
        }

    }
}
