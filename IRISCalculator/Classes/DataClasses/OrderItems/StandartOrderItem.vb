Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic
Imports WPFProjectCore

Namespace DataClasses
    Public Class StandartOrderItem
        Inherits BaseOrderItem
#Region "Свойства"
#Region "Внутренние"
        Private printPaperSizeValue As New PaperSizeItem
        Private productSizeValue As New PaperSizeItem With {.Name = "А4", .Height = 210, .Width = 297, .FieldHeight = 2, .FieldWidth = 2}
        Private isProductLargePaperValue As Boolean = True
        Private isValidCostPriceValue As Boolean = False
        Private isProductOrientationEqualsPaperOrientationValue As Boolean = False
        Private paperItemValue As New CatalogItem
        Private printItemValue As New CatalogItem
        Private cutItemValue As New CatalogItem
        Private pageCountValue As Integer = 1
        Private pageMinimumCountValue As Integer = 1
        Private isProductCatalogValue As Boolean = False
        Private isShortFoldSideValue As Boolean = False
        Private isCatalogPageCountErrorValue As Boolean = False
        Private itemHeaderValue As String = "Стандартная составная часть"
#End Region
        ''' <summary>
        ''' Размер печатного листа
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintPaperSize As PaperSizeItem
            Get
                Return printPaperSizeValue
            End Get
            Set(value As PaperSizeItem)
                printPaperSizeValue = value
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
        ''' Количество страниц (полос)
        ''' </summary>
        ''' <returns></returns>
        Public Property PageCount As Integer
            Get
                Return pageCountValue
            End Get
            Set(value As Integer)
                pageCountValue = value
                OnPropertyChanged(NameOf(PageCount))
            End Set
        End Property
        ''' <summary>
        ''' Минимальное число полос
        ''' </summary>
        ''' <returns></returns>
        Public Property PageMinimumCount As Integer
            Get
                Return pageMinimumCountValue
            End Get
            Set(value As Integer)
                pageMinimumCountValue = value
                OnPropertyChanged(NameOf(PageMinimumCount))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то, является ли изделие каталогом/брошюрой
        ''' </summary>
        ''' <returns></returns>
        Public Property IsProductCatalog As Boolean
            Get
                Return isProductCatalogValue
            End Get
            Set(value As Boolean)
                isProductCatalogValue = value
                OnPropertyChanged(NameOf(IsProductCatalog))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то сгибается ли каталог по короткой стороне
        ''' </summary>
        ''' <returns></returns>
        Public Property IsShortFoldSide As Boolean
            Get
                Return isShortFoldSideValue
            End Get
            Set(value As Boolean)
                isShortFoldSideValue = value
                OnPropertyChanged(NameOf(IsShortFoldSide))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то, что число полос не подходит для печати брашюрой
        ''' </summary>
        ''' <returns></returns>
        Public Property IsCatalogPageCountError As Boolean
            Get
                Return isCatalogPageCountErrorValue
            End Get
            Set(value As Boolean)
                isCatalogPageCountErrorValue = value
                OnPropertyChanged(NameOf(IsCatalogPageCountError))
            End Set
        End Property
        ''' <summary>
        ''' Список прочих действий с сотавной частью
        ''' </summary>
        ''' <returns></returns>
        Public Property OtherOrderActionList As New ObservableCollection(Of OtherStandartOrderActionItem)
        ''' <summary>
        ''' Загаловок составной части
        ''' </summary>
        ''' <returns></returns>
        Public Property ItemHeader As String
            Get
                Return itemHeaderValue
            End Get
            Set(value As String)
                itemHeaderValue = value
                OnPropertyChanged(NameOf(ItemHeader))
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
            'Получаем информацию является ли изделие каталогом и по какой стороне сгиб
            Dim isCatalogFoldHeight As Boolean = IsProductCatalog And Not ((ProductSize.Height <= ProductSize.Width And IsShortFoldSide) OrElse (ProductSize.Height >= ProductSize.Width And Not IsShortFoldSide))
            Dim isCatalogFoldWidth As Boolean = IsProductCatalog And Not ((ProductSize.Width < ProductSize.Height And IsShortFoldSide) OrElse (ProductSize.Width > ProductSize.Height And Not IsShortFoldSide))
            'Получаем размер изделия (плюс вылеты) и плюс раскладка на случай печати брошюрой
            Dim productHeight As Double = ProductSize.Height * IIf(isCatalogFoldHeight, 2, 1) + ProductSize.FieldHeight * 2
            Dim productWidth As Double = ProductSize.Width * IIf(isCatalogFoldWidth, 2, 1) + ProductSize.FieldWidth * 2
            'Если изделие больше листа, задаем флаг ошибки
            IsProductLargePaper = (productHeight <= paperHeight And productWidth <= paperWidth) Or (productHeight <= paperWidth And productWidth <= paperHeight)
            'Если ошибки размера нет, идем дальше
            If IsProductLargePaper Then
                ProductCount = GetProductInPaper(New Size(paperWidth, paperHeight), New Size(productWidth, productHeight), True)
            Else
                'Если размер задан с ошибкой, ставим число изделий на листе в 0
                ProductCount = 0
            End If
        End Sub
        ''' <summary>
        ''' Расчет себестоимости продукта
        ''' </summary>
        Private Sub CalculationCostPrice()
            'Сбрасываем себестоимость
            ProductCostPrice = 0
            SetMinimumPage()
            'Проверяем заданы ли все обязательные параметры для расчета себестоимости
            IsValidCostPrice = PaperItem.Name <> "" And PrintItem.Name <> "" And CutItem.Name <> ""
            'Если печать в режиме каталога
            If IsProductCatalog Then
                'Проверяем, что число полос кратно 4
                IsCatalogPageCountError = Math.Truncate(PageCount / 4) <> PageCount / 4
            Else
                IsCatalogPageCountError = False
            End If
            'Корректируем флаг валидности последующих расчетов
            IsValidCostPrice = IsValidCostPrice And Not IsCatalogPageCountError
            'Если все задлано, продолжаем расчет
            If IsValidCostPrice Then
                Dim sheetSize = GetSheetSize(PaperItem.Unit)
                Dim paperHeight As Double = PrintPaperSize.Height
                Dim paperWidth As Double = PrintPaperSize.Width
                'Высчитываем себестоимость печатного листа исходя из того, что ряд листов может поставляться больше, чем SRA3
                Dim paperCostPrice = PaperItem.CostPrice / GetProductInPaper(sheetSize, New Size(paperWidth, paperHeight))
                'Высчитываем себестоимость составной части
                '
                'Себестоимость бумаги высчитали ранее
                'Себестоимость печати задана в прайсе
                'Себестоимость резки за изделие, поэтому умножаем на число изделий на листе
                'Умножаем результат на число изделий и делим на 2, если печать двусторонняя
                'Если печать каталогом делим еще на 2
                '
                ProductCostPrice = (paperCostPrice + PrintItem.CostPrice + CutItem.CostPrice * ProductCount) / ProductCount * PageCount / IIf(PrintItem.Name.EndsWith("4") Or PrintItem.Name.EndsWith("1"), 2, 1) / IIf(IsProductCatalog, 2, 1)
                'Проходим по списку дополнительных обработок и добавляем к себестоимости позиции их себестоимость
                For Each ooal In OtherOrderActionList
                    ProductCostPrice += ooal.GetCostPrice
                Next
            Else
                'Если не все задано, то возвращаем себестоимость в 0
                ProductCostPrice = 0
            End If
        End Sub
        ''' <summary>
        ''' Возвращает количество одного размера, входящего в другой (например количество изделий на листе)
        ''' </summary>
        ''' <returns></returns>
        Private Function GetProductInPaper(size1 As Size, size2 As Size, Optional isSetOrientationEquals As Boolean = False) As Integer
            'Количиство изделий по горизонтали в первой ориентации
            Dim horizontalCount1 As Integer = Math.Truncate(size1.Height / size2.Height)
            'Количиство изделий по вертикали в первой ориентации
            Dim verticalCount1 As Integer = Math.Truncate(size1.Width / size2.Width)
            'Количиство изделий по горизонтали во второй ориентации
            Dim horizontalCount2 As Integer = Math.Truncate(size1.Width / size2.Height)
            'Количиство изделий по вертикали во второй ориентации
            Dim verticalCount2 As Integer = Math.Truncate(size1.Height / size2.Width)
            If horizontalCount1 * verticalCount1 > horizontalCount2 * verticalCount2 Then
                If isSetOrientationEquals Then
                    IsProductOrientationEqualsPaperOrientation = True
                End If
                Return horizontalCount1 * verticalCount1
            Else
                If isSetOrientationEquals Then
                    IsProductOrientationEqualsPaperOrientation = False
                End If
                Return horizontalCount2 * verticalCount2
            End If
        End Function
        ''' <summary>
        ''' Задает минимальное количество полос для указанного типа печати
        ''' </summary>
        Private Sub SetMinimumPage()
            'Если тип печати двусторонний...
            If (PrintItem.Name.EndsWith("4") Or PrintItem.Name.EndsWith("1")) And PageCount < 2 Then
                '...Задаем минимум полос в 2
                PageCount = 2
                PageMinimumCount = 2
            Else
                PageMinimumCount = 1
            End If
        End Sub
#End Region
        ''' <summary>
        ''' Запускаем вычисления себестоимости и количества продукции на листе
        ''' </summary>
        Public Overrides Sub Calculation()
            CalculationProductCount()
            If ProductCount > 0 Then CalculationCostPrice()
        End Sub
        ''' <summary>
        ''' Переопределяет процедуру копирования свойств класса
        ''' </summary>
        ''' <param name="input"></param>
        ''' <param name="ignoreBaseType"></param>
        Public Overrides Sub SetPropertys(input As BaseOrderItem, Optional ignoreBaseType As String = "")
            'Вызываем базовое копирование свойст игнарируя те, что являются коллекциями
            MyBase.SetPropertys(input, "Collection")
            'Далее вручную переносим значения коллекции доп. действий в новый класс
            For Each oal In CType(input, StandartOrderItem).OtherOrderActionList
                Dim osoa As New OtherStandartOrderActionItem
                osoa.SetPropertys(oal)
                Me.OtherOrderActionList.Add(osoa)
            Next
        End Sub
#End Region
    End Class
    ''' <summary>
    ''' Описывает позицию списка дополнительных обработок
    ''' </summary>
    Public Class OtherStandartOrderActionItem
        Inherits NotifyProperty_Base(Of OtherStandartOrderActionItem)
#Region "Свойства"
#Region "Внутренние"
        Private catalogItemValue As New CatalogItem
        Private countInProductValue As Double = 1
#End Region
        ''' <summary>
        '''Позиция каталога отвечающая за  доп. обработку 
        ''' </summary>
        ''' <returns></returns>
        Public Property CatalogItem As CatalogItem
            Get
                Return catalogItemValue
            End Get
            Set(value As CatalogItem)
                catalogItemValue = value
                OnPropertyChanged(NameOf(CatalogItem))
            End Set
        End Property
        ''' <summary>
        ''' Значение количества указанной позиции
        ''' </summary>
        ''' <returns></returns>
        Public Property CountInProduct As Double
            Get
                Return countInProductValue
            End Get
            Set(value As Double)
                countInProductValue = value
                OnPropertyChanged(NameOf(CountInProduct))
            End Set
        End Property
#End Region
        ''' <summary>
        ''' Возврачает себестоимость позиции
        ''' </summary>
        ''' <returns></returns>
        Public Function GetCostPrice() As Double
            Return CatalogItem.CostPrice * CountInProduct
        End Function
    End Class
End Namespace