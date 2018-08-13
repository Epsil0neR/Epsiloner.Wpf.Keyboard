using System.Windows;
using System.Windows.Input;
using Epsiloner.Wpf.Keyboard.KeyBinding;

namespace Sample_2
{
    public class MainWindowViewModel
    {
        /// <inheritdoc />
        public MainWindowViewModel()
        {
            Manager = Manager.Default;

            SayHelloCommand = new RelayCommand(SayHelloCommandHandler);
            ExitCommand = new RelayCommand(ExitCommandHandler);
        }

        public Manager Manager { get; set; }

        public ICommand SayHelloCommand { get; }
        public ICommand ExitCommand { get; }



        private void SayHelloCommandHandler(object obj)
        {
            MessageBox.Show("Hello user!");
        }

        private void ExitCommandHandler(object obj)
        {
            if (MessageBox.Show("Exit application?", "Confirm exit", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                Application.Current.Shutdown();
        }
    }
}
