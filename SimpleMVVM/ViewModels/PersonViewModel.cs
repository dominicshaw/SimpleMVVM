using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleMVVM.Annotations;
using SimpleMVVM.Models;
using SimpleMVVM.MVVM;

namespace SimpleMVVM.ViewModels
{
    public class PersonViewModel : INotifyPropertyChanged
    {
        private readonly Person _person;
        private bool _working;

        public bool Working
        {
            get { return _working; }
            set
            {
                if (value == _working) return;
                _working = value;
                OnPropertyChanged();
            }
        }

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

        public ICommand SaveCommand => new AsyncCommand(Save);

        public PersonViewModel(Person p)
        {
            _person = p;
        }

        public async Task Save()
        {
            try
            {
                Working = true;
                await Task.Delay(3000); // database save
            }
            finally
            {
                Working = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
