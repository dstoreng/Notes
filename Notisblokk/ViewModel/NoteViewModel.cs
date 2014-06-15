using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace Notisblokk
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes { get { return _notes; } }

        public NoteViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            _notes = await IsolatedStorageOperations.Load<ObservableCollection<Note>>(MainPage.FILENAME);
            _notes.CollectionChanged += _notes_CollectionChanged;
            NotifyPropertyChanged("collection loaded");
        }

        private async void SaveData()
        {
            await _notes.Save(MainPage.FILENAME);            
        }

        private void _notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("collection changed");    
            SaveData();
        }

        public void AddNote(String description, String content)
        {
            Note n = new Note(description, content);
            _notes.Add(n);
            SaveData();
        }

        public void ChangeDetails(Note n, String description, String content)
        {
            n.Description = description;
            n.Content = content;
            SaveData();
        }

        public void Delete(Note n)
        {
            _notes.Remove(n);
            SaveData();
        }

       public Note GetItem(String id)
        {
            foreach (Note n in _notes)
            {
                if (n.Id.Equals(id))
                    return n;
            }
            return null;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
