﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Helper.Object
{
    public static class ObjectExtensionMethod
    {
        public static T CopyPropertiesFrom<T>(this T self, object parent)
        {
            var fromProperties = parent.GetType().GetProperties();
            var toProperties = self.GetType().GetProperties();

            foreach (var fromProperty in fromProperties)
            {
                foreach (var toProperty in toProperties)
                {
                    if (fromProperty.Name == toProperty.Name && fromProperty.PropertyType == toProperty.PropertyType)
                    {
                        toProperty.SetValue(self, fromProperty.GetValue(parent));
                        break;
                    }
                }
            }
            return self;
        }
    }
}
