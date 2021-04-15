﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Imast.Ext.Core
{
    /// <summary>
    /// A set of extensions for string
    /// </summary>
    public static class Str
    {
        /// <summary>
        /// Checks if given string is empty, null or whitespace
        /// </summary>
        /// <param name="str">The given string to test</param>
        /// <returns></returns>
        public static bool IsBlank(this string str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Convert bytes to base64 string
        /// </summary>
        /// <param name="bytes">The bytes to convert</param>
        /// <returns></returns>
        public static string ToBase64(this byte[] bytes)
        {
            return bytes == null ? null : Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Checks if given string is not empty, null or whitespace
        /// </summary>
        /// <param name="str">The given string to test</param>
        /// <returns></returns>
        public static bool IsNotBlank(this string str)
        {
            return !string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Removes XML characters from a string
        /// </summary>
        /// <param name="text">String to remove XML characters</param>
        public static string EscapeXMLCharacters(this string text)
        {
            // From xml spec valid chars: 
            // #x9 | #xA | #xD | [#x20-#xD7FF] | [#xE000-#xFFFD] | [#x10000-#x10FFFF]     
            // any Unicode character, excluding the surrogate blocks, FFFE, and FFFF. 
            //string re = @"[^\x09\x0A\x0D\x20-\xD7FF\xE000-\xFFFD\x10000-x10FFFF]";
            return Regex.Replace(text, @"\x01", "|");
        }

        /// <summary>
        /// Removes CRLF and tab characters from string
        /// </summary>
        /// <param name="text">String to remove characters</param>
        /// <returns>String</returns>
        public static string EscapeCRLFTabs(this string text)
        {
            return Regex.Replace(text, @"\t|\n|\r", " ");
        }

        /// <summary>
        /// Parse value to given type
        /// </summary>
        /// <param name="value">The value to parse</param>
        /// <param name="type">The type to parse to</param>
        /// <returns></returns>
        public static object Parse(this string value, Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Char:
                    return char.TryParse(value, out var charValue) ? new char?(charValue) : null;

                case TypeCode.Int16:
                    return short.TryParse(value, out var shortValue) ? new short?(shortValue) : null;

                case TypeCode.Int32:
                    return int.TryParse(value, out var intValue) ? new int?(intValue) : null;

                case TypeCode.Int64:
                    return long.TryParse(value, out var longValue) ? new long?(longValue) : null;

                case TypeCode.Double:
                    return double.TryParse(value, out var doubleValue) ? new double?(doubleValue) : null;

                case TypeCode.Single:
                    return float.TryParse(value, out var floatValue) ? new float?(floatValue) : null;

                case TypeCode.Boolean:
                    return bool.TryParse(value, out var boolValue) ? new bool?(boolValue) : null;

                case TypeCode.SByte:
                    return sbyte.TryParse(value, out var sbyteValue) ? new sbyte?(sbyteValue) : null;

                case TypeCode.Byte:
                    return byte.TryParse(value, out var byteValue) ? new byte?(byteValue) : null;

                case TypeCode.UInt16:
                    return ushort.TryParse(value, out var ushortValue) ? new ushort?(ushortValue) : null;

                case TypeCode.UInt32:
                    return uint.TryParse(value, out var uintValue) ? new uint?(uintValue) : null;

                case TypeCode.UInt64:
                    return ulong.TryParse(value, out var ulongValue) ? new ulong?(ulongValue) : null;

                case TypeCode.Decimal:
                    return decimal.TryParse(value, out var decimalValue) ? new decimal?(decimalValue) : null;

                case TypeCode.DateTime:
                    return DateTime.TryParse(value, out var dateTimeValue) ? new DateTime?(dateTimeValue) : null;

                case TypeCode.String:
                case TypeCode.Object:
                case TypeCode.DBNull:
                    return value;

                default:
                    return null;
            }
        }

        /// <summary>
        /// Split string by size
        /// </summary>
        /// <param name="str">The string to split</param>
        /// <param name="size">The size of parts to split to</param>
        /// <returns></returns>
        public static IEnumerable<string> Split(this string str, int size)
        {
            return Enumerable.Range(0, str.Length / size)
                             .Select(s => str.Substring(s * size, size));
        }

        /// <summary>
        /// Return other value if string is blank
        /// </summary>
        /// <param name="str">Target string</param>
        /// <param name="value">Other value</param>
        /// <returns></returns>
        public static string OnBlank(this string str, string value)
        {
            return str.IsBlank() ? value : str;
        }

        /// <summary>
        /// Checks equality without considering the case
        /// </summary>
        /// <param name="str">The string to compare</param>
        /// <param name="other">The other string</param>
        /// <returns></returns>
        public static bool EqualsIgnoreCase(this string str, string other)
        {
            // nulls are equal
            if (str == null)
            {
                return other == null;
            }

            return str.Equals(other, StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        /// Return other value if string is blank
        /// </summary>
        /// <param name="str">Target string</param>
        /// <param name="value">Other value</param>
        /// <returns></returns>
        public static string OnBlank(this string str, Func<string> value)
        {
            return str.IsBlank() ? value?.Invoke() : str;
        }

        /// <summary>
        /// Check if given word is in given text
        /// </summary>
        /// <param name="str">The given text</param>
        /// <param name="word">The given word</param>
        /// <returns></returns>
        public static bool ContainsWord(this string str, string word)
        {
            if (string.IsNullOrWhiteSpace(str) || string.IsNullOrWhiteSpace(word))
            {
                return false;
            }

            var punctuation = str.Where(char.IsPunctuation).Distinct().ToArray();
            var words = str.Split().Select(x => x.Trim(punctuation));

            return words.Contains(word, StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Upper the first letter of the string
        /// </summary>
        /// <param name="input">The input string</param>
        /// <returns></returns>
        public static string FirstToUpper(this string input)
        {
            if (input.IsBlank())
            {
                return string.Empty;
            }

            return input.First().ToString().ToUpper() + input[1..];
        }
    }
}