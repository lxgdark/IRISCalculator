Imports System.Collections.ObjectModel
Imports Microsoft.VisualBasic
Imports WPFProjectCore

Namespace DataClasses
    Public Class OneCatalogPositionOrderItem
        Inherits BaseOrderItem
#Region "Свойства"
#Region "Внутренние"
        Private itemHeaderValue As String = "Отдельная составная часть"
        Private isValidCostPriceValue As Boolean = False
        Private catalogItemValue As New CatalogItem
        Private countInProductValue As Double = 1
#End Region
        ''' <summary>
        ''' Заголовок состовной части
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

        Public Property IsValidCostPrice As Boolean
            Get
                Return isValidCostPriceValue
            End Get
            Set(value As Boolean)
                isValidCostPriceValue = value
                OnPropertyChanged(NameOf(IsValidCostPrice))
            End Set
        End Property
#End Region
#Region "Процедуры и функции"
        Public Overrides Sub Calculation()
            IsValidCostPrice = CatalogItem.Name <> ""
            ProductCostPrice = CatalogItem.CostPrice * CountInProduct
        End Sub
#End Region
    End Class
End Namespace