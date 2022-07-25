using System;

namespace Base
{
    public static class StringEnumExtension
    {
        /// <summary>
        /// Will get the string value for a given enums value, this will
        /// only work if you assign the StringValue attribute to
        /// the items in your enum.
        ///
        /// See https://weblogs.asp.net/stefansedich/enum-with-string-values-in-c
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static string GetStringValue(this Enum value)
        {
            var type = value.GetType();
            var fieldInfo = type.GetField(value.ToString());
            var attributes = (StringEnumAttribute[])fieldInfo.GetCustomAttributes(typeof(StringEnumAttribute), false);
            return attributes.Length > 0 ? attributes[0].stringValue : null;
        }
    }
}
