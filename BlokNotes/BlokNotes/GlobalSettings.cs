using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace BlokNotes
{
    public static class GlobalSettings
    {
        #region File System Settings

        public static string MainSavePath { get; set; }
        public static string SaveNotesPath
        {
            get
            {
                return Path.Combine(MainSavePath, "SavesNotes");
            }
        }

        public delegate void SaveDelegate(string s1, string s2);

        public static event SaveDelegate OnSave;
        public static void OnSave_Invoke(string path, string text)
        {
            OnSave?.Invoke(path, text);
        }

        public static string ReadFromFile(string path)
        {
            return File.ReadAllText(path);
        }

        public delegate List<string> GetFilesDelegate(string path);
        public static GetFilesDelegate GetFiles;

        #endregion

    }
}
