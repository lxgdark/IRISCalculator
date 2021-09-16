Imports System.ComponentModel
Imports System.Globalization
''' <summary>
''' Ковертирует стиль сообщения в его позицию
''' </summary>
Public Class MessageBoxTopMostToVerticalPositionConverter
    Inherits ConvertorBase(Of MessageBoxTopMostToVerticalPositionConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If value Then
            Return VerticalAlignment.Center
        Else
            Return VerticalAlignment.Top
        End If
    End Function
End Class
''' <summary>
''' Конвертирует стиль сообщения в цвет изображения
''' </summary>
Public Class MessageBoxStyleToImageColorConverter
    Inherits ConvertorBase(Of MessageBoxStyleToImageColorConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        Dim result As New SolidColorBrush
        Select Case value
            Case WPFProjectCore.MessageStyle.ErrorMessage
                result = Windows.Application.Current.Resources.Item("MessageBoxErrorColor")
            Case WPFProjectCore.MessageStyle.InfoMessage
                result = Windows.Application.Current.Resources.Item("MessageBoxInfoColor")
            Case WPFProjectCore.MessageStyle.YesNo
                result = Windows.Application.Current.Resources.Item("MessageBoxQuestionColor")
        End Select
        Return result
    End Function
End Class
''' <summary>
''' Конвертирует стиль сообщение в картинку
''' </summary>
Public Class MessageBoxStyleToImageConverter
    Inherits ConvertorBase(Of MessageBoxStyleToImageConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        Return New BitmapImage(New Uri("pack://application:,,,/Pict/MessageBox/" & CType(value, WPFProjectCore.MessageStyle).ToString & ".png", UriKind.Absolute))
    End Function
End Class
''' <summary>
''' Определяет идимость второй кнопки сообщения в зависимости от стиля сообщения
''' </summary>
Public Class MessageBoxYesNoToVisibleConverter
    Inherits ConvertorBase(Of MessageBoxYesNoToVisibleConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If CType(value, WPFProjectCore.MessageStyle) = WPFProjectCore.MessageStyle.YesNo Then
            Return Visibility.Visible
        Else
            Return Visibility.Collapsed
        End If
    End Function
End Class
''' <summary>
''' Определяет текст главной кнопкии сообщения в зависимости от стиля сообщения
''' </summary>
Public Class MessageBoxYesNoToButtonTextConverter
    Inherits ConvertorBase(Of MessageBoxYesNoToButtonTextConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If CType(value, WPFProjectCore.MessageStyle) = WPFProjectCore.MessageStyle.YesNo Then
            Return "Да"
        Else
            Return "OK"
        End If
    End Function
End Class
''' <summary>
''' Выравнивает главную кнопку сообщения слева или по центру в зависимости от типа сообщения
''' </summary>
Public Class MessageBoxYesNoToButtonAlignmentConverter
    Inherits ConvertorBase(Of MessageBoxYesNoToButtonAlignmentConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If CType(value, WPFProjectCore.MessageStyle) = WPFProjectCore.MessageStyle.YesNo Then
            Return 1
        Else
            Return 2
        End If
    End Function
End Class