Option Explicit On
Option Strict On
Option Compare Binary
Option Infer Off

Imports System
Imports System.Collections.Generic
Imports System.Reflection
Imports System.Runtime.CompilerServices

Public Module WalkmanLibExtensions
#Region "Enums"
    <Extension()>
    Public Function IsDefined(Of TEnum As Structure)(value As TEnum) As Boolean
        Return [Enum].IsDefined(GetType(TEnum), value)
    End Function
    <Extension()>
    Public Function GetName(Of TEnum As Structure)(value As TEnum) As String
        Return [Enum].GetName(GetType(TEnum), value)
    End Function
    <Extension()>
    Public Function HasFlag(Of TEnum As Structure)(value As TEnum, flag As TEnum) As Boolean
        Return TryCast(value, [Enum]).HasFlag(TryCast(flag, [Enum]))
    End Function
    Public Function GetNames(Of TEnum As Structure)() As String()
        Return [Enum].GetNames(GetType(TEnum))
    End Function
    Public Function GetValues(Of TEnum As Structure)() As TEnum()
        Return CType([Enum].GetValues(GetType(TEnum)), TEnum())
    End Function
    Public Function GetUnderlyingType(Of TEnum As Structure)() As Type
        Return [Enum].GetUnderlyingType(GetType(TEnum))
    End Function
    Public Function Parse(Of TEnum As Structure)(value As String, Optional ignoreCase As Boolean = False) As TEnum
        Return DirectCast([Enum].Parse(GetType(TEnum), value, ignoreCase), TEnum)
    End Function
    ''' <summary>Checks if the enum value is defined with <see cref="IsDefined"/>. If true, return <paramref name="value"/>. If false, throws <see cref="ComponentModel.InvalidEnumArgumentException"/>.</summary>
    <Extension()>
    Public Function CheckDefined(Of TEnum As {Structure, IConvertible})(value As TEnum) As TEnum
        If value.IsDefined() Then Return value _
        Else Throw New ComponentModel.InvalidEnumArgumentException("value", value.ToInt32(Nothing), GetType(TEnum))
    End Function
#End Region

#Region "Nullable"
    Public Function NullableParseBool(value As String) As Boolean?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Boolean.Parse(value))
    End Function
    Public Function NullableParseChar(value As String) As Char?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Char.Parse(value))
    End Function
    Public Function NullableParseByte(value As String, Optional fp As IFormatProvider = Nothing) As Byte?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Byte.Parse(value, fp))
    End Function
    Public Function NullableParseShort(value As String, Optional fp As IFormatProvider = Nothing) As Short?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Short.Parse(value, fp))
    End Function
    Public Function NullableParseInt(value As String, Optional fp As IFormatProvider = Nothing) As Integer?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Integer.Parse(value, fp))
    End Function
    Public Function NullableParseLong(value As String, Optional fp As IFormatProvider = Nothing) As Long?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Long.Parse(value, fp))
    End Function
    Public Function NullableParseSingle(value As String, Optional fp As IFormatProvider = Nothing) As Single?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Single.Parse(value, fp))
    End Function
    Public Function NullableParseDouble(value As String, Optional fp As IFormatProvider = Nothing) As Double?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Double.Parse(value, fp))
    End Function
    Public Function NullableParseDecimal(value As String, Optional fp As IFormatProvider = Nothing) As Decimal?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Decimal.Parse(value, fp))
    End Function
    Public Function NullableParseDateTime(value As String, Optional fp As IFormatProvider = Nothing) As DateTime?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, DateTime.Parse(value, fp))
    End Function
    Public Function NullableParseExactDateTime(value As String, <StringSyntax(StringSyntaxAttribute.DateTimeFormat)> format As String, Optional fp As IFormatProvider = Nothing) As DateTime?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, DateTime.ParseExact(value, format, fp))
    End Function
    Public Function NullableParseDateTimeOffset(value As String, Optional fp As IFormatProvider = Nothing) As DateTimeOffset?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, DateTimeOffset.Parse(value, fp))
    End Function
    Public Function NullableParseExactDateTimeOffset(value As String, <StringSyntax(StringSyntaxAttribute.DateTimeFormat)> format As String, Optional fp As IFormatProvider = Nothing) As DateTimeOffset?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, DateTimeOffset.ParseExact(value, format, fp))
    End Function
    Public Function NullableParseEnum(Of TEnum As Structure)(value As String, Optional ignoreCase As Boolean = False) As TEnum?
        Return If(String.IsNullOrWhiteSpace(value), Nothing, Parse(Of TEnum)(value, ignoreCase))
    End Function

    <Extension()>
    Public Function NullableToString(value As Single?, Optional fp As IFormatProvider = Nothing) As String
        Return If(Not value.HasValue, Nothing, value.Value.ToString(fp))
    End Function
    <Extension()>
    Public Function NullableToString(value As Double?, Optional fp As IFormatProvider = Nothing) As String
        Return If(Not value.HasValue, Nothing, value.Value.ToString(fp))
    End Function
    <Extension()>
    Public Function NullableToString(value As Decimal?, Optional fp As IFormatProvider = Nothing) As String
        Return If(Not value.HasValue, Nothing, value.Value.ToString(fp))
    End Function
    <Extension()>
    Public Function NullableToString(value As Date?, Optional fp As IFormatProvider = Nothing) As String
        Return If(Not value.HasValue, Nothing, value.Value.ToString(fp))
    End Function
    <Extension()>
    Public Function NullableToString(value As DateTimeOffset?, Optional fp As IFormatProvider = Nothing) As String
        Return If(Not value.HasValue, Nothing, value.Value.ToString(fp))
    End Function
#End Region

    ''' <summary>Gets the value associated with the specified <paramref name="key"/>, or <paramref name="defaultValue"/> if it isn't contained in the <see cref="IDictionary(Of TKey, TValue)"/>.</summary>
    ''' <param name="defaultValue">Value to return if <paramref name="key"/> is not found in the <see cref="IDictionary(Of TKey, TValue)"/>.</param>
    <Extension()>
    Public Function GetValue(Of TKey, TValue)(dictionary As IReadOnlyDictionary(Of TKey, TValue), key As TKey, Optional defaultValue As TValue = Nothing) As TValue
        Dim value As TValue
        Return If(Not dictionary.TryGetValue(key, value), defaultValue, value)
    End Function
    ''' <summary>Gets the value associated with the specified <paramref name="key"/>, or <see langword="Nothing"/> if it isn't contained in the <see cref="IDictionary(Of TKey, TValue)"/>.</summary>
    <Extension()>
    Public Function GetValueOrNull(Of TKey, TValue As Structure)(dictionary As IReadOnlyDictionary(Of TKey, TValue), key As TKey) As TValue?
        Dim value As TValue = Nothing
        Return If(Not dictionary.TryGetValue(key, value), Nothing, value)
    End Function

    <Extension()>
    Public Function EmptyToNull(input As String) As String
        Return If(String.IsNullOrWhiteSpace(input), Nothing, input)
    End Function

