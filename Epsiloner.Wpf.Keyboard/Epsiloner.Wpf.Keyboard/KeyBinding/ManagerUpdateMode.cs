namespace Epsiloner.Wpf.Keyboard.KeyBinding
{
    /// <summary>
    /// Manager update mode.
    /// </summary>
    public enum ManagerUpdateMode
    {
        /// <summary>
        /// Can only update gestures for existing configs.
        /// </summary>
        OnlyUpdateExisting,

        /// <summary>
        /// Updates existing gestures and creates new.
        /// </summary>
        Full,
    }
}