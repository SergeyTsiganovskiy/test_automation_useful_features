using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace test_automation_useful_features.Helpers
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

        public static Dictionary<string, string> GetEnumOptionsDescriptionsAsDict<T>() where T : Enum
        {
            Dictionary<string, string> enumValList = new Dictionary<string, string>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                FieldInfo fieldInfo = value.GetType().GetField(value.ToString());
                DescriptionAttribute[] attributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                enumValList.Add(value.ToString(), attributes.Length > 0 ? attributes[0].Description : string.Empty);
            }
            return enumValList;
        }
    }
}