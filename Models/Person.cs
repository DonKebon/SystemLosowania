

using System.ComponentModel;

namespace SystemLosowania.Models
{
    internal class Person : INotifyPropertyChanged
    {
        public int Count { get; set; }

        private string personName;
        public string PersonName
        {
            get => personName;
            set
            {
                if (personName != value)
                {
                    personName = value;
                    OnPropertyChanged(nameof(PersonName));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
