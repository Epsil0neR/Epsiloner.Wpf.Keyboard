using System.Windows;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard.Behaviors
{
    public class GestureToCommand : BaseGestureToCommand
    {
        public static DependencyProperty GestureProperty = DependencyProperty.Register(nameof(Gesture), typeof(KeyGesture), typeof(GestureToCommand));

        public KeyGesture Gesture
        {
            get { return (KeyGesture)GetValue(GestureProperty); }
            set { SetValue(GestureProperty, value); }
        }
        protected override KeyGesture GestureValue => Gesture;
    }
}
