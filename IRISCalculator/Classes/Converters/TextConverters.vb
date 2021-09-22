Imports System.Globalization
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
        Dim str As String() = value.ToString.Split("$", StringSplitOptions.RemoveEmptyEntries)
        If str.Length = 3 Then
            Dim result As String = ""
            Select Case str(1)
                Case "PAPER"
                    result &= "Бумага "
                Case "SERVICE"
                    result &= "Услуга "
            End Select

            Select Case str(2)
                Case "DIZ"
                    result &= "дизайнерская"
                Case "MAT"
                    result &= "матовая"
                Case "OFIS"
                    result &= "офисная (офсетная)"
                Case "SAMOKL"
                    result &= "самоклейка"
                Case "SINT"
                    result &= "синтетика"
                    '\\\\
                Case "PRINT"
                    result &= "печать"
            End Select
            Return result
        Else
            Return value
        End If
    End Function
End Class