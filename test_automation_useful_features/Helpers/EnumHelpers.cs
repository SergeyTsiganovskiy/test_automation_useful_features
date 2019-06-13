using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace ExaLinkRebatesAndFees.Utility.CommonHelpers
{
    public static class EnumHelpers
    {
        public static string GetEnumOptionDescription(Enum value)
        {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes =
                (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            return attributes.Length > 0 ? attributes[0].Description : value.ToString();
        }

        public static Dictionary<string, string> GetEnumOptionsDescriptions<T>() where T : struct
        {
            Type enumType = typeof(T);
            if (enumType.BaseType != typeof(Enum))
                throw new ArgumentException($"{typeof(T)} is not System.Enum");
            Dictionary<string, string> enumValList = new Dictionary<string, string>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                enumValList.Add(value.ToString(), attributes.Length > 0 ? attributes[0].Description : string.Empty);
            }
            return enumValList;
        }

        internal static string GetEnumOptionDescription(object closedProjectTabClolumnHeaders)
        {
            throw new NotImplementedException();
        }
    }
}