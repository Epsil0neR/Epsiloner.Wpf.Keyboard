using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard.Tests
{
    [TestClass()]
    public class MultiKeyGestureSerializerTests
    {
        [TestMethod()]
        public void CanConvertToStringTest()
        {
            var s = new MultiKeyGestureSerializer();
            var g = new MultiKeyGesture(new[]
            {
                new Gesture(Key.None, ModifierKeys.Control),
                new Gesture(Key.T),
                new Gesture(Key.E),
                new Gesture(Key.S),
                new Gesture(Key.T),
            });

            Assert.IsTrue(s.CanConvertToString(g, null));
        }

        [TestMethod()]
        public void ConvertToStringTest()
        {
            var s = new MultiKeyGestureSerializer();
            var g = new MultiKeyGesture(new[]
            {
                new Gesture(Key.None, ModifierKeys.Control),
                new Gesture(Key.T),
                new Gesture(Key.E),
                new Gesture(Key.S),
                new Gesture(Key.T),
            });
            var res = s.ConvertToString(g, null);
            Assert.AreEqual("Control T E S T".ToLowerInvariant(), res.ToLowerInvariant());
        }
    }
}