#If NET7_0_OR_GREATER Then
#Else
    ' see https://github.com/dotnet/runtime/issues/62505#issuecomment-1044625848
    <AttributeUsage(AttributeTargets.Property Or AttributeTargets.Field Or AttributeTargets.Parameter, AllowMultiple:=False, Inherited:=False)>
    Friend NotInheritable Class StringSyntaxAttribute : Inherits Attribute
        Public Const CompositeFormat As String = "CompositeFormat"
        Public Const DateOnlyFormat As String = "DateOnlyFormat"
        Public Const DateTimeFormat As String = "DateTimeFormat"
        Public Const EnumFormat As String = "EnumFormat"
        Public Const GuidFormat As String = "GuidFormat"
        Public Const Json As String = "Json"
        Public Const NumericFormat As String = "NumericFormat"
        Public Const Regex As String = "Regex"
        Public Const TimeOnlyFormat As String = "TimeOnlyFormat"
        Public Const TimeSpanFormat As String = "TimeSpanFormat"
        Public Const Uri As String = "Uri"
        Public Const Xml As String = "Xml"
        Public Sub New(syntax As String)
            _syntax = syntax
            _arguments = New Object() {}
        End Sub
        Public Sub New(syntax As String, ParamArray arguments As Object())
            _syntax = syntax
            _arguments = arguments
        End Sub
        Private ReadOnly _syntax As String
        Private ReadOnly _arguments As Object()
        Public ReadOnly Property Syntax As String
            Get
                Return _syntax : End Get : End Property
        Public ReadOnly Property Arguments As Object()
            Get
                Return _arguments : End Get : End Property
    End Class
#End If

    <Extension()>
    Public Sub SetDoubleBuffered(control As Windows.Forms.Control, enable As Boolean) ' thanks to https://stackoverflow.com/a/15268338/2999220
        Dim doubleBufferPropertyInfo As PropertyInfo = control.GetType().GetProperty("DoubleBuffered", BindingFlags.Instance Or BindingFlags.NonPublic)
        doubleBufferPropertyInfo.SetValue(control, enable)
    End Sub
End Module
