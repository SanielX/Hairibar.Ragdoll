using UnityEngine;

//Largely based on https://github.com/lordofduct/spacepuppy-unity-framework
//Copyright(c) 2015, Dylan Engelman, Jupiter Lighthouse Studio
//Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.


namespace Hairibar.NaughtyExtensions.Editor
{
    internal static class ConversionUtilities
    {
        #region Color
        public static Color ToColor(this Color32 value)
        {
            return new Color(value.r / 255f,
                              value.g / 255f,
                              value.b / 255f,
                              value.a / 255f);
        }

        public static Color ToColor(object value)
        {
            if (value is Color color) return color;
            else if (value is Color32 color32) return color32;
            else throw new InvalidConversionException();
        }


        public static Color32 ToColor32(this Color value)
        {
            return new Color32((byte) (value.r * 255f),
                               (byte) (value.g * 255f),
                               (byte) (value.b * 255f),
                               (byte) (value.a * 255f));
        }

        public static Color32 ToColor32(object value)
        {
            if (value is Color32 color32) return color32;
            else if (value is Color color) return ToColor32(color);
            else throw new InvalidConversionException();
        }
        #endregion

        #region ToEnum
        public static T ToEnum<T>(string val, T defaultValue = default) where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

            try
            {
                T result = (T) System.Enum.Parse(typeof(T), val, true);
                return result;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ToEnum<T>(int val, T defaultValue = default) where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

            try
            {
                return (T) System.Enum.ToObject(typeof(T), val);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ToEnum<T>(long val, T defaultValue = default) where T : struct, System.IConvertible
        {
            if (!typeof(T).IsEnum) throw new System.ArgumentException("T must be an enumerated type");

            try
            {
                return (T) System.Enum.ToObject(typeof(T), val);
            }
            catch
            {
                return defaultValue;
            }
        }

        public static T ToEnum<T>(object val, T defaultValue = default) where T : struct, System.IConvertible
        {
            return ToEnum<T>(System.Convert.ToString(val), defaultValue);
        }
        #endregion

        #region ConvertToInt

        public static int ToInt(sbyte value)
        {
            return System.Convert.ToInt32(value);
        }

        public static int ToInt(byte value)
        {
            return System.Convert.ToInt32(value);
        }

        public static int ToInt(short value)
        {
            return System.Convert.ToInt32(value);
        }

        public static int ToInt(ushort value)
        {
            return System.Convert.ToInt32(value);
        }

        public static int ToInt(int value)
        {
            return value;
        }

        public static int ToInt(uint value)
        {
            if (value > int.MaxValue)
            {
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            }
            else
            {
                return System.Convert.ToInt32(value & 0xffffffff);
            }
        }

        public static int ToInt(long value)
        {
            if (value > int.MaxValue)
            {
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            }
            else
            {
                return System.Convert.ToInt32(value & 0xffffffff);
            }
        }

        public static int ToInt(ulong value)
        {
            if (value > int.MaxValue)
            {
                return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            }
            else
            {
                return System.Convert.ToInt32(value & 0xffffffff);
            }
        }

        public static int ToInt(float value)
        {
            return System.Convert.ToInt32(value);
            //if (value > int.MaxValue)
            //{
            //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            //}
            //else
            //{
            //    return System.Convert.ToInt32(value & 0xffffffff);
            //}
        }

        public static int ToInt(double value)
        {
            return System.Convert.ToInt32(value);
            //if (value > int.MaxValue)
            //{
            //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            //}
            //else
            //{
            //    return System.Convert.ToInt32(value & 0xffffffff);
            //}
        }

        public static int ToInt(decimal value)
        {
            return System.Convert.ToInt32(value);
            //if (value > int.MaxValue)
            //{
            //    return int.MinValue + System.Convert.ToInt32(value & 0x7fffffff);
            //}
            //else
            //{
            //    return System.Convert.ToInt32(value & 0xffffffff);
            //}
        }

        public static int ToInt(bool value)
        {
            return value ? 1 : 0;
        }

        public static int ToInt(char value)
        {
            return System.Convert.ToInt32(value);
        }

        public static int ToInt(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToInt32(value);
                }
                catch
                {
                    return 0;
                }
            }
            else
            {
                return ToInt(value.ToString());
            }
        }

