using System;
using System.Windows.Markup;

namespace AC.Desktop.Controls.Helpers
{
    public class EnumBindingSourceExtension : MarkupExtension
    {
        #region Fields

        private Type _enumType;

        #endregion

        #region Properties
        public Type EnumType
        {
            get { return _enumType; }
            set
            {
                if (_enumType != value)
                {
                    if (value != null)
                    {
                        Type enumType = Nullable.GetUnderlyingType(value) ?? value;

                        if (!enumType.IsEnum)
                            throw new Exception("Object is not enum");
                    }
                    _enumType = value;
                }
            }
        }

        #endregion

        #region Constructors

        public EnumBindingSourceExtension() { }
        public EnumBindingSourceExtension(Type type)
        {
            _enumType = type;
        }

        #endregion

        #region Override 

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_enumType == null) throw new Exception("Enum type is null");
            Type actualEnumType = Nullable.GetUnderlyingType(_enumType) ?? _enumType;
            var enumValues = Enum.GetValues(actualEnumType);

            if (actualEnumType == _enumType) return enumValues;

            Array tempArray = Array.CreateInstance(actualEnumType, enumValues.Length + 1);
            enumValues.CopyTo(tempArray, 1);
            return tempArray;
        }

        #endregion
    }
}
