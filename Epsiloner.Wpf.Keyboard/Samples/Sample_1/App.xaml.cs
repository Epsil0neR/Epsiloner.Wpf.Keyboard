using System.Windows;
using System.Windows.Input;
using Epsiloner.Wpf.Keyboard;
using Epsiloner.Wpf.Keyboard.KeyBinding;

namespace Sample_1
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        /// <inheritdoc />
        public App()
        {
            var configs = new Configs()
            {
                new Config()
                {
                    Name = "SayHello",
                    Description = "Says hello to user",
                    Gesture = new KeyGesture(Key.F1)
                },
                new Config()
                {
                    Name = "Global.Exit",
                    Description = "Shutdowns application. Press: CTRL, then ALT, then SHIFT with E",
                    Gesture = new MultiKeyGesture(new []
                    {
                        new Gesture(Key.None, ModifierKeys.Control),
                        new Gesture(Key.None, ModifierKeys.Alt),
                        new Gesture(Key.E, ModifierKeys.Shift),
                    })
                }
            };
            Manager.Default.LoadFrom(configs, ManagerUpdateMode.Full);
        }
    }
}
