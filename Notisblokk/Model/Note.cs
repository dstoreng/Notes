using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Notisblokk
{
    public class Note : INotifyPropertyChanged
    {
        private String _description;
        private String _content;
        private String _id;
        private DateTime _date;
        public Boolean IsPinned;
        public String Id { get { return _id; } set { _id = value; } }

        public String Description { 
            get { return _description; }
            set { _description = value; 
                OnPropertyChanged("Description"); 
            } 
        }
        public String Content { 
            get { return _content; }
            set { _content = value; 
                OnPropertyChanged("Content"); 
            } 
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value;
                OnPropertyChanged("Date");
            }
        }

        public Note()
        {
        }

        public Note(String desc, String content)
        {
            _id = Guid.NewGuid().ToString();
            Description = desc;
            Content = content;
            Date = DateTime.Now;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
