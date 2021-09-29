Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    Public Class StandartOrderItem
        Inherits NotifyProperty_Base(Of StandartOrderItem)
#Region "Свойства"
#Region "Внутренние"
        Private printPaperSizeValue As New PaperSizeItem
        Private productSizeValue As New PaperSizeItem With {.Name = "А4", .Height = 210, .Width = 297, .FieldHeight = 3, .FieldWidth = 3}
#End Region
        ''' <summary>
        ''' Размер печатного листа
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintPaperSize As PaperSizeItem
            Get
                Return PrintPaperSizeValue
            End Get
            Set(value As PaperSizeItem)
                PrintPaperSizeValue = value
                OnPropertyChanged(NameOf(PrintPaperSize))
            End Set
        End Property
        ''' <summary>
        ''' Размер готовго изделия
        ''' </summary>
        ''' <returns></returns>
        Public Property ProductSize As PaperSizeItem
            Get
                Return productSizeValue
            End Get
            Set(value As PaperSizeItem)
                productSizeValue = value
                OnPropertyChanged(NameOf(ProductSize))
            End Set
        End Property
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"

#End Region

#End Region
    End Class
End Namespace