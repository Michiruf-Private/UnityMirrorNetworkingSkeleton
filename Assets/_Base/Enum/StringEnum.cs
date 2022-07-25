using System;

namespace Base
{
    /// <summary>
    /// This attribute is used to represent a string value
    /// for a value in an enum.
    ///
    /// See https://weblogs.asp.net/stefansedich/enum-with-string-values-in-c
    /// </summary>
    public class StringEnumAttribute : Attribute
    {
        /// <summary>
        /// Holds the stringvalue for a value in an enum.
        /// </summary>
        public string stringValue { get; }

        /// <summary>
        /// Constructor used to init a StringValue Attribute
        /// </summary>
        /// <param name="value"></param>
        public StringEnumAttribute(string value)
        {
            stringValue = value;
        }
    }
}
