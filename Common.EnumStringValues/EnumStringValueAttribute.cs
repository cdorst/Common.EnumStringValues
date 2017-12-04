using System;

namespace Common.EnumStringValues
{
    [AttributeUsage(AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    public sealed class EnumStringValueAttribute : Attribute
    {
        private readonly string _value;

        public EnumStringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value => _value;
    }
}
