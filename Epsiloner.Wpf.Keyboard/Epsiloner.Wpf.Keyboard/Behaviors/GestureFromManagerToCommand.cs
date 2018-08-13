using System.Windows;
using System.Windows.Data;
using Epsiloner.Wpf.Keyboard.Converters;
using Epsiloner.Wpf.Keyboard.KeyBinding;

namespace Epsiloner.Wpf.Keyboard.Behaviors
{
    /// <summary>
    /// Binds command to gesture from <see cref="Config"/> in <see cref="KeyBinding.Manager"/>.
    /// </summary>
    public class GestureFromManagerToCommand : GestureToCommand
    {
        /// <summary>
        /// Key binding configs manager.
        /// </summary>
        public static DependencyProperty ManagerProperty = DependencyProperty.Register(nameof(Manager), typeof(Manager), typeof(GestureFromManagerToCommand));

        /// <summary>
        /// Key binding configs manager.
        /// </summary>
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

        /// <summary>
        /// Name of <see cref="Config"/>.
        /// </summary>
        public static DependencyProperty ConfigNameProperty = DependencyProperty.Register(nameof(ConfigName), typeof(string), typeof(GestureFromManagerToCommand));

        /// <summary>
        /// Name of <see cref="Config"/>.
        /// </summary>
        public string ConfigName
        {
            get { return (string)GetValue(ConfigNameProperty); }
            set { SetValue(ConfigNameProperty, value); }
        }
    }
}