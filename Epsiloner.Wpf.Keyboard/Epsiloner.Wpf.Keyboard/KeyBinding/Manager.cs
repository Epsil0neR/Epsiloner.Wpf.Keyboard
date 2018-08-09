using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard.KeyBinding
{
    /// <summary>
    /// Manager of key binding configurations.
    /// </summary>
    public class Manager : INotifyPropertyChanged
    {
        public static Manager Default { get; set; } = new Manager();

        public event EventHandler ConfigsChanged;

        public ObservableCollection<Config> Configs { get; }

        public Manager()
        {
            Configs = new ObservableCollection<Config>();
        }

        [IndexerName("Item")]
        public KeyGesture this[string name]
        {
            get { return Configs.FirstOrDefault(x => x.Name == name)?.Gesture; }
        }

        public Configs ToEdit()
        {
            var rv = new Configs();
            rv.AddRange(Configs.Where(x => !x.IsHidden).Select(x => x.Clone(false)));
            return rv;
        }

        public void LoadFrom(IEnumerable<Config> configs, ManagerUpdateMode mode)
        {
            configs = configs.Where(x => !string.IsNullOrWhiteSpace(x.Name) && x.Gesture != null).ToList();

            if (mode == ManagerUpdateMode.Full)
            {
                Configs.Clear();
                foreach (var config in configs)
                {
                    Configs.Add(config.Clone(true));
                }
            }

            if (mode == ManagerUpdateMode.User)
            {
                foreach (var config in configs)
                {
                    var c = Configs.FirstOrDefault(x => x.Name == config.Name && !x.IsHidden);
                    if (c != null)
                    {
                        var ind = Configs.IndexOf(c);
                        var clone = c.Clone(false);
                        clone.Gesture = config.Gesture;

                        Configs[ind] = clone;
                    }
                }
            }

            ConfigsChanged?.Invoke(this, EventArgs.Empty);
            OnPropertyChanged("Item[]");
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}