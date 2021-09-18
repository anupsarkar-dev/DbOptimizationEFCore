using System.Collections.Generic;
using System.Dynamic;

namespace KoopDB.Extensions
{
    public static class PropertyExtensions
    {
        public static List<T> ToDynamicList<T>(this T type)
        {
            return new();
        }


        internal static void SetPropertyValue(this object obj, string propName, object value)
        {
            obj?.GetType().GetProperty(propName)?.SetValue(obj, value, null);
        }


        internal static object GetPropertyValue(this object obj, string propName)
        {
            return obj.GetType().GetProperty(propName)?.GetValue(obj);
        }

       
    }
}
