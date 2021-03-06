﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace Notisblokk.Theme
{
    public class CustomTheme : INotifyPropertyChanged
    {

        public CustomTheme(String x)
        {
            this.name = x;
            changeTheme(x);
        }
        public String name { get; set; }

        private String _title;
        private String _header;
        private String _h1;
        private String _h2;
        private String _h3;
        private String _text;
        private String _Background;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                OnPropertyChanged(Title);
            }
        }

        public string Header
        {
            get
            {
                return _header;
            }
            set
            {
                OnPropertyChanged(Header);
            }
        }

        public string H1
        {
            get
            {
                return _h1;
            }
            set
            {
                OnPropertyChanged(H1);
            }
        }

        public string H2
        {
            get
            {
                return _h2;
            }
            set
            {
                OnPropertyChanged(H2);
            }
        }

        public string H3
        {
            get
            {
                return _h3;
            }
            set
            {
                OnPropertyChanged(H3);
            }
        }

        public string Text
        {
            get
            {
                return _text;
            }
            set
            {
                OnPropertyChanged(Text);
            }
        }

        public string Background
        {
            get
            {
                return _Background;
            }
            set
            {
                OnPropertyChanged(Background);
            }
        }

        public void changeTheme(string x)
        {
            if (x == "Snap")
            {
                _Background = "#FFFFFFFF";
                _text = "#FF000000";
                _h3 = "#FFEA2E59";
                _h2 = "#FFE0E4CC";
                _h1 = "#FFEA2E59";
                _header = "#FF57A590";
                _title = "#FFFFFFFF";
            }

            if (x == "Skyss")
            {
                _Background = "#FFF0F0F0";
                _title = "#FFAA3A28";
                _header = "#FF272224";
                _h1 = "#FFAA3A28";
                _h2 = "#FF4B4948";
                _h3 = "#FFC44D58";
                _text = "#FF272224";
            }

            if (x == "Keli")
            {
                _title = "#FFFCF6E6";
                _header = "#FF9D435D";
                _h1 = "#FFC95F6D";
                _h2 = "#FF9296A1";
                _h3 = "#FF9296A1";
                _text = "#FF000000";
                _Background = "#FFFCF6E6";
            }

        }

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion
    }

}
