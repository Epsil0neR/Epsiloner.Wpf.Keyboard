using System;
using System.Globalization;
using System.Windows.Data;
using Epsiloner.Wpf.Keyboard.KeyBinding;

namespace Epsiloner.Wpf.Keyboard.Converters
{
    internal class ConfigNameAndManagerToGestureMultiConverter : IMultiValueConverter
    {
        /// <inheritdoc />
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {

            var name = values[0] as string;
            var manager = values[1] as Manager ?? Manager.Default;
            return manager?[name];
        }

        /// <inheritdoc />
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}