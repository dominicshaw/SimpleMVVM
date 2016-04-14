using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using SimpleMVVM.Annotations;
using SimpleMVVM.Models;
using SimpleMVVM.MVVM;
using SimpleMVVM.Views;

namespace SimpleMVVM.ViewModels
{
    public class MainViewModel :INotifyPropertyChanged
    {
        private PersonViewModel _selectedPerson;
        private bool _working;
        public ObservableCollection<PersonViewModel> People { get;  } = new ObservableCollection<PersonViewModel>();

        public PersonViewModel SelectedPerson
        {
            get { return _selectedPerson; }
            set
            {
                if (Equals(value, _selectedPerson)) return;
                _selectedPerson = value;
                OnPropertyChanged();
            }
        }

        public ICommand ShowPersonCommand => new DelegateCommand(ShowPerson);
        public ICommand DeletePersonCommand => new AsyncCommand(DeletePerson);
        public ICommand AddPersonCommand => new DelegateCommand(AddPerson);

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

        public static async Task<MainViewModel> Get()
        {
            var mvm = new MainViewModel();
            await mvm.Load();

            return mvm;
        }

        private async Task Load()
        {
            var people = await Person.GetAll();

            foreach (var p in people)
                People.Add(new PersonViewModel(p));
        }

        private void ShowPerson()
        {
            if (SelectedPerson == null)
                return;

            new ViewPerson(SelectedPerson).Show();
        }

        private async Task DeletePerson()
        {
            if (SelectedPerson == null)
                return;

            try
            {
                Working = true;
                await Task.Delay(2000); // simulated DB delete
                People.Remove(SelectedPerson);
            }
            finally
            {
                Working = false;
            }
        }

        private void AddPerson()
        {
            var p =
                new PersonViewModel(
                    new Person() { PersonID = People.Max(x => x.PersonID) + 1 });

            People.Add(p);

            new ViewPerson(p).Show();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
