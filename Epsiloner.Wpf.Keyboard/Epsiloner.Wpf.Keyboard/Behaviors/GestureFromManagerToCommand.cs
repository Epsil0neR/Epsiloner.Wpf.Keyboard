using System.Windows;
using System.Windows.Data;
using Epsiloner.Wpf.Keyboard.Converters;
using Epsiloner.Wpf.Keyboard.KeyBinding;

namespace Epsiloner.Wpf.Keyboard.Behaviors
{
    public class GestureFromManagerToCommand : GestureToCommand
    {
        public static DependencyProperty ManagerProperty = DependencyProperty.Register(nameof(Manager), typeof(Manager), typeof(GestureFromManagerToCommand));

        public Manager Manager
        {
            get { return (Manager)GetValue(ManagerProperty) ?? Manager.Default; }
            set { SetValue(ManagerProperty, value); }
        }

        /// <inheritdoc />
        public GestureFromManagerToCommand()
        {
            var c = new ConfigNameAndManagerToGestureMultiConverter();
            var b = new MultiBinding()
            {
                Converter = c
            };
            b.Bindings.Add(new Binding(nameof(ConfigName)) { Source = this, Mode = BindingMode.OneWay });
            b.Bindings.Add(new Binding(nameof(Manager)) { Source = this, Mode = BindingMode.OneWay });

            BindingOperations.SetBinding(this, GestureProperty, b);
        }

        public static DependencyProperty ConfigNameProperty = DependencyProperty.Register(nameof(ConfigName), typeof(string), typeof(GestureFromManagerToCommand));

        public string ConfigName
        {
            get { return (string)GetValue(ConfigNameProperty); }
            set { SetValue(ConfigNameProperty, value); }
        }
    }
}