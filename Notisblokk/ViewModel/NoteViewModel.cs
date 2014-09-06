using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.ObjectModel;
using Notisblokk.Theme;

namespace Notisblokk
{
    public class NoteViewModel : INotifyPropertyChanged
    {
        private readonly String NOTEFILE = "notefile.xml";
        private ObservableCollection<Note> _notes;
        public ObservableCollection<Note> Notes { get { return _notes; } }

        public NoteViewModel()
        {
            LoadData();
        }

        private async void LoadData()
        {
            _notes = await IsolatedStorageOperations.Load<ObservableCollection<Note>>(NOTEFILE);
            _notes.CollectionChanged += _notes_CollectionChanged;
            NotifyPropertyChanged("collection loaded");
        }

        private async void SaveData()
        {
            await _notes.Save(NOTEFILE);            
        }

        private void _notes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            NotifyPropertyChanged("collection changed");    
            SaveData();
        }

        public void AddNote(Note note)
        {
            Note n = GetItem(note.Id);
            
            // Note is not new
            if (n != null)
            {
                n.Description = note.Description;
                n.Content = note.Content;
            }
            // Note is new
            else
            {
                n = new Note(note.Description, note.Content);
                n.IsPinned = false;
                _notes.Add(n);
            }
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

        public void PinnedChanged(Note n)
        {
            if (n.IsPinned)
            {
                n.IsPinned = false;
            }
            else
            {
                foreach (Note t in Notes)
                {
                    if (t.IsPinned == true)
                        t.IsPinned = false;
                }
                n.IsPinned = true;
            }

            SaveData();
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
