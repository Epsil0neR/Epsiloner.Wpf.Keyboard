using Epsiloner.Wpf.Keyboard;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using keyboard = System.Windows.Input.Keyboard;

namespace Epsiloner.Wpf.Keyboard.Tests
{
    [TestClass]
    public class MultiKeyGestureTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Gestures as null is not allowed")]
        public void MultiKeyGesture_constructor_ArgumentNullException_null()
        {
            var g = new MultiKeyGesture(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException), "Empty collection of gestures is not allowed.")]
        public void MultiKeyGesture_constructor_ArgumentNullException_empty()
        {
            var g = new MultiKeyGesture(new List<Gesture>());
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException), "Collection of gestures must have at least 1 valid gesture.")]
        public void MultiKeyGesture_constructor_ArgumentNullException_onlyInvalidGestures()
        {
            var l = new List<Gesture>()
            {
                new Gesture(Key.None, ModifierKeys.None)
            };
            var g = new MultiKeyGesture(l);
        }


        #region Matches
        [TestMethod]
        public void MultiKeyGesture_Matches()
        {
            //NOTE: do not test modifiers, because they are taken from Keyboard.Modifiers static property.
            var g = new MultiKeyGesture(new Gesture[]
            {
                new Gesture(Key.T, ModifierKeys.None),
                new Gesture(Key.E, ModifierKeys.None),
                new Gesture(Key.S, ModifierKeys.None),
                new Gesture(Key.T, ModifierKeys.None),
            }, TimeSpan.FromSeconds(10));

            var e1 = BuildKeyEventArgs(Key.T);
            var e2 = BuildKeyEventArgs(Key.E);
            var e3 = BuildKeyEventArgs(Key.S);
            var e4 = BuildKeyEventArgs(Key.T);

            var r1 = g.Matches(null, e1);
            var r2 = g.Matches(null, e2);
            var r3 = g.Matches(null, e3);
            var r4 = g.Matches(null, e4);

            Assert.IsTrue(e1.Handled);
            Assert.IsFalse(r1);

            Assert.IsTrue(e2.Handled);
            Assert.IsFalse(r2);

            Assert.IsTrue(e3.Handled);
            Assert.IsFalse(r3);

            Assert.IsFalse(e4.Handled);
            Assert.IsTrue(r4);
        }


        private KeyEventArgs BuildKeyEventArgs(Key key)
        {
            return new KeyEventArgs(
                keyboard.PrimaryDevice,
                new HwndSource(0, 0, 0, 0, 0, "", IntPtr.Zero), // dummy source
                0,
                key)
            {
                RoutedEvent = UIElement.KeyDownEvent
            };
        }
        #endregion
    }
}
