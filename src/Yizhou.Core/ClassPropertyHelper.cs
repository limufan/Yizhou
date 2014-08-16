using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Yizhou.Core
{
    public class ClassPropertyHelper
    {
        public static void ChangeProperty(object target, object source)
        {
            Type sourceType = source.GetType();
            Type targetType = target.GetType();
            PropertyInfo[] sourceProperties = sourceType.GetProperties();
            foreach (PropertyInfo sourceProperty in sourceProperties)
            {
                string propertyName = sourceProperty.Name;
                PropertyInfo targetPropertyInfo = targetType.GetProperty(propertyName);
                if (targetPropertyInfo == null)
                {
                    propertyName = propertyName[0].ToString().ToLower() + propertyName.Substring(1);
                    targetPropertyInfo = targetType.GetProperty(propertyName);
                }
                if (targetPropertyInfo == null)
                {
                    propertyName = propertyName[0].ToString().ToUpper() + propertyName.Substring(1);
                    targetPropertyInfo = targetType.GetProperty(propertyName);
                }
                if (targetPropertyInfo != null && 
                    targetPropertyInfo.PropertyType == sourceProperty.PropertyType && 
                    targetPropertyInfo.CanWrite)
                {
                    object changeValue = sourceProperty.GetValue(source, null);
                    targetPropertyInfo.SetValue(target, changeValue, null);
                }
            }
        }
    }
}
