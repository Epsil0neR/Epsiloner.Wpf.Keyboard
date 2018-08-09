using System.Linq;
using System.Windows.Markup;

namespace Epsiloner.Wpf.Keyboard
{
    /// <summary>
    /// Serializes <see cref="MultiKeyGesture"/> to <see cref="System.String"/>.
    /// </summary>
    public class MultiKeyGestureSerializer : ValueSerializer
    {

        /// <inheritdoc />
        public override bool CanConvertToString(object value, IValueSerializerContext context)
        {
            if (value is MultiKeyGesture)
                return true;

            return base.CanConvertToString(value, context);
        }

        /// <inheritdoc />
        public override string ConvertToString(object value, IValueSerializerContext context)
        {
            var v = (MultiKeyGesture)value; //This method is suitable only with 1 type.

            var gestures = v.Gestures.Select(x => x.ToString());
            var rv = string.Join(" ", gestures);

            return rv;
        }
    }
}
