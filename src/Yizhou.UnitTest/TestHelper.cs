using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using NUnit.Framework;

namespace Yizhou.UnitTest
{
    public class TestHelper
    {
        public static void AreEqual(object expected, object actual)
        {
            Type expectedType = expected.GetType();
            Type actualType = actual.GetType();
            PropertyInfo[] expectedProperties = expectedType.GetProperties();
            foreach (PropertyInfo expectedProperty in expectedProperties)
            {
                PropertyInfo actualPropertyInfo = actualType.GetProperty(expectedProperty.Name);
                if (actualPropertyInfo != null &&
                    actualPropertyInfo.PropertyType == expectedProperty.PropertyType)
                {
                    object expectedValue = expectedProperty.GetValue(expected, null);
                    object actualValue = actualPropertyInfo.GetValue(actual, null);
                    Assert.AreEqual(expectedValue, actualValue);
                }
            }
        }

        public static void FillTestData(object obj)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo property in properties)
            {
                PropertyInfo propertyInfo = type.GetProperty(property.Name);
                if (!propertyInfo.CanWrite)
                {
                    continue;
                }
                if (propertyInfo.PropertyType == typeof(string))
                {
                    propertyInfo.SetValue(obj, Guid.NewGuid().ToString(), null);
                }
                else if(propertyInfo.PropertyType == typeof(double))
                {
                    propertyInfo.SetValue(obj, new Random().NextDouble(), null);
                }
                else if (propertyInfo.PropertyType == typeof(DateTime))
                {
                    propertyInfo.SetValue(obj, DateTime.Now, null);
                }
                else if (propertyInfo.PropertyType == typeof(int))
                {
                    propertyInfo.SetValue(obj, new Random().Next(), null);
                }
                else if (propertyInfo.PropertyType == typeof(float))
                {
                    propertyInfo.SetValue(obj, (float)new Random().Next(), null);
                }
            }
        }
    }
}
