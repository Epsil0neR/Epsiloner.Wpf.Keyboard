using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace Epsiloner.Wpf.Keyboard
{
    /// <summary>
    /// Represents single key gesture.
    /// </summary>
    public class Gesture
    {
        /// <summary>Gets the key associated with this <see cref="KeyGesture" />.</summary>
        /// <returns>The key associated with the gesture.  The default value is <see cref="Key.None" />.</returns>
        public Key Key { get; }

        /// <summary>Gets the modifier keys associated with this <see cref="KeyGesture" />.</summary>
        /// <returns>The modifier keys associated with the gesture. The default value is <see cref="ModifierKeys.None" />.</returns>
        public ModifierKeys Modifiers { get; }

        /// <summary>
        /// Creates instance that represents single key gesture.
        /// </summary>
        /// <param name="key">For work only with modifiers, key must be set to <seealso cref="Key.None"/>.</param>
        /// <param name="modifiers"></param>
        public Gesture(Key key, ModifierKeys modifiers = ModifierKeys.None)
        {
            Key = key;
            Modifiers = modifiers;
        }

        /// <summary>Determines whether this <see cref="T:System.Windows.Input.KeyGesture" /> matches the input associated with the specified <see cref="T:System.Windows.Input.InputEventArgs" /> object.</summary>
        /// <param name="key">Pressed key.</param>
        /// <param name="modifiers">Modifier keys.</param>
        /// <returns>
        /// <see langword="true" /> if the event data matches this <see cref="Gesture" />; otherwise, <see langword="false" />.</returns>
        public bool Matches(Key key, ModifierKeys modifiers)
        {
            return IsDefinedKey(key)
                && key == Key
                && Modifiers == modifiers;
        }

        public bool PartiallyMatches(Key key, ModifierKeys modifiers)
        {
            if (key != Key.None)
                return false;

            var inp = GetFlags(modifiers);
            foreach (var m in inp)
            {
                if (!Modifiers.HasFlag(m))
                    return false;
            }

            return true;
        }

        internal static bool IsDefinedKey(Key key)
        {
            if (key >= Key.None) return key <= Key.OemClear;
            return false;
        }

        public bool IsValid()
        {
            return Key != Key.None || Modifiers != ModifierKeys.None;
        }

        public static bool IsValid(Gesture gesture)
        {
            if (gesture == null)
                return false;

            return gesture.IsValid();
        }

        private static IEnumerable<Enum> GetFlags(Enum input)
        {
            foreach (Enum value in Enum.GetValues(input.GetType()))
                if (input.HasFlag(value))
                    yield return value;
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var rv = string.Empty;
            if (Modifiers != ModifierKeys.None)
            {
                var modifiers = GetFlags(Modifiers).Where(x => (ModifierKeys)x != ModifierKeys.None);
                rv = string.Join("+", modifiers);
            }

            if (Modifiers != ModifierKeys.None && Key != Key.None)
            {
                rv += "+" + Key.ToString();
            }
            else if (Key != Key.None)
            {
                rv = Key.ToString();
            }

            return rv;
        }
    }
}
