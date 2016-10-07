using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Model.Enums
{
    public static class EnumExtensionForDescription<T>
    {      
        public static string GetDescription(T value)
        {
            return ((DescriptionAttribute)Attribute.GetCustomAttribute(
                value.GetType().GetFields(BindingFlags.Public | BindingFlags.Static)
                    .Single(x => x.GetValue(null).Equals(value)),
                typeof(DescriptionAttribute)))?.Description ?? value.ToString();
        }

        public static IEnumerable<string> GetAllDescriptions(IEnumerable<T> enumerable)
        {
            List<string> allValues = new List<string>();
            foreach (T enumValue in enumerable)
            {
                allValues.Add(GetDescription(enumValue));
            }
            return allValues.ToArray();
        }
    }
}
