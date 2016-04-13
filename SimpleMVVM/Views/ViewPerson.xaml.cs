using System.Windows;
using SimpleMVVM.ViewModels;

namespace SimpleMVVM.Views
{
    /// <summary>
    /// Interaction logic for ViewPerson.xaml
    /// </summary>
    public partial class ViewPerson : Window
    {
        public ViewPerson(PersonViewModel p)
        {
            InitializeComponent();

            DataContext = p;
        }
    }
}
