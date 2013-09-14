using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace ITI.Common.Utilities.Exceptions
{
    /// <summary>
    /// Provides additional data for the <c>PropertyChanged</c> event.
    /// </summary>
    /// <remarks>
    /// <p>
    /// Provides some additional properties over and above the name of the
    /// property that has changed (which is inherited from the
    /// <see cref="System.ComponentModel.PropertyChangedEventArgs"/> base class).
    /// This allows calling code to determine whether or not a property has
    /// actually changed (i.e. a <c>PropertyChanged</c> event may have been
    /// raised, but the value itself may be equivalent).
    /// </p>
    /// </remarks>
    /// <seealso cref="System.ComponentModel.PropertyChangedEventArgs"/>
    /// <author>Ahmed Al Amir</author>
    [Serializable]
    public class PropertyChangeEventArgs : PropertyChangedEventArgs
    {
        private object oldValue;
        private object newValue;

        /// <summary>
        /// Create a new instance of the
        /// <see cref="ITI.Common.Utilities.Exceptions.PropertyChangeEventArgs"/> class.
        /// </summary>
        /// <param name="propertyName">
        /// The name of the property that was changed.</param>
        /// <param name="oldValue">The old value of the property.</param>
        /// <param name="newValue">the new value of the property.</param>
        public PropertyChangeEventArgs(
            string propertyName, object oldValue, object newValue)
            : base(propertyName)
        {
            this.oldValue = oldValue;
            this.newValue = newValue;
        }

        /// <summary>
        /// Get the old value for the property.
        /// </summary>
        /// <seealso cref="System.ComponentModel.PropertyChangedEventArgs.PropertyName"/>
        public object OldValue
        {
            get { return oldValue; }
        }

        /// <summary>
        /// Get the new value of the property.
        /// </summary>
        /// <seealso cref="System.ComponentModel.PropertyChangedEventArgs.PropertyName"/>
        public object NewValue
        {
            get { return newValue; }
        }
    }
}
