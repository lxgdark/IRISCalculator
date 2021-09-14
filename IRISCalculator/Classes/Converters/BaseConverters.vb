Imports System
Imports System.ComponentModel
Imports System.Globalization
Imports Microsoft.VisualBasic
#Region "База"
''' <summary>
''' Базовый конвертер для MarkupExtension
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class ConvertorBase(Of T As {Class, New})
    Inherits Markup.MarkupExtension
    Implements IValueConverter
    Public MustOverride Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
    Public Overridable Function ConvertBack(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
#Region "MarkupExtension members"
    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        If _converter Is Nothing Then
            _converter = New T()
        End If
        Return _converter
    End Function
    Private Shared _converter As T = Nothing
#End Region
End Class
''' <summary>
''' Базовый мультиконвертер для MarkupExtension
''' </summary>
''' <typeparam name="T"></typeparam>
Public MustInherit Class MultiConvertorBase(Of T As {Class, New})
    Inherits Markup.MarkupExtension
    Implements IMultiValueConverter

    Public MustOverride Function Convert(values() As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IMultiValueConverter.Convert

    Public Overridable Function ConvertBack(value As Object, targetTypes() As Type, parameter As Object, culture As CultureInfo) As Object() Implements IMultiValueConverter.ConvertBack
        Throw New NotImplementedException()
    End Function
#Region "MarkupExtension members"
    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        If _converter Is Nothing Then
            _converter = New T()
        End If
        Return _converter
    End Function
    Private Shared _converter As T = Nothing
#End Region
End Class
#End Region
#Region "Универсальные конвертеры"
''' <summary>
''' Конвертирует Boolean в Visibility
''' </summary>
''' <remarks></remarks>
Public Class BooleanToVisibleConverter
    Inherits ConvertorBase(Of BooleanToVisibleConverter)
    Public Sub New()
    End Sub
    Public Overrides Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object
        If parameter <> "Not" Then
            If CBool(value) Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        Else
            If CBool(value) Then
                Return Visibility.Collapsed

            Else
                Return Visibility.Visible
            End If
        End If
    End Function
End Class
''' <summary>
''' Конвертирует логичекое значение в обратное ему
''' </summary>
Public Class BooleanNotConverter
    Inherits ConvertorBase(Of BooleanNotConverter)
    Public Sub New()
    End Sub
    Public Overrides Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object
        Return Not value
    End Function
End Class
''' <summary>
''' Возвращает отступ высчитаный на основе шаблона с добалением входного значения
''' </summary>
Public Class ThicknessConverter
    Inherits ConvertorBase(Of ThicknessConverter)
    Public Sub New()
    End Sub
    Public Overrides Function Convert(value As Object, targetType As System.Type, parameter As Object, culture As System.Globalization.CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        Dim result As New Thickness
        Dim inputThickness As String() = parameter.ToString.Split({","}, StringSplitOptions.RemoveEmptyEntries)
        result.Left = IIf(inputThickness(0).StartsWith("*"), CInt(inputThickness(0).Replace("*", "")) + value, inputThickness(0))
        result.Top = IIf(inputThickness(1).StartsWith("*"), CInt(inputThickness(1).Replace("*", "")) + value, inputThickness(1))
        result.Right = IIf(inputThickness(2).StartsWith("*"), CInt(inputThickness(2).Replace("*", "")) + value, inputThickness(2))
        result.Bottom = IIf(inputThickness(3).StartsWith("*"), CInt(inputThickness(3).Replace("*", "")) + value, inputThickness(3))
        Return result
    End Function
End Class
#End Region
#Region "Конвертеры перечеслителей и чисел"
''' <summary>
''' Конвертирует значение перечеслителя в значение видимости
''' </summary>
Public Class EnumToVisibleConverter
    Inherits ConvertorBase(Of EnumToVisibleConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If CStr(parameter).IndexOf(CStr(value)) > -1 Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function
End Class
''' <summary>
''' Конвертирует значение перечеслителя в логическое значение
''' </summary>
Public Class EnumToBooleanConverter
    Inherits ConvertorBase(Of EnumToBooleanConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Return CStr(parameter).IndexOf(CStr(value)) > -1
    End Function
    Public Overrides Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Return parameter
    End Function
End Class
''' <summary>
''' Конвертирует кличество в видимость
''' </summary>
Public Class CountToVisibleConverter
    Inherits ConvertorBase(Of CountToVisibleConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Dim diapazon As String() = parameter.ToString.Split({"$"}, StringSplitOptions.RemoveEmptyEntries)
        If value >= diapazon(0) And value <= diapazon(1) Then
            Return Visibility.Visible
        Else
            Return Visibility.Hidden
        End If
    End Function
End Class
''' <summary>
''' Конвертирует нулевое количество в видимость
''' </summary>
Public Class NullCountToVisibleConverter
    Inherits ConvertorBase(Of NullCountToVisibleConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If value Is Nothing Then Return Visibility.Collapsed
        If parameter = "Not" Then
            If value = 0 Then
                Return Visibility.Visible
            Else
                Return Visibility.Collapsed
            End If
        Else
            If value = 0 Then
                Return Visibility.Collapsed
            Else
                Return Visibility.Visible
            End If
        End If
    End Function
End Class
#End Region