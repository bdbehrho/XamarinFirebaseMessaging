using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace XamarinFirebaseMessaging.Models
{
    public class Message : INotifyPropertyChanged
    {
        private string _text = null;
        public string Text {
            get { return _text; }
            set {
                _text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }
        public string UserId { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
