Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    Public Class StandartOrderItem
        Inherits NotifyProperty_Base(Of StandartOrderItem)
#Region "Свойства"
#Region "Внутренние"
        Private PrintPaperSizeValue As New Size(320, 450)
        Private PrintedFieldValue As New Thickness(5)

#End Region
        ''' <summary>
        ''' Размер печатного листа
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintPaperSize As Size
            Get
                Return PrintPaperSizeValue
            End Get
            Set(value As Size)
                PrintPaperSizeValue = value
                OnPropertyChanged(NameOf(PrintPaperSize))
            End Set
        End Property
        ''' <summary>
        ''' Отступы от края листа при печати
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintedField As Thickness
            Get
                Return PrintedFieldValue
            End Get
            Set(value As Thickness)
                PrintedFieldValue = value
                OnPropertyChanged(NameOf(PrintedField))
            End Set
        End Property
#End Region
    End Class
End Namespace