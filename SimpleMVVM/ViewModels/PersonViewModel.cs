using System.ComponentModel;
using System.Runtime.CompilerServices;
using SimpleMVVM.Annotations;
using SimpleMVVM.Models;

namespace SimpleMVVM.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly Person _person;

        public int PersonID
        {
            get { return _person.PersonID; }
            set { _person.PersonID = value; }
        }

        public string FirstName
        {
            get { return _person.FirstName; }
            set
            {
                _person.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get { return _person.LastName; }
            set
            {
                _person.LastName = value;
                OnPropertyChanged();
            }
        }

        public PersonViewModel(Person p)
        {
            _person = p;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
