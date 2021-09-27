Imports IRISCalculator.DataClasses
Imports WPFProjectCore
Namespace Workers
    ''' <summary>
    ''' Класс для работы со страницами главного TabControl
    ''' </summary>
    Public Class GlobalPageWorker
        Inherits NotifyProperty_Base(Of GlobalPageWorker)
#Region "Свойства"
#Region "Внутренние"
        Private HeaderValue As String = ""
        Private IsStartPageValue As Boolean = True
        Private isHeaderEditStateValue As Boolean = False
        Private OrderObjectValue As Page = New StandartOrder
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
                If value = "" Then
                    HeaderValue = OrderObject.Title
                Else
                    HeaderValue = value
                End If
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
End Namespace