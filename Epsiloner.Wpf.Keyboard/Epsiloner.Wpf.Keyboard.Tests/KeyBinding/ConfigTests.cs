using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard.KeyBinding.Tests
{
    [TestClass]
    public class ConfigTests
    {
        [TestMethod]
        public void ConfigTest()
        {
            var c = new Config();
            Assert.IsNotNull(c);
        }

        [TestMethod]
        public void LockTest()
        {
            var c = new Config();
            Assert.IsFalse(c.IsLocked);
            c.Lock();
            Assert.IsTrue(c.IsLocked);
        }

        [TestMethod]
        public void LockTest_doubleCallDoNothing()
        {
            var c = new Config();
            Assert.IsFalse(c.IsLocked);
            c.Lock();
            Assert.IsTrue(c.IsLocked);
            c.Lock();
            Assert.IsTrue(c.IsLocked);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LockTest_throws_InvalidOperationException_Name_set()
        {
            var c = new Config()
            {
                Name = "Test"
            };
            c.Lock();
            Assert.AreEqual("Test", c.Name);
            c.Name = "new name";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LockTest_throws_InvalidOperationException_Description_set()
        {
            var c = new Config()
            {
                Description = "Test"
            };
            c.Lock();
            Assert.AreEqual("Test", c.Description);
            c.Description = "new name";
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LockTest_throws_InvalidOperationException_Gesture_set()
        {
            var g = new KeyGesture(Key.F1);
            var c = new Config()
            {
                Gesture = g
            };
            c.Lock();
            Assert.AreEqual(g, c.Gesture);
            c.Gesture = null;
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void LockTest_throws_InvalidOperationException_IsHidden_set()
        {
            var c = new Config()
            {
                IsHidden = true
            };
            c.Lock();
            Assert.AreEqual(true, c.IsHidden);
            c.IsHidden = false;
        }


        [DataRow(true, true)]
        [DataRow(true, false)]
        [DataRow(false, false)]
        [DataRow(false, true)]
        [TestMethod]
        public void CloneTest(bool lockOrig, bool lockClone)
        {
            var g = new KeyGesture(Key.T, ModifierKeys.Alt);
            var c1 = new Config()
            {
                Name = "Test1",
                Description = "Descr1",
                IsHidden = true,
                Gesture = g,
            };
            if (lockOrig)
                c1.Lock();

            var c2 = c1.Clone(lockClone);
            Assert.AreEqual(c1.Name, c2.Name);
            Assert.AreEqual(c1.Description, c2.Description);
            Assert.AreEqual(c1.IsHidden, c2.IsHidden);
            Assert.AreEqual(c1.Gesture, c2.Gesture);
            Assert.AreEqual(lockOrig, c1.IsLocked);
            Assert.AreEqual(lockClone, c2.IsLocked, "Lock status must be set from .Clone() method.");
        }
    }
}