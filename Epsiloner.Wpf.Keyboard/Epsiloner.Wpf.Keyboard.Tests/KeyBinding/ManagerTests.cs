using Microsoft.VisualStudio.TestTools.UnitTesting;
using Epsiloner.Wpf.Keyboard.KeyBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard.KeyBinding.Tests
{
    [TestClass()]
    public class ManagerTests
    {
        [TestMethod()]
        public void ManagerTest()
        {
            var m = new Manager();
            Assert.IsNotNull(m);
        }

        [TestMethod()]
        public void ToEditTest()
        {
            var m = new Manager();
            var configs = new Configs()
            {
                new Config()
                {
                    Name = "Test.Help",
                    Gesture = new KeyGesture(Key.F1)
                },
                new Config()
                {
                    Name = "Test.Fullscreen",
                    Gesture = new KeyGesture(Key.F11)
                }
            };
            m.LoadFrom(configs, ManagerUpdateMode.Full);


            var c = m.ToEdit();
            Assert.AreEqual(m.Configs.Count, c.Count, "Configs to edit must contain all items manager contains.");
            foreach (var config in c)
            {
                Assert.IsFalse(config.IsLocked, "Config must be editable (not locked)");
            }
        }


        [TestMethod()]
        public void LoadFromTest_wasEmpty()
        {
            var m = new Manager();
            int pc = 0;
            m.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Item[]")
                    ++pc;
            };
            var c = new Configs()
            {
                new Config()
                {
                    Name = "Test.Help",
                    Gesture = new KeyGesture(Key.F1)
                },
                new Config()
                {
                    Name = "Test.Fullscreen",
                    Gesture = new KeyGesture(Key.F11)
                }
            };

            m.LoadFrom(c, ManagerUpdateMode.Full);
            Assert.AreEqual(c.Count, m.Configs.Count);
            Assert.AreEqual(1, pc);

            foreach (var config in m.Configs)
            {
                Assert.IsTrue(config.IsLocked);
            }
        }

        [TestMethod()]
        public void LoadFromTest_wasNotEmpty()
        {
            var gestureInitial = new KeyGesture(Key.T, ModifierKeys.Control);
            var gestureAfter = new MultiKeyGesture(new[] { new Gesture(Key.None, ModifierKeys.Control), new Gesture(Key.T) });
            var m = new Manager();
            var key = "Test";
            var c = new Configs()
            {
                new Config()
                {
                    Name = "Test.Help",
                    Gesture = new KeyGesture(Key.F1)
                },
                new Config()
                {
                    Name = "Test.Fullscreen",
                    Gesture = new KeyGesture(Key.F11)
                },
                new Config()
                {
                    Name = key,
                    Gesture =gestureInitial
                }
            };

            m.LoadFrom(c, ManagerUpdateMode.Full);

            int pc = 0;
            m.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Item[]")
                    ++pc;
            };

            var c1 = new Configs()
            {
                new Config()
                {
                    Name = "Test.NotExisting",
                    Gesture = new KeyGesture(Key.F12)
                },
                new Config()
                {
                    Name = key,
                    Gesture = gestureAfter
                }
            };
            m.LoadFrom(c1, ManagerUpdateMode.OnlyUpdateExisting);
            Assert.IsNull(m["Test.NotExisting"]);
            Assert.AreEqual(3, m.Configs.Count);
            Assert.AreEqual(1, pc);
            Assert.AreEqual(m[key], gestureAfter);

            foreach (var config in m.Configs)
            {
                Assert.IsTrue(config.IsLocked);
            }
        }
    }
}