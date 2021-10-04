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
        Private isValidCostPriceValue As Boolean = False
        Private isProductOrientationEqualsPaperOrientationValue As Boolean = False
        Private paperItemValue As New CatalogItem
        Private printItemValue As New CatalogItem
        Private cutItemValue As New CatalogItem
        Private pageCountValue As Integer = 1
        Private productCostPriceValue As Double = 0
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
        ''' <summary>
        ''' Бумага на которой изготавливается изделие
        ''' </summary>
        ''' <returns></returns>
        Public Property PaperItem As CatalogItem
            Get
                Return paperItemValue
            End Get
            Set(value As CatalogItem)
                paperItemValue = value
                OnPropertyChanged(NameOf(PaperItem))
            End Set
        End Property
        ''' <summary>
        ''' Параметры печати
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintItem As CatalogItem
            Get
                Return printItemValue
            End Get
            Set(value As CatalogItem)
                printItemValue = value
                OnPropertyChanged(NameOf(PrintItem))
            End Set
        End Property
        ''' <summary>
        ''' Параметры резки
        ''' </summary>
        ''' <returns></returns>
        Public Property CutItem As CatalogItem
            Get
                Return cutItemValue
            End Get
            Set(value As CatalogItem)
                cutItemValue = value
                OnPropertyChanged(NameOf(CutItem))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то, достаточно ли данны для расчета себестоимости
        ''' </summary>
        ''' <returns></returns>
        Public Property IsValidCostPrice As Boolean
            Get
                Return isValidCostPriceValue
            End Get
            Set(value As Boolean)
                isValidCostPriceValue = value
                OnPropertyChanged(NameOf(IsValidCostPrice))
            End Set
        End Property
        ''' <summary>
        ''' Себестоимость состаной части
        ''' </summary>
        ''' <returns></returns>
        Public Property ProductCostPrice As Double
            Get
                Return productCostPriceValue
            End Get
            Set(value As Double)
                productCostPriceValue = value
                OnPropertyChanged(NameOf(ProductCostPrice))
            End Set
        End Property
        ''' <summary>
        ''' Количество страниц (полос)
        ''' </summary>
        ''' <returns></returns>
        Public Property PageCount As Integer
            Get
                Return pageCountValue
            End Get
            Set(value As Integer)
                pageCountValue = value
            End Set
        End Property
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"
        ''' <summary>
        ''' Вычисляет количество изделий на листе
        ''' </summary>
        Private Sub CalculationProductCount()
            'Получаем размер печтной области (минус поля)
            Dim paperHeight As Double = PrintPaperSize.Height - PrintPaperSize.FieldHeight * 2
            Dim paperWidth As Double = PrintPaperSize.Width - PrintPaperSize.FieldWidth * 2
            'Получаем размер изделия (плюс вылеты)
            Dim productHeight As Double = ProductSize.Height + ProductSize.FieldHeight * 2
            Dim productWidth As Double = ProductSize.Width + ProductSize.FieldWidth * 2
            'Если изделие больше листа, задаем флаг ошибки
            IsProductLargePaper = (productHeight <= paperHeight And productWidth <= paperWidth) Or (productHeight <= paperWidth And productWidth <= paperHeight)
            'Если ошибки размера нет, идем дальше
            If IsProductLargePaper Then
                'Количиство изделий по горизонтали в первой ориентации
                Dim horizontalCount1 As Integer = Math.Truncate(paperHeight / productHeight)
                'Количиство изделий по вертикали в первой ориентации
                Dim verticalCount1 As Integer = Math.Truncate(paperWidth / productWidth)
                'Количиство изделий по горизонтали во второй ориентации
                Dim horizontalCount2 As Integer = Math.Truncate(paperWidth / productHeight)
                'Количиство изделий по вертикали во второй ориентации
                Dim verticalCount2 As Integer = Math.Truncate(paperHeight / productWidth)
                'Выбираем ориентацию при котророй изделий на листе будет больше
                'И задаем значение количиства изделий на листе, а так же устанавливаем флах, сопадает ли ориентация изделия с ориентацией бумаги
                If horizontalCount1 * verticalCount1 > horizontalCount2 * verticalCount2 Then
                    ProductCount = horizontalCount1 * verticalCount1
                    IsProductOrientationEqualsPaperOrientation = True
                Else
                    ProductCount = horizontalCount2 * verticalCount2
                    IsProductOrientationEqualsPaperOrientation = False
                End If
            Else
                'Если размер задан с ошибкой, ставим число изделий на листе в 0
                ProductCount = 0
            End If
        End Sub
        ''' <summary>
        ''' Расчет себестоимости продукта
        ''' </summary>
        Private Sub CalculationCostPrice()
            'Проверяем заданы ли все обязательные параметры для расчета себестоимости
            IsValidCostPrice = PaperItem.Name <> "" And PrintItem.Name <> "" And CutItem.Name <> ""
            'Если все задлано, продолжаем расчет
            If IsValidCostPrice Then
                Dim sheetSize = GetSheetSize(PaperItem.Unit)
                Dim paperHeight As Double = PrintPaperSize.Height
                Dim paperWidth As Double = PrintPaperSize.Width

                Dim paperCostPrice = PaperItem.CostPrice / GetProductInPaper(sheetSize, New Size(paperWidth, paperHeight))
                ProductCostPrice = (paperCostPrice + PrintItem.CostPrice + CutItem.CostPrice) / ProductCount
            Else
                'Если не все задано, то возвращаем себестоимость в 0
                ProductCostPrice = 0
            End If
        End Sub
        ''' <summary>
        ''' Извлекает размер листа из единицы измерения бумаги
        ''' </summary>
        ''' <param name="unit"></param>
        ''' <returns></returns>
        Private Function GetSheetSize(unit As String) As Size
            'Базовый размер листа 450х320
            Dim result As New Size(450, 320)
            'Если единица измерения начинается с L...
            If unit.StartsWith("L") Then
                '...то удаляем L и разбиваем на две части по букве "х"
                Dim s As String() = unit.TrimStart("L".Chars(0)).Split("x", StringSplitOptions.RemoveEmptyEntries)
                'Первая часть ширина
                result.Width = s(0)
                'Вторая часть высота
                result.Height = s(1)
            End If
            Return result
        End Function
        ''' <summary>
        ''' Возвращает количество одного размера, входящего в другой (например количество изделий на листе)
        ''' </summary>
        ''' <returns></returns>
        Private Function GetProductInPaper(size1 As Size, size2 As Size) As Integer
            'Количиство изделий по горизонтали в первой ориентации
            Dim horizontalCount1 As Integer = Math.Truncate(size1.Height / size2.Height)
            'Количиство изделий по вертикали в первой ориентации
            Dim verticalCount1 As Integer = Math.Truncate(size1.Width / size2.Width)
            'Количиство изделий по горизонтали во второй ориентации
            Dim horizontalCount2 As Integer = Math.Truncate(size1.Width / size2.Height)
            'Количиство изделий по вертикали во второй ориентации
            Dim verticalCount2 As Integer = Math.Truncate(size1.Height / size2.Width)
            If horizontalCount1 * verticalCount1 > horizontalCount2 * verticalCount2 Then
                Return horizontalCount1 * verticalCount1
            Else
                Return horizontalCount2 * verticalCount2
            End If
        End Function
#End Region

        Public Sub Calculation()
            CalculationProductCount()
            If ProductCount > 0 Then CalculationCostPrice()
        End Sub

#End Region
    End Class
End Namespace