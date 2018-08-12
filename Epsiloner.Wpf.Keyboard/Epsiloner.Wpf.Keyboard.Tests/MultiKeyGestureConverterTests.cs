using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Epsiloner.Wpf.Keyboard.Tests
{
    [TestClass]
    public class MultiKeyGestureConverterTests
    {
        [TestMethod]
        public void MultiKeyGestureConverter_constructor()
        {
            var c = new MultiKeyGestureConverter();
            Assert.IsNotNull(c);
        }

        [DataRow(true, typeof(string))]
        [DataRow(false, typeof(int))]
        [DataRow(false, typeof(Gesture))]
        [DataRow(false, typeof(MultiKeyGesture))]
        [TestMethod]
        public void CanConvertFromTest(bool expected, Type type)
        {
            var c = new MultiKeyGestureConverter();
            Assert.AreEqual(expected, c.CanConvertFrom(null, type));
        }

        [DataRow("a control+b", "a ctrl+b")]
        [DataRow("a control+b", "a control+b")]
        [DataRow("a alt+b c", "a alt+b c")]
        [DataRow("a windows+b", "a win+b")]
        [DataRow("a windows+b", "a windows+b")]
        [DataRow("control t e s t", "ctrl t e s t")]
        [DataRow("control t e s t", "control t e s t")]
        [DataRow("t e s t", "t e s t")]
        [TestMethod]
        public void ConvertFromTest(string expected, string input)
        {
            var c = new MultiKeyGestureConverter();
            var s = new MultiKeyGestureSerializer();

            var o = c.ConvertFrom(input);
            Assert.IsNotNull(o);
            Assert.IsInstanceOfType(o, typeof(MultiKeyGesture));
            var g = o as MultiKeyGesture;
            var t = s.ConvertToString(g, null);

            Assert.AreEqual(expected.ToLowerInvariant(), t.ToLowerInvariant());
        }
    }
}