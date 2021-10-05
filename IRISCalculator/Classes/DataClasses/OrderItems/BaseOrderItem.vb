Imports Microsoft.VisualBasic
Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Базовый интерфейс для составной части заказа
    ''' </summary>
    Public Interface IBaseOrderItem
        ''' <summary>
        ''' Процедура инициирующая расчет составной части
        ''' </summary>
        Sub Calculation()
        ''' <summary>
        ''' Возвращает количество изделий на лесте
        ''' </summary>
        ''' <returns></returns>
        Function GetProductCount() As Integer
        ''' <summary>
        ''' Возвращает себестоимость составной части (за изделие)
        ''' </summary>
        ''' <returns></returns>
        Function GetProductCostPrice() As Double
    End Interface
    ''' <summary>
    ''' Базовый класс составной части расчета
    ''' </summary>
    Public MustInherit Class BaseOrderItem
        Inherits NotifyProperty_Base(Of BaseOrderItem)
        Implements IBaseOrderItem
#Region "Свойства"
#Region "Внутренние"
        Private productCountValue As Integer = 0
        Private productCostPriceValue As Double = 0
#End Region
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
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"
        ''' <summary>
        ''' Извлекает размер листа из единицы измерения бумаги
        ''' </summary>
        ''' <param name="unit"></param>
        ''' <returns></returns>
        Protected Function GetSheetSize(unit As String) As Size
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
#End Region
#Region "Реализация интерфейса"
        Public MustOverride Sub Calculation() Implements IBaseOrderItem.Calculation
        Public Function GetProductCount() As Integer Implements IBaseOrderItem.GetProductCount
            Return ProductCount
        End Function
        Public Function GetProductCostPrice() As Double Implements IBaseOrderItem.GetProductCostPrice
            Return ProductCostPrice
        End Function
#End Region
#End Region
    End Class
End Namespace