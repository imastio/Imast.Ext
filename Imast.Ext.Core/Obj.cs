using System;
using System.Collections.Generic;
using System.Linq;

namespace Imast.Ext.Core
{
    /// <summary>
    /// Set of extension methods for objects in general
    /// </summary>
    public static class Obj
    {
        /// <summary>
        /// Try cast given object to a given type
        /// </summary>
        /// <typeparam name="T">The target type</typeparam>
        /// <param name="source">The object to cast</param>
        /// <param name="result">[out] The result of cast</param>
        /// <returns>True if cast is successful</returns>
        public static bool TryCast<T>(this object source, out T result) where T : class
        {
            // default value
            result = default;

            // if not of type T
            if (!(source is T))
            {
                return false;
            }

            // cast 
            result = (T)source;

            // successful
            return true;
        }

        /// <summary>
        /// Cast value to the given type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static T As<T>(this object value)
        {
            if (!(value is T))
            {
                return default;
            }

            // cast
            return (T)value;
        }

        /// <summary>
        /// Check value is of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool Is<T>(this object value)
        {
            return value is T;
        }

        /// <summary>
        /// Check value is not of type T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNot<T>(this object value)
        {
            return !Is<T>(value);
        }

        /// <summary>
        /// Check if values contains the target
        /// </summary>
        /// <typeparam name="TEnum">Target type</typeparam>
        /// <param name="target">The target</param>
        /// <param name="values">The values</param>
        /// <returns></returns>
        public static bool OneOf<TEnum>(this TEnum target, params TEnum[] values) where TEnum : Enum
        {
            return values.Any(v => v.Equals(target));
        }

        /// <summary>
        /// Check if values contains the target
        /// </summary>
        /// <typeparam name="TEnum">Target type</typeparam>
        /// <param name="target">The target</param>
        /// <param name="values">The values</param>
        /// <returns></returns>
        public static bool OneOf<TEnum>(this TEnum target, IEnumerable<TEnum> values) where TEnum : Enum
        {
            return values.Any(v => v.Equals(target));
        }
    }
}