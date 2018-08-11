using System;
using System.Windows.Input;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Epsiloner.Wpf.Keyboard.Tests
{
    [TestClass]
    public class GestureTests
    {
        [DataRow(Key.None, ModifierKeys.Alt)]
        [DataRow(Key.None, ModifierKeys.Control)]
        [DataRow(Key.None, ModifierKeys.Shift)]
        [DataRow(Key.None, ModifierKeys.Windows)]
        [DataRow(Key.None, ModifierKeys.Windows | ModifierKeys.Alt)]
        [DataRow(Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control)]
        [DataRow(Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Shift)]
        [DataRow(Key.A, ModifierKeys.Alt)]
        [DataRow(Key.A, ModifierKeys.None)]
        [TestMethod]
        public void Gesture_Constructor(Key key, ModifierKeys modifiers)
        {
            var g = new Gesture(key, modifiers);

            Assert.IsNotNull(g);
            Assert.IsTrue(g.IsValid());
            Assert.AreEqual(key, g.Key);
            Assert.AreEqual(modifiers, g.Modifiers);
        }

        [DataRow(true, Key.None, ModifierKeys.Alt)]
        [DataRow(true, Key.None, ModifierKeys.Control)]
        [DataRow(true, Key.None, ModifierKeys.Shift)]
        [DataRow(true, Key.None, ModifierKeys.Windows)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Shift)]
        [DataRow(true, Key.A, ModifierKeys.Alt)]
        [DataRow(true, Key.A, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.None)]
        [TestMethod]
        public void Gesture_IsValid(bool expected, Key key, ModifierKeys modifiers)
        {
            var g = new Gesture(key, modifiers);
            Assert.AreEqual(expected, g.IsValid());
        }

        [DataRow(true, Key.None, ModifierKeys.Alt)]
        [DataRow(true, Key.None, ModifierKeys.Control)]
        [DataRow(true, Key.None, ModifierKeys.Shift)]
        [DataRow(true, Key.None, ModifierKeys.Windows)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control)]
        [DataRow(true, Key.None, ModifierKeys.Windows | ModifierKeys.Alt | ModifierKeys.Control | ModifierKeys.Shift)]
        [DataRow(true, Key.A, ModifierKeys.Alt)]
        [DataRow(true, Key.A, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.None)]
        [TestMethod]
        public void Gesture_static_IsValid(bool expected, Key key, ModifierKeys modifiers)
        {
            var g = new Gesture(key, modifiers);
            Assert.AreEqual(expected, Gesture.IsValid(g));
        }

        [TestMethod]
        public void Gesture_static_IsValid()
        {
            Assert.IsFalse(Gesture.IsValid(null), "Null cannot be valid gesture.");
        }

        [DataRow(true, Key.A, ModifierKeys.Control, Key.A, ModifierKeys.Control)]
        [DataRow(true, Key.None, ModifierKeys.Control, Key.None, ModifierKeys.Control)]
        [DataRow(true, Key.A, ModifierKeys.None, Key.A, ModifierKeys.None)]
        [DataRow(true, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.Control | ModifierKeys.Alt)]
        [DataRow(false, Key.A, ModifierKeys.None, Key.B, ModifierKeys.None)]
        [DataRow(false, Key.A, ModifierKeys.None, Key.None, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.Control, Key.None, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.Control)]
        [DataRow(false, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.Control | ModifierKeys.Windows)]
        [TestMethod]
        public void Gesture_Matches(bool expected, Key gestureKey, ModifierKeys gestureModifiers, Key inpKey, ModifierKeys inpModifiers)
        {
            var g = new Gesture(gestureKey, gestureModifiers);
            Assert.AreEqual(expected, g.Matches(inpKey, inpModifiers));
        }

        [DataRow(false, Key.A, ModifierKeys.None, Key.B, ModifierKeys.None)]
        [DataRow(true, Key.A, ModifierKeys.None, Key.None, ModifierKeys.None)]
        [DataRow(true, Key.None, ModifierKeys.Control, Key.None, ModifierKeys.None)]
        [DataRow(false, Key.None, ModifierKeys.Control, Key.None, ModifierKeys.Alt)]
        [DataRow(true, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.None)]
        [DataRow(true, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.Control)]
        [DataRow(false, Key.None, ModifierKeys.Control | ModifierKeys.Alt, Key.None, ModifierKeys.Control | ModifierKeys.Windows)]
        [TestMethod]
        public void Gesture_PartiallyMatches(bool expected, Key gestureKey, ModifierKeys gestureModifiers, Key inpKey, ModifierKeys inpModifiers)
        {
            var g = new Gesture(gestureKey, gestureModifiers);
            Assert.AreEqual(expected, g.PartiallyMatches(inpKey, inpModifiers));
        }

        [DataRow("A", Key.A, ModifierKeys.None)]
        [DataRow("Alt+A", Key.A, ModifierKeys.Alt)]
        [DataRow("Alt+Control+A", Key.A, ModifierKeys.Alt | ModifierKeys.Control)]
        [DataRow("Alt+Control", Key.None, ModifierKeys.Alt | ModifierKeys.Control)]

        [TestMethod]
        public void Gesture_ToString(string expected, Key key, ModifierKeys modifiers)
        {
            var g = new Gesture(key, modifiers);
            Assert.AreEqual(expected.ToLowerInvariant(), g.ToString().ToLowerInvariant());
        }
    }
}
