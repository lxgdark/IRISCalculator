﻿Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Класс описывающий страницы главного TabControl
    ''' </summary>
    Public Class GlobalPage
        Inherits NotifyProperty_Base(Of GlobalPage)
#Region "Свойства"
#Region "Внутренние"
        Private HeaderValue As String = ""
        Private IsStartPageValue As Boolean = True
        Private isHeaderEditStateValue As Boolean = False
        Private OrderObjectValue As Page = New Order
#End Region
        ''' <summary>
        ''' Заголовок вкладки
        ''' </summary>
        ''' <returns></returns>
        Public Property Header As String
            Get
                Return HeaderValue
            End Get
            Set(value As String)
                HeaderValue = value
                OnPropertyChanged(NameOf(Header))
            End Set
        End Property
        ''' <summary>
        ''' Объект заказа
        ''' </summary>
        ''' <returns></returns>
        Public Property OrderObject As Page
            Get
                Return OrderObjectValue
            End Get
            Set(value As Page)
                OrderObjectValue = value
                OnPropertyChanged(NameOf(OrderObject))
            End Set
        End Property
        ''' <summary>
        ''' Флаг определяющий является ли текущая страница стартовой
        ''' </summary>
        ''' <returns></returns>
        Public Property IsStartPage As Boolean
            Get
                Return IsStartPageValue
            End Get
            Set(value As Boolean)
                IsStartPageValue = value
                OnPropertyChanged(NameOf(IsStartPage))
            End Set
        End Property
        ''' <summary>
        ''' Указывает на то редактируется ли сейчас заголовок вкладки
        ''' </summary>
        ''' <returns></returns>
        Public Property IsHeaderEditState As Boolean
            Get
                Return isHeaderEditStateValue
            End Get
            Set(value As Boolean)
                isHeaderEditStateValue = value
                OnPropertyChanged(NameOf(IsHeaderEditState))
            End Set
        End Property
#End Region
    End Class
    ''' <summary>
    ''' Класс описывающий заказ
    ''' </summary>
    Public Class Order
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
        Private isShowCatakogDopInfoValue As Boolean = False

#End Region
        ''' <summary>
        ''' Определяет показывать ли дополнительные данные в списке каталога
        ''' </summary>
        ''' <returns></returns>
        Public Property IsShowCatakogDopInfo As Boolean
            Get
                Return isShowCatakogDopInfoValue
            End Get
            Set(value As Boolean)
                isShowCatakogDopInfoValue = value
                OnPropertyChanged(NameOf(IsShowCatakogDopInfo))
            End Set
        End Property
#End Region
    End Class
End Namespace