        public static int ToInt(string value, System.Globalization.NumberStyles style)
        {
            return ToInt(ToDouble(value, style));
        }
        public static int ToInt(string value)
        {
            return ToInt(ToDouble(value, System.Globalization.NumberStyles.Any));
        }

        #endregion

        #region "ToSingle"

        public static float ToSingle(sbyte value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(byte value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(short value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(ushort value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(int value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(uint value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(long value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(ulong value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(float value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(double value)
        {
            return (float) value;
        }

        public static float ToSingle(decimal value)
        {
            return System.Convert.ToSingle(value);
        }

        public static float ToSingle(bool value)
        {
            return value ? 1 : 0;
        }

        public static float ToSingle(char value)
        {
            return ToSingle(System.Convert.ToInt32(value));
        }

        public static float ToSingle(Vector2 value)
        {
            return value.x;
        }

        public static float ToSingle(Vector3 value)
        {
            return value.x;
        }

        public static float ToSingle(Vector4 value)
        {
            return value.x;
        }

        public static float ToSingle(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToSingle(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
            {
                return ToSingle((Vector2) value);
            }
            else if (value is Vector3)
            {
                return ToSingle((Vector3) value);
            }
            else if (value is Vector4)
            {
                return ToSingle((Vector3) value);
            }
            else
            {
                return ToSingle(value.ToString());
            }
        }

        public static float ToSingle(string value, System.Globalization.NumberStyles style)
        {
            return System.Convert.ToSingle(ToDouble(value, style));
        }
        public static float ToSingle(string value)
        {
            return System.Convert.ToSingle(ToDouble(value, System.Globalization.NumberStyles.Any));
        }
        #endregion

        #region "ToDouble"

        public static double ToDouble(sbyte value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(byte value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(short value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(ushort value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(int value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(uint value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(long value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(ulong value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(float value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(double value)
        {
            return value;
        }

        public static double ToDouble(decimal value)
        {
            return System.Convert.ToDouble(value);
        }

        public static double ToDouble(bool value)
        {
            return value ? 1 : 0;
        }

        public static double ToDouble(char value)
        {
            return ToDouble(System.Convert.ToInt32(value));
        }

        public static double ToDouble(Vector2 value)
        {
            return value.x;
        }

        public static double ToDouble(Vector3 value)
        {
            return value.x;
        }

        public static double ToDouble(Vector4 value)
        {
            return value.x;
        }

        public static double ToDouble(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToDouble(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
            {
                return ToDouble((Vector2) value);
            }
            else if (value is Vector3)
            {
                return ToDouble((Vector3) value);
            }
            else if (value is Vector4)
            {
                return ToDouble((Vector3) value);
            }
            else
            {
                return ToDouble(value.ToString(), System.Globalization.NumberStyles.Any, null);
            }
        }

        /// <summary>
        /// System.Converts any string to a number with no errors.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="style"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static double ToDouble(string value, System.Globalization.NumberStyles style, System.IFormatProvider provider)
        {
            if (string.IsNullOrEmpty(value)) return 0d;

            style = style & System.Globalization.NumberStyles.Any;
            double dbl = 0;
            if (double.TryParse(value, style, provider, out dbl))
            {
                return dbl;
            }
            else
            {
                //test hex
                int i;
                bool isNeg = false;
                for (i = 0; i < value.Length; i++)
                {
                    if (value[i] == ' ' || value[i] == '+') continue;
                    if (value[i] == '-')
                    {
                        isNeg = !isNeg;
                        continue;
                    }
                    break;
                }

                if (i < value.Length - 1 &&
                        (
                        (value[i] == '#') ||
                        (value[i] == '0' && (value[i + 1] == 'x' || value[i + 1] == 'X')) ||
                        (value[i] == '&' && (value[i + 1] == 'h' || value[i + 1] == 'H'))
                        ))
                {
                    //is hex
                    style = (style & System.Globalization.NumberStyles.HexNumber) | System.Globalization.NumberStyles.AllowHexSpecifier;

                    if (value[i] == '#') i++;
                    else i += 2;
                    int j = value.IndexOf('.', i);

                    if (j >= 0)
                    {
                        long lng = 0;
                        long.TryParse(value.Substring(i, j - i), style, provider, out lng);

                        if (isNeg)
                            lng = -lng;

                        long flng = 0;
                        string sfract = value.Substring(j + 1).Trim();
                        long.TryParse(sfract, style, provider, out flng);
                        return System.Convert.ToDouble(lng) + System.Convert.ToDouble(flng) / System.Math.Pow(16d, sfract.Length);
                    }
                    else
                    {
                        string num = value.Substring(i);
                        long l;
                        if (long.TryParse(num, style, provider, out l))
                            return System.Convert.ToDouble(l);
                        else
                            return 0d;
                    }
                }
                else
                {
                    return 0d;
                }
            }


            ////################
            ////OLD garbage heavy version

            //if (value == null) return 0d;
            //value = value.Trim();
            //if (string.IsNullOrEmpty(value)) return 0d;

            //#if UNITY_WEBPLAYER
            //			Match m = Regex.Match(value, RX_ISHEX, RegexOptions.IgnoreCase);
            //#else
            //            Match m = Regex.Match(value, RX_ISHEX, RegexOptions.IgnoreCase | RegexOptions.Compiled);
            //#endif

            //if (m.Success)
            //{
            //    long lng = 0;
            //    style = (style & System.Globalization.NumberStyles.HexNumber) | System.Globalization.NumberStyles.AllowHexSpecifier;
            //    long.TryParse(m.Groups["num"].Value, style, provider, out lng);

            //    if (m.Groups["sign"].Value == "-")
            //        lng = -lng;

            //    if (m.Groups["fractional"].Success)
            //    {
            //        long flng = 0;
            //        string sfract = m.Groups["fractional"].Value.Substring(1);
            //        long.TryParse(sfract, style, provider, out flng);
            //        return System.Convert.ToDouble(lng) + System.Convert.ToDouble(flng) / System.Math.Pow(16d, sfract.Length);
            //    }
            //    else
            //    {
            //        return System.Convert.ToDouble(lng);
            //    }

            //}
            //else
            //{
            //    style = style & System.Globalization.NumberStyles.Any;
            //    double dbl = 0;
            //    double.TryParse(value, style, provider, out dbl);
            //    return dbl;

            //}
        }

        public static double ToDouble(string value, System.Globalization.NumberStyles style)
        {
            return ToDouble(value, style, null);
        }

        public static double ToDouble(string value)
        {
            return ToDouble(value, System.Globalization.NumberStyles.Any, null);
        }
        #endregion

        #region "ToDecimal"
        public static decimal ToDecimal(sbyte value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(byte value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(short value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(ushort value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(int value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(uint value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(long value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(ulong value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(float value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(double value)
        {
            return System.Convert.ToDecimal(value);
        }

        public static decimal ToDecimal(decimal value)
        {
            return value;
        }

        public static decimal ToDecimal(bool value)
        {
            return value ? 1 : 0;
        }

        public static decimal ToDecimal(char value)
        {
            return ToDecimal(System.Convert.ToInt32(value));
        }

        public static decimal ToDecimal(Vector2 value)
        {
            return (decimal) value.x;
        }

        public static decimal ToDecimal(Vector3 value)
        {
            return (decimal) value.x;
        }

        public static decimal ToDecimal(Vector4 value)
        {
            return (decimal) value.x;
        }

        public static decimal ToDecimal(object value)
        {
            if (value == null)
            {
                return 0;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToDecimal(value);
                }
                catch
                {
                    return 0;
                }
            }
            else if (value is Vector2)
            {
                return ToDecimal((Vector2) value);
            }
            else if (value is Vector3)
            {
                return ToDecimal((Vector3) value);
            }
            else if (value is Vector4)
            {
                return ToDecimal((Vector3) value);
            }
            else
            {
                return ToDecimal(value.ToString());
            }
        }

        public static decimal ToDecimal(string value, System.Globalization.NumberStyles style)
        {
            return System.Convert.ToDecimal(ToDouble(value, style));
        }
        public static decimal ToDecimal(string value)
        {
            return System.Convert.ToDecimal(ToDouble(value, System.Globalization.NumberStyles.Any));
        }
        #endregion

        #region "ToBool"
        public static bool ToBool(sbyte value)
        {
            return value != 0;
        }

        public static bool ToBool(byte value)
        {
            return value != 0;
        }

        public static bool ToBool(short value)
        {
            return value != 0;
        }

        public static bool ToBool(ushort value)
        {
            return value != 0;
        }

        public static bool ToBool(int value)
        {
            return value != 0;
        }

        public static bool ToBool(uint value)
        {
            return value != 0;
        }

        public static bool ToBool(long value)
        {
            return value != 0;
        }

        public static bool ToBool(ulong value)
        {
            return value != 0;
        }

        public static bool ToBool(float value)
        {
            return value != 0;
        }

        public static bool ToBool(double value)
        {
            return value != 0;
        }

        public static bool ToBool(decimal value)
        {
            return value != 0;
        }

        public static bool ToBool(bool value)
        {
            return value;
        }

        public static bool ToBool(char value)
        {
            return System.Convert.ToInt32(value) != 0;
        }

        public static bool ToBool(object value)
        {
            if (value == null)
            {
                return false;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToBoolean(value);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return ToBool(value.ToString());
            }
        }

        /// <summary>
        /// Converts a string to boolean. Is FALSE greedy.
        /// A string is considered TRUE if it DOES meet one of the following criteria:
        /// 
        /// doesn't read blank: ""
        /// doesn't read false (not case-sensitive)
        /// doesn't read 0
        /// doesn't read off (not case-sensitive)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool ToBool(string str)
        {
            //str = (str + "").Trim().ToLower();
            //return !System.Convert.ToBoolean(string.IsNullOrEmpty(str) || str == "false" || str == "0" || str == "off");

            return !(string.IsNullOrEmpty(str) || str.Equals("false", System.StringComparison.OrdinalIgnoreCase) || str.Equals("0", System.StringComparison.OrdinalIgnoreCase) || str.Equals("off", System.StringComparison.OrdinalIgnoreCase));
        }


        public static bool ToBoolInverse(sbyte value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(byte value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(short value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(ushort value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(int value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(uint value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(long value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(ulong value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(float value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(double value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(decimal value)
        {
            return value != 0;
        }

        public static bool ToBoolInverse(bool value)
        {
            return value;
        }

        public static bool ToBoolInverse(char value)
        {
            return System.Convert.ToInt32(value) != 0;
        }

        public static bool ToBoolInverse(object value)
        {
            if (value == null)
            {
                return false;
            }
            else if (value is System.IConvertible)
            {
                try
                {
                    return System.Convert.ToBoolean(value);
                }
                catch
                {
                    return false;
                }
            }
            else
            {
                return ToBoolInverse(value.ToString());
            }
        }

        /// <summary>
        /// Converts a string to boolean. Is TRUE greedy (inverse of ToBool)
        /// A string is considered TRUE if it DOESN'T meet any of the following criteria:
        /// 
        /// reads blank: ""
        /// reads false (not case-sensitive)
        /// reads 0
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks></remarks>
        public static bool ToBoolInverse(string str)
        {
            //str = (str + "").Trim().ToLower();
            //return (!string.IsNullOrEmpty(str) && str != "false" && str != "0");

            return !string.IsNullOrEmpty(str) &&
                   !str.Equals("false", System.StringComparison.OrdinalIgnoreCase) &&
                   !str.Equals("0", System.StringComparison.OrdinalIgnoreCase) &&
                   !str.Equals("off", System.StringComparison.OrdinalIgnoreCase);
        }
        #endregion

        #region "ToString"

        public static string ToString(sbyte value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(byte value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(short value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(ushort value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(int value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(uint value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(long value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(ulong value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(float value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(double value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(decimal value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(bool value, string sFormat)
        {
            switch (sFormat)
            {
                case "num":
                    return (value) ? "1" : "0";
                case "normal":
                case "":
                case null:
                    return System.Convert.ToString(value);
                default:
                    return System.Convert.ToString(value);
            }
        }

        public static string ToString(bool value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(char value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(object value)
        {
            return System.Convert.ToString(value);
        }

        public static string ToString(string str)
        {
            return str;
        }
        #endregion

        #region ToVector2
        public static Vector2 ToVector2(float value)
        {
            return new Vector2(value, value);
        }

        /// <summary>
        /// Creates Vector2 from X and Y values of a Vector3
        /// </summary>
        /// <param name="vec"></param>
        /// <returns></returns>
        public static Vector2 ToVector2(Vector3 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 ToVector2(Vector4 vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 ToVector2(Quaternion vec)
        {
            return new Vector2(vec.x, vec.y);
        }

        public static Vector2 ToVector2(object value)
        {
            if (value == null) return Vector2.zero;
            if (value is Vector2) return (Vector2) value;
            if (value is Vector3)
            {
                var v = (Vector3) value;
                return new Vector2(v.x, v.y);
            }
            if (value is Vector4)
            {
                var v = (Vector4) value;
                return new Vector2(v.x, v.y);
            }
            if (value is Quaternion)
            {
                var q = (Quaternion) value;
                return new Vector2(q.x, q.y);
            }
            if (value is Color)
            {
                var c = (Color) value;
                return new Vector2(c.r, c.g);
            }

            return ToVector2(System.Convert.ToString(value));
        }

        #endregion

        #region ToVector3

        public static Vector3 ToVector3(float value)
        {
            return new Vector3(value, value, value);
        }

        public static Vector3 ToVector3(Vector2 vec)
        {
            return new Vector3(vec.x, vec.y, 0);
        }

        public static Vector3 ToVector3(Vector4 vec)
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }

        public static Vector3 ToVector3(Quaternion vec)
        {
            return new Vector3(vec.x, vec.y, vec.z);
        }

        public static Vector3 ToVector3(object value)
        {
            if (value == null) return Vector3.zero;
            if (value is Vector2)
            {
                var v = (Vector2) value;
                return new Vector3(v.x, v.y, 0f);
            }
            if (value is Vector3)
            {
                return (Vector3) value;
            }
            if (value is Vector4)
            {
                var v = (Vector4) value;
                return new Vector3(v.x, v.y, v.z);
            }
            if (value is Quaternion)
            {
                var q = (Quaternion) value;
                return new Vector3(q.x, q.y, q.z);
            }
            if (value is Color)
            {
                var c = (Color) value;
                return new Vector3(c.r, c.g, c.b);
            }

            return ToVector3(System.Convert.ToString(value));
        }

        #endregion

        #region ToVector4
        public static Vector4 ToVector4(float value)
        {
            return new Vector4(value, value, value, value);
        }

        public static Vector4 ToVector4(Vector2 vec)
        {
            return new Vector4(vec.x, vec.y, 0f, 0f);
        }

        public static Vector4 ToVector4(Vector3 vec)
        {
            return new Vector4(vec.x, vec.y, vec.z, 0f);
        }

        public static Vector4 ToVector4(Quaternion vec)
        {
            return new Vector4(vec.x, vec.y, vec.z, vec.w);
        }

        public static Vector4 ToVector4(object value)
        {
            if (value == null) return Vector4.zero;
            if (value is Vector2)
            {
                var v = (Vector2) value;
                return new Vector4(v.x, v.y, 0f, 0f);
            }
            if (value is Vector3)
            {
                var v = (Vector3) value;
                return new Vector4(v.x, v.y, v.z, 0f);
            }
            if (value is Vector4)
            {
                return (Vector4) value;
            }
            if (value is Quaternion)
            {
                var q = (Quaternion) value;
                return new Vector4(q.x, q.y, q.z, q.w);
            }
            if (value is Color)
            {
                var c = (Color) value;
                return new Vector4(c.r, c.g, c.b, c.a);
            }
            if (value is Rect)
            {
                var r = (Rect) value;
                return new Vector4(r.x, r.y, r.width, r.height);
            }

            return Vector4.one * ToSingle(value);
        }
        #endregion

        #region ToQuaternion
        public static Quaternion ToQuaternion(Vector2 vec)
        {
            return new Quaternion(vec.x, vec.y, 0f, 0f);
        }

        public static Quaternion ToQuaternion(Vector3 vec)
        {
            return new Quaternion(vec.x, vec.y, vec.z, 0f);
        }

        public static Quaternion ToQuaternion(Vector4 vec)
        {
            return new Quaternion(vec.x, vec.y, vec.z, vec.w);
        }

        public static Quaternion ToQuaternion(object value)
        {
            if (value == null) return Quaternion.identity;
            if (value is Vector2)
            {
                var v = (Vector2) value;
                return new Quaternion(v.x, v.y, 0f, 0f);
            }
            if (value is Vector3)
            {
                var v = (Vector3) value;
                return new Quaternion(v.x, v.y, v.z, 0f);
            }
            if (value is Vector4)
            {
                var v = (Vector4) value;
                return new Quaternion(v.x, v.y, v.z, v.w);
            }
            if (value is Quaternion)
            {
                return (Quaternion) value;
            }

            return ToQuaternion(System.Convert.ToString(value));
        }
        #endregion
    }
}
