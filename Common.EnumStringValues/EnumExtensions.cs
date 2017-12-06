using System;

namespace Common.EnumStringValues
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Returns the first instance of attribute
        /// <typeparamref name="T"/> for the given enum value
        /// </summary>
        public static T GetAttribute<T>(this Enum value) where T : Attribute
        {
            var type = value.GetType();
            var memberInfo = type.GetMember(value.ToString());
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            return (T)attributes[0];
        }

        /// <summary>
        /// Returns enum string value metadata for the given enum value
        /// </summary>
        public static string GetStringValue(this Enum value)
        {
            var attribute = value.GetAttribute<EnumStringValueAttribute>();
            return attribute == null ? value.ToString() : attribute.Value;
        }

        /// <summary>
        /// Returns the enum value matching the given EnumStringValue attribute value
        /// </summary>
        public static T GetEnumValue<T>(this string value)
        {
            var type = typeof(T);
            if (!type.IsEnum) throw new InvalidOperationException();
            foreach (var field in type.GetFields())
            {
                if (Attribute.GetCustomAttribute(field, typeof(EnumStringValueAttribute)) is EnumStringValueAttribute attribute)
                    if (attribute.Value == value) return (T)field.GetValue(null);
                else
                    if (field.Name == value) return (T)field.GetValue(null);
            }
            return default(T);
        }
    }
}
