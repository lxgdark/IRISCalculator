Imports Microsoft.VisualBasic
Imports WPFProjectCore

Namespace DataClasses
    Public Class StandartOrderItem
        Inherits NotifyProperty_Base(Of StandartOrderItem)
#Region "Свойства"
#Region "Внутренние"
        Private printPaperSizeValue As New PaperSizeItem
        Private productSizeValue As New PaperSizeItem With {.Name = "А4", .Height = 210, .Width = 297, .FieldHeight = 2, .FieldWidth = 2}
        Private productCountValue As Integer = 0
        Private isProductLargePaperValue As Boolean = True
        Private horizontalProductCountValue As Integer = 0
        Private verticalProductCountValue As Integer = 0
        Private isProductOrientationEqualsPaperOrientationValue As Boolean = False
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
        ''' <summary>
        ''' Количество изделий по горизонтали
        ''' </summary>
        ''' <returns></returns>
        Public Property HorizontalProductCount As Integer
            Get
                Return horizontalProductCountValue
            End Get
            Set(value As Integer)
                horizontalProductCountValue = value
                OnPropertyChanged(NameOf(HorizontalProductCount))
            End Set
        End Property
        ''' <summary>
        ''' Количество изделий по вертикали
        ''' </summary>
        ''' <returns></returns>
        Public Property VerticalProductCount As Integer
            Get
                Return verticalProductCountValue
            End Get
            Set(value As Integer)
                verticalProductCountValue = value
                OnPropertyChanged(NameOf(VerticalProductCount))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то, совпадает ли ориентация продукции и бумаги
        ''' </summary>
        ''' <returns></returns>
        Public Property IsProductOrientationEqualsPaperOrientation As Boolean
            Get
                Return isProductOrientationEqualsPaperOrientationValue
            End Get
            Set(value As Boolean)
                isProductOrientationEqualsPaperOrientationValue = value
                OnPropertyChanged(NameOf(IsProductOrientationEqualsPaperOrientation))
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

            IsProductLargePaper = (productHeight <= paperHeight And productWidth <= paperWidth) Or (productHeight <= paperWidth And productWidth <= paperHeight)
            If IsProductLargePaper Then
                Dim horizontalCount1 As Integer = Math.Truncate(paperHeight / productHeight)
                Dim verticalCount1 As Integer = Math.Truncate(paperWidth / productWidth)
                Dim horizontalCount2 As Integer = Math.Truncate(paperWidth / productHeight)
                Dim verticalCount2 As Integer = Math.Truncate(paperHeight / productWidth)
                If horizontalCount1 * verticalCount1 > horizontalCount2 * verticalCount2 Then
                    ProductCount = horizontalCount1 * verticalCount1
                    HorizontalProductCount = horizontalCount1
                    VerticalProductCount = verticalCount1
                    IsProductOrientationEqualsPaperOrientation = True
                Else
                    ProductCount = horizontalCount2 * verticalCount2
                    HorizontalProductCount = horizontalCount2
                    VerticalProductCount = verticalCount2
                    IsProductOrientationEqualsPaperOrientation = False
                End If
            Else
                    ProductCount = 0
            End If
        End Sub
#End Region
    End Class
End Namespace