using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace Epsiloner.Wpf.Keyboard.Behaviors
{
    public abstract class BaseGestureToCommand : Behavior<UIElement>
    {
        public static DependencyProperty CommandProperty = DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(BaseGestureToCommand));
        public static DependencyProperty CommandParameterProperty = DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(BaseGestureToCommand));
        public static DependencyProperty IsEnabledProperty = DependencyProperty.Register(nameof(IsEnabled), typeof(bool), typeof(BaseGestureToCommand), new PropertyMetadata(true));
        public static DependencyProperty ModeProperty = DependencyProperty.Register(nameof(Mode), typeof(GestureToCommandMode), typeof(BaseGestureToCommand), new PropertyMetadata(GestureToCommandMode.Before, ModePropertyChangedCallback));
        public static DependencyProperty ExecuteAsynchronouslyProperty = DependencyProperty.Register(nameof(ExecuteAsynchronously), typeof(bool), typeof(BaseGestureToCommand), new PropertyMetadata(false));
        public static DependencyProperty IgnoreHandledProperty = DependencyProperty.Register(nameof(IgnoreHandled), typeof(bool), typeof(BaseGestureToCommand), new PropertyMetadata(false));

        private static void ModePropertyChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var g = (BaseGestureToCommand)d;

            if (!g._attached)
                return;

            g.DetachHandlers();
            g.AttachHandlers();
        }

        private static Dictionary<KeyEventArgs, Timer> _partially = new Dictionary<KeyEventArgs, Timer>();
        private bool _attached;
        private readonly RoutedEventHandler _routedEventHandler;
        private readonly TimeSpan _partiallyTimespan;

        protected abstract KeyGesture GestureValue { get; }

        public bool IsEnabled
        {
            get { return (bool)GetValue(IsEnabledProperty); }
            set { SetValue(IsEnabledProperty, value); }
        }

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        public GestureToCommandMode Mode
        {
            get { return (GestureToCommandMode)GetValue(ModeProperty); }
            set { SetValue(ModeProperty, value); }
        }

        public bool ExecuteAsynchronously
        {
            get { return (bool)GetValue(ExecuteAsynchronouslyProperty); }
            set { SetValue(ExecuteAsynchronouslyProperty, value); }
        }

        public bool IgnoreHandled
        {
            get { return (bool)GetValue(IgnoreHandledProperty); }
            set { SetValue(IgnoreHandledProperty, value); }
        }


        /// <inheritdoc />
        protected BaseGestureToCommand()
        {
            _partiallyTimespan = new TimeSpan(0, 0, 5); //5 sec
#if DEBUG
            _partiallyTimespan = new TimeSpan(0, 5, 0); //5 min
#endif
            _routedEventHandler = Handler;
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            AttachHandlers();
            _attached = true;
        }

        private void AttachHandlers()
        {
            switch (Mode)
            {
                case GestureToCommandMode.Before:
                    AssociatedObject.AddHandler(UIElement.PreviewKeyDownEvent, _routedEventHandler, true);
                    break;
                case GestureToCommandMode.After:
                    AssociatedObject.AddHandler(UIElement.KeyDownEvent, _routedEventHandler, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void DetachHandlers()
        {
            AssociatedObject.RemoveHandler(UIElement.PreviewKeyDownEvent, _routedEventHandler);
            AssociatedObject.RemoveHandler(UIElement.KeyDownEvent, _routedEventHandler);
        }

        protected override void OnDetaching()
        {
            _attached = false;
            DetachHandlers();
            base.OnDetaching();
        }

        private void Handler(object sender, RoutedEventArgs args)
        {
            KeyHandled(sender, args as KeyEventArgs);
        }


        private void KeyHandled(object sender, KeyEventArgs e)
        {
            var gesture = GestureValue;
            if (gesture == null)
                return;

            var handled = e.Handled;
            if (e.Handled && _partially.ContainsKey(e))
            {
                handled = false;
            }
            if ((handled && !IgnoreHandled) || !IsEnabled)
                return;

            var cmd = Command;
            var param = CommandParameter;

            var matches = gesture.Matches(AssociatedObject, e);
            if (!matches && e.Handled && !_partially.ContainsKey(e))
            {
                PartiallyMatching(e);
            }
            if (matches && cmd?.CanExecute(param) == true)
            {
                e.Handled = true;

                if (ExecuteAsynchronously)
                    Task.Factory.StartNew(() => cmd.Execute(param), TaskCreationOptions.LongRunning);
                else
                    cmd.Execute(param);
            }
        }

        private void PartiallyMatching(KeyEventArgs e)
        {
            if (_partially.ContainsKey(e))
            {
                var timer = _partially[e];
                try
                {
                    timer.Stop();
                    timer.Start();
                }
                catch (Exception) { }
                return;
            }

            var t = new Timer(_partiallyTimespan.TotalMilliseconds)
            {
                AutoReset = false
            };
            t.Elapsed += (sender, args) =>
            {
                _partially.Remove(e);
                t.Stop();
                t.Dispose();
            };

            _partially[e] = t;
            t.Start();
        }
    }
}