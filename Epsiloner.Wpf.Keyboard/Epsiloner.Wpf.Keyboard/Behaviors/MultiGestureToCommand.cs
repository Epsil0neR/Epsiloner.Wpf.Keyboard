using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;

namespace Epsiloner.Wpf.Keyboard.Behaviors
{
    public class MultiGestureToCommand : BaseGestureToCommand
    {
        public static DependencyProperty GestureProperty = DependencyProperty.Register(nameof(Gesture), typeof(MultiKeyGesture), typeof(MultiGestureToCommand));

        [TypeConverter(typeof(MultiKeyGestureConverter))]
        [ValueSerializer(typeof(MultiKeyGestureSerializer))]
        public MultiKeyGesture Gesture
        {
            get { return (MultiKeyGesture)GetValue(GestureProperty); }
            set { SetValue(GestureProperty, value); }
        }

        protected override KeyGesture GestureValue => Gesture;
    }
}