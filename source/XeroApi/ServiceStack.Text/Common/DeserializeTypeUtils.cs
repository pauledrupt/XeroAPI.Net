//
// https://github.com/ServiceStack/ServiceStack.Text
// ServiceStack.Text: .NET C# POCO JSON, JSV and CSV Text Serializers.
//
// Authors:
//   Demis Bellot (demis.bellot@gmail.com)
//
// Copyright 2012 ServiceStack Ltd.
//
// Licensed under the same terms of ServiceStack: new BSD license.
//

using System;
using System.Reflection;

namespace ServiceStack.Text.Common
{
    public class DeserializeTypeUtils
    {
        public static ParseStringDelegate GetParseMethod(Type type)
        {
            var typeConstructor = GetTypeStringConstructor(type);
            if (typeConstructor != null)
            {
                return value => typeConstructor.Invoke(new object[] { value });
            }

            return null;
        }

        /// <summary>
        /// Get the type(string) constructor if exists
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        public static ConstructorInfo GetTypeStringConstructor(Type type)
        {
            foreach (var ci in type.GetConstructors())
            {
                var paramInfos = ci.GetParameters();
                var matchFound = (paramInfos.Length == 1 && paramInfos[0].ParameterType == typeof(string));
                if (matchFound)
                {
                    return ci;
                }
            }
            return null;
        }

    }
}