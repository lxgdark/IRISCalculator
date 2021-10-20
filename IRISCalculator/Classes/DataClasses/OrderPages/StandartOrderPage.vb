Imports System.Collections.ObjectModel
Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Класс описывающий заказ
    ''' </summary>
    Public Class StandartOrderPage
        Inherits OrderPage
        Implements INotifyPropertyChanged
#Region "Реализация интерфейса INotifyPropertyChanged"
        Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged
        Public Shadows Sub OnPropertyChanged(PropertyName As String)
            RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(PropertyName))
        End Sub
#End Region
#Region "Свойства"
#Region "Внутренние"
        Private leftPanelWidthValue As New GridLength(50, GridUnitType.Star)
        Private rightPanelWidthValue As New GridLength(50, GridUnitType.Star)
        Private minPrintCopyValue As Integer = 0
        Private minCostPriceValue As Double = 0
        Private minPriceValue As Double = 0
        Private productCostPriceValue As Double = 0
#End Region
        Public Property OrderItemList As New ObservableCollection(Of IBaseOrderItem)
        Public Property PrintCopyCountList As New ObservableCollection(Of PrintCopyCountItem)
        ''' <summary>
        ''' Размер левой панели страницы расчета
        ''' </summary>
        ''' <returns></returns>
        Public Property LeftPanelWidth As GridLength
            Get
                Return leftPanelWidthValue
            End Get
            Set(value As GridLength)
                leftPanelWidthValue = value
                OnPropertyChanged(NameOf(LeftPanelWidth))
            End Set
        End Property
        ''' <summary>
        ''' Размер правой панели страницы расчета
        ''' </summary>
        ''' <returns></returns>
        Public Property RightPanelWidth As GridLength
            Get
                Return rightPanelWidthValue
            End Get
            Set(value As GridLength)
                rightPanelWidthValue = value
                OnPropertyChanged(NameOf(RightPanelWidth))
            End Set
        End Property
        ''' <summary>
        ''' Минимальный тираж
        ''' </summary>
        ''' <returns></returns>
        Public Property MinPrintCopy As Integer
            Get
                Return minPrintCopyValue
            End Get
            Set(value As Integer)
                minPrintCopyValue = value
                OnPropertyChanged(NameOf(MinPrintCopy))
            End Set
        End Property
        ''' <summary>
        ''' Минимальная себестоимость
        ''' </summary>
        ''' <returns></returns>
        Public Property MinCostPrice As Double
            Get
                Return minCostPriceValue
            End Get
            Set(value As Double)
                minCostPriceValue = value
                OnPropertyChanged(NameOf(MinCostPrice))
            End Set
        End Property
        ''' <summary>
        ''' Минимальная цена продажи (цветопроба)
        ''' </summary>
        ''' <returns></returns>
        Public Property MinPrice As Double
            Get
                Return minPriceValue
            End Get
            Set(value As Double)
                minPriceValue = value
                OnPropertyChanged(NameOf(MinPrice))
            End Set
        End Property
        ''' <summary>
        ''' Себестоимость всей продукции (за одно изделие)
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
        ''' Указывает на то является ли эта страница копией
        ''' </summary>
        ''' <returns></returns>
        Public Property IsCopyPage As Boolean = False
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"
        Public Shared Function GetOrderItemByType(typeName As String) As IBaseOrderItem
            Dim result As IBaseOrderItem
            If typeName = "StandartOrderItem" Then
                result = New StandartOrderItem
            ElseIf typeName = "OneCatalogPositionOrderItem" Then
                result = New OneCatalogPositionOrderItem
            Else
                result = New StandartOrderItem
            End If
            Return result
        End Function
#End Region
        ''' <summary>
        ''' Обнудляет свойства расчета 
        ''' </summary>
        Public Sub ClearPropertys()
            MinPrintCopy = 0
            MinCostPrice = 0
            MinPrice = 0
            ProductCostPrice = 0
        End Sub
        ''' <summary>
        ''' Задает сохраняемые параметры класс (при копировании, сохранении и загрузке)
        ''' </summary>
        ''' <param name="input"></param>
        Public Sub SetPropertys(input As StandartOrderPage)
            Me.OrderItemList.Clear()
            For Each oal In input.OrderItemList
                Dim boi As BaseOrderItem
                Dim T() As String = oal.GetType.ToString.Split(".", StringSplitOptions.RemoveEmptyEntries)
                boi = StandartOrderPage.GetOrderItemByType(T(T.Length - 1))
                boi.SetPropertys(oal)
                Me.OrderItemList.Add(boi)
            Next
        End Sub
#End Region
    End Class

End Namespace