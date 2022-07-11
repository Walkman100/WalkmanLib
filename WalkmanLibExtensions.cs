using System;
using System.Collections.Generic;
using System.Reflection;

public static class WalkmanLibExtensions {
    #region Enums
    public static bool IsDefined<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Enum.IsDefined(typeof(TEnum), value);
    public static string GetName<TEnum>(this TEnum value) where TEnum : struct, Enum =>
        Enum.GetName(typeof(TEnum), value);
    public static bool HasFlag<TEnum>(this TEnum value, TEnum flag) where TEnum : struct, Enum =>
        value.HasFlag(flag);
    public static string[] GetNames<TEnum>() where TEnum : struct, Enum =>
        Enum.GetNames(typeof(TEnum));
    public static TEnum[] GetValues<TEnum>() where TEnum : struct, Enum =>
        (TEnum[])Enum.GetValues(typeof(TEnum));
    public static Type GetUnderlyingType<TEnum>() where TEnum : struct, Enum =>
        Enum.GetUnderlyingType(typeof(TEnum));
    public static TEnum Parse<TEnum>(string value, bool ignoreCase = false) where TEnum : struct, Enum =>
        (TEnum)Enum.Parse(typeof(TEnum), value, ignoreCase);
    /// <summary>Checks if the enum value is defined with <see cref="IsDefined"/>. If true, returns <paramref name="value"/>. If false, throws <see cref="System.ComponentModel.InvalidEnumArgumentException"/>.</summary>
    public static TEnum CheckDefined<TEnum>(this TEnum value) where TEnum : struct, Enum, IConvertible =>
        value.IsDefined() ? value : throw new System.ComponentModel.InvalidEnumArgumentException(nameof(value), value.ToInt32(null), typeof(TEnum));
    #endregion

    #region Nullable
    public static Boolean? NullableParseBool(string value) =>
        string.IsNullOrWhiteSpace(value) ? (Boolean?)null : bool.Parse(value);
    public static Char? NullableParseChar(string value) =>
        string.IsNullOrWhiteSpace(value) ? (Char?)null : char.Parse(value);
    public static Byte? NullableParseByte(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Byte?)null : byte.Parse(value, fp);
    public static Int16? NullableParseShort(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Int16?)null : short.Parse(value, fp);
    public static Int32? NullableParseInt(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Int32?)null : int.Parse(value, fp);
    public static Int64? NullableParseLong(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Int64?)null : long.Parse(value, fp);
    public static Single? NullableParseSingle(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Single?)null : float.Parse(value, fp);
    public static Double? NullableParseDouble(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Double?)null : double.Parse(value, fp);
    public static Decimal? NullableParseDecimal(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (Decimal?)null : decimal.Parse(value, fp);
    public static DateTime? NullableParseDateTime(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (DateTime?)null : DateTime.Parse(value, fp);
    public static DateTime? NullableParseExactDateTime(string value, string format, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (DateTime?)null : DateTime.ParseExact(value, format, fp);
    public static DateTimeOffset? NullableParseDateTimeOffset(string value, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (DateTimeOffset?)null : DateTimeOffset.Parse(value, fp);
    public static DateTimeOffset? NullableParseExactDateTimeOffset(string value, string format, IFormatProvider fp = null) =>
        string.IsNullOrWhiteSpace(value) ? (DateTimeOffset?)null : DateTimeOffset.ParseExact(value, format, fp);
    public static TEnum? NullableParseEnum<TEnum>(string value, bool ignoreCase = false) where TEnum : struct, Enum =>
        string.IsNullOrWhiteSpace(value) ? (TEnum?)null : Parse<TEnum>(value, ignoreCase);

    public static string NullableToString(this Single? value, IFormatProvider fp = null) =>
        !value.HasValue ? null : value.Value.ToString(fp);
    public static string NullableToString(this Double? value, IFormatProvider fp = null) =>
        !value.HasValue ? null : value.Value.ToString(fp);
    public static string NullableToString(this Decimal? value, IFormatProvider fp = null) =>
        !value.HasValue ? null : value.Value.ToString(fp);
    public static string NullableToString(this DateTime? value, IFormatProvider fp = null) =>
        !value.HasValue ? null : value.Value.ToString(fp);
    public static string NullableToString(this DateTimeOffset? value, IFormatProvider fp = null) =>
        !value.HasValue ? null : value.Value.ToString(fp);
    #endregion

    /// <summary>Gets the value associated with the specified key, or <see langword="default"/> if it isn't contained in the <see cref="IDictionary{TKey, TValue}"/>.</summary>
    /// <param name="defaultValue">Value to return if <paramref name="key"/> is not found in the <see cref="IDictionary{TKey, TValue}"/>.</param>
    public static TValue GetValue<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key, TValue defaultValue = default) =>
        !dictionary.TryGetValue(key, out TValue value) ? defaultValue : value;
    /// <summary>Gets the value associated with the specified key, or <see langword="null"/> if it isn't contained in the <see cref="IDictionary{TKey, TValue}"/>.</summary>
    public static TValue? GetValueOrNull<TKey, TValue>(this IReadOnlyDictionary<TKey, TValue> dictionary, TKey key) where TKey : notnull where TValue : struct =>
        !dictionary.TryGetValue(key, out TValue value) ? (TValue?)null : value;

    public static string EmptyToNull(this string input) =>
        string.IsNullOrWhiteSpace(input) ? null : input;

#if NETCOREAPP
#else
    public static void SetDoubleBuffered(this System.Windows.Forms.Control control, bool enable) {
        //thanks to https://stackoverflow.com/a/15268338/2999220
        PropertyInfo doubleBufferPropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
        doubleBufferPropertyInfo.SetValue(control, enable);
    }
#endif
}
