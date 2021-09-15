Imports System.ComponentModel
Imports System.Globalization
Imports IRISCalculator.Workers
''' <summary>
''' Конвертирует путь к фоновому изображению в BitmapImage
''' </summary>
Public Class StringToImageConverter
    Inherits ConvertorBase(Of StringToImageConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If (value = "" Or Not IO.File.Exists(value)) And value <> ThemeWorker.BaseImageRelativeString Then
            Return Nothing
        Else
            Return New BitmapImage(New Uri(value, UriKind.Absolute))
        End If
    End Function
End Class
''' <summary>
''' Конвертирует режим отображения фона в переключатели режима "Замостить"
''' </summary>
Public Class BackgroundImageVisualModeToTileConverter
    Inherits ConvertorBase(Of BackgroundImageVisualModeToTileConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If CType(value, ThemeWorker.BackgroundImageVisualModeEnum) = ThemeWorker.BackgroundImageVisualModeEnum.Tile Then
            Return TileMode.Tile
        Else
            Return TileMode.None
        End If
    End Function
End Class
''' <summary>
''' Конвертирует режим отображения фона в размер замощения
''' </summary>
Public Class BackgroundImageVisualModeToViewportConverter
    Inherits ConvertorBase(Of BackgroundImageVisualModeToViewportConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If CType(value, ThemeWorker.BackgroundImageVisualModeEnum) = ThemeWorker.BackgroundImageVisualModeEnum.Tile Then
            Return New Rect(New Point(0, 0), New Size(0.2, 0.2))
        Else
            Return New Rect(New Point(0, 0), New Size(1, 1))
        End If
    End Function
End Class
''' <summary>
''' Конвертирует режим отображения фона в размер замощения для мини режима
''' </summary>
Public Class BackgroundImageVisualModeToMiniViewportConverter
    Inherits ConvertorBase(Of BackgroundImageVisualModeToMiniViewportConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If CType(value, ThemeWorker.BackgroundImageVisualModeEnum) = ThemeWorker.BackgroundImageVisualModeEnum.Tile Then
            Return New Rect(New Point(0, 0), New Size(0.1, 0.5))
        Else
            Return New Rect(New Point(0, 0), New Size(1, 1))
        End If
    End Function
End Class
''' <summary>
''' Конвертирует режим отображения фона в режим растягивания изображения
''' </summary>
Public Class BackgroundImageVisualModeToStretchConverter
    Inherits ConvertorBase(Of BackgroundImageVisualModeToStretchConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        If DesignerProperties.GetIsInDesignMode(New DependencyObject) Then Return Nothing
        If CType(value, ThemeWorker.BackgroundImageVisualModeEnum) = ThemeWorker.BackgroundImageVisualModeEnum.Tile Then
            Return Stretch.Uniform
        Else
            Return Stretch.UniformToFill
        End If
    End Function
End Class
''' <summary>
''' Конвертирует цвет в кисть
''' </summary>
Public Class ColorToBrushConverter
    Inherits ConvertorBase(Of ColorToBrushConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Return New SolidColorBrush(value)
    End Function
End Class