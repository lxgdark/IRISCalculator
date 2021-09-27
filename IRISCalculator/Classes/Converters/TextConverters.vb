Imports System.Globalization
Imports IRISCalculator.Workers
''' <summary>
''' Конвертирует английское название текущей страницы в русское
''' </summary>
Public Class PageEngTitleToRusConverter
    Inherits ConvertorBase(Of PageEngTitleToRusConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Select Case CType(value, AppCore.CurentSelectedPageEnum)
            Case AppCore.CurentSelectedPageEnum.Home
                Return "Главная"
            Case AppCore.CurentSelectedPageEnum.Settings
                Return "Настройки приложения"
            Case AppCore.CurentSelectedPageEnum.Theme
                Return "Настройки цвета"
            Case Else
                Return "Неизвестная страница"
        End Select
    End Function
End Class
''' <summary>
''' Конвертирует код раздела каталога в понятное наименование
''' </summary>
Public Class CataloGrouCodeToNameConverter
    Inherits ConvertorBase(Of CataloGrouCodeToNameConverter)
    Public Overrides Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object
        Return CatalogGroupNameWorker.GetItemName(value)
    End Function
End Class