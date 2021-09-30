Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    Public Class StandartOrderItem
        Inherits NotifyProperty_Base(Of StandartOrderItem)
#Region "Свойства"
#Region "Внутренние"
        Private printPaperSizeValue As New PaperSizeItem
        Private productSizeValue As New PaperSizeItem With {.Name = "А4", .Height = 210, .Width = 297, .FieldHeight = 2, .FieldWidth = 2}
        Private productCountValue As Integer = 0
        Private isProductLargePaperValue As Boolean = False
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
        ''' <summary>
        ''' Количестве изделий на листе
        ''' </summary>
        ''' <returns></returns>
        Public Property ProductCount As Integer
            Get
                Return productCountValue
            End Get
            Set(value As Integer)
                productCountValue = value
                OnPropertyChanged(NameOf(ProductCount))
            End Set
        End Property
        ''' <summary>
        ''' Флаг указывающий на ошибку, когджа изделие больше печатной области
        ''' </summary>
        ''' <returns></returns>
        Public Property IsProductLargePaper As Boolean
            Get
                Return isProductLargePaperValue
            End Get
            Set(value As Boolean)
                isProductLargePaperValue = value
                OnPropertyChanged(NameOf(IsProductLargePaper))
            End Set
        End Property
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"

#End Region
        Public Sub CalculationProductCount()
            Dim paperHeight As Double = PrintPaperSize.Height - PrintPaperSize.FieldHeight * 2
            Dim paperWidth As Double = PrintPaperSize.Width - PrintPaperSize.FieldWidth * 2

            Dim productHeight As Double = ProductSize.Height + ProductSize.FieldHeight * 2
            Dim productWidth As Double = ProductSize.Width + ProductSize.FieldWidth * 2

            'IsProductLargePaper = Not ((productHeight <= paperHeight Or productHeight <= paperWidth) And (productWidth <= paperHeight Or productWidth <= paperWidth))
        End Sub
#End Region
    End Class
End Namespace