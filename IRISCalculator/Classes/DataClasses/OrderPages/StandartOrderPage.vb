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
#End Region
        Public Property OrderItemList As New ObservableCollection(Of IBaseOrderItem)
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
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"

#End Region

#End Region
    End Class
End Namespace