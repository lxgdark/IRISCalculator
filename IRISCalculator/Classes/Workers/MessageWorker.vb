Imports WPFProjectCore

Namespace Workers
    Public Class MessageWorker
        Inherits NotifyProperty_Base(Of MessageWorker)
        Implements IMessageWorker

#Region "Свойства"
#Region "Внутренние"
        Private MessageStyleValue As MessageStyle = MessageStyle.InfoMessage
        Private TitleTextValue As String = ""
        Private MessageTextValue As String = ""
        Private IsTopMostValue As Boolean = False
        Private IsOpenValue As Boolean = False
#End Region
        ''' <summary>
        ''' Флаг определяющий, показывать ли сообщение поверх всех окон
        ''' </summary>
        ''' <returns></returns>
        Public Property IsTopMost As Boolean
            Get
                Return IsTopMostValue
            End Get
            Set(value As Boolean)
                IsTopMostValue = value
                OnPropertyChanged(NameOf(IsTopMost))
            End Set
        End Property
        ''' <summary>
        ''' Флаг, определяющий открыта ли в данный момент сообщение
        ''' </summary>
        ''' <returns></returns>
        Public Property IsOpen As Boolean
            Get
                Return IsOpenValue
            End Get
            Set(value As Boolean)
                IsOpenValue = value
                OnPropertyChanged(NameOf(IsOpen))
            End Set
        End Property
        ''' <summary>
        ''' Стиль сообщения
        ''' </summary>
        ''' <returns></returns>
        Public Property MessageStyle As MessageStyle
            Get
                Return MessageStyleValue
            End Get
            Set(value As MessageStyle)
                MessageStyleValue = value
                OnPropertyChanged(NameOf(MessageStyle))
            End Set
        End Property
        ''' <summary>
        ''' Заголовок сообщения
        ''' </summary>
        ''' <returns></returns>
        Public Property TitleText As String
            Get
                Return TitleTextValue
            End Get
            Set(value As String)
                TitleTextValue = value
                OnPropertyChanged(NameOf(TitleText))
            End Set
        End Property
        ''' <summary>
        ''' Текст сообщения
        ''' </summary>
        ''' <returns></returns>
        Public Property MessageText As String
            Get
                Return MessageTextValue
            End Get
            Set(value As String)
                MessageTextValue = value
                OnPropertyChanged(NameOf(MessageText))
            End Set
        End Property
        ''' <summary>
        ''' Результат диалога (положительный или отрицательный)
        ''' </summary>
        Public MessageResult As Boolean
        ''' <summary>
        ''' Время, когда нужно скрыть сообщение автоматически
        ''' </summary>
        Private StopTime As New Date
#End Region
#Region "Процедуры и функции"
        ''' <summary>
        ''' Показывает сообщение
        ''' </summary>
        ''' <param name="msgText">Текст сообщения</param>
        ''' <param name="msgTitle">Заголовок сообщения</param>
        ''' <param name="msgOptons">Опции сообщения</param>
        ''' <returns></returns>
        Public Async Function ShowMessage(msgText As String, Optional msgTitle As String = "Внимание!", Optional msgOptons As MessageOptions = Nothing) As Task(Of Boolean) Implements IMessageWorker.ShowMessage
            'Устанавливаем флаг, что сообщение открыто
            IsOpen = True
            'Добавляем к времени окончания показа сообщения 4 секунды
            StopTime = Date.Now.AddSeconds(4)
            'Если опции не переданы, то загружаем их базовые значение
            If msgOptons Is Nothing Then msgOptons = New MessageOptions
            'Установка стиля сообщения
            MessageStyle = msgOptons.MessageStyle
            'Установка тектса сообщения
            MessageText = msgText
            'Установка загаловка сообщения
            TitleText = msgTitle
            'Если в опциях сообщения был флаг на автоскрытия сообщения, то вызываем соответствующую процедуру
            If msgOptons.IsAutoHide Then AutoStop()
            'Устанавливаем флаг на блокировку интерфейса в зависимости от настроек сообщения или его типа
            IsTopMost = msgOptons.IsTopMost Or MessageStyle = MessageStyle.YesNo
            If IsTopMost Then
                'Если стиль собщения требует реакции пользователя, запускаем асинхронное ожидание этой реакции
                Return Await Task.Run(Function()
                                          Do
                                          Loop While IsOpen
                                          Return MessageResult
                                      End Function)
            Else
                Return True
            End If
        End Function
        ''' <summary>
        ''' Закрывает Диалог
        ''' </summary>
        Public Async Sub CloseMessage(isOK As Boolean)
            'Присваееваем результат отображения диалога (положительнпая или отрицательная рекция пользователя)
            MessageResult = isOK
            'Отменяем флаг ожидание реакции пользователя
            IsOpen = False
            'Небольшая пауза для естесвенной анимации скрытия сообщения
            Await Task.Delay(100)
            'Сбрасываем время автоскрытия
            StopTime = Date.Now
            'Снимаем блокировку интерфейса
            IsTopMost = False
        End Sub
        ''' <summary>
        ''' Ожидает время автоскрытия сообщения
        ''' </summary>
        Private Async Sub AutoStop()
            Await Task.Run(Sub()
                               Do
                               Loop While Date.Now < StopTime
                               CloseMessage(True)
                           End Sub)
        End Sub
#End Region
#Region "Шаблоны собщений"
        ''' <summary>
        ''' Возвращает стандартные настроки сообщения для типа сообщения ожидаюего реакцию пользователя
        ''' </summary>
        ''' <returns></returns>
        Public Shared Function GetStandartYesNoOptions() As MessageOptions
            Return New MessageOptions With {.IsAutoHide = False, .IsTopMost = True, .MessageStyle = MessageStyle.YesNo}
        End Function
        ''' <summary>
        ''' Возвращает стандартные настроки сообщения для информационного сообщение, которое не скрывается
        ''' </summary>
        Public Shared Function GetNoHideInfoOptions() As MessageOptions
            Return New MessageOptions With {.IsAutoHide = False, .IsTopMost = False, .MessageStyle = MessageStyle.InfoMessage}
        End Function
        ''' <summary>
        ''' Возвращает стандартные настроки сообщения для сообщения об ошибке поверх всех окон
        ''' </summary>
        Public Shared Function GetTopMostErrorOptions() As MessageOptions
            Return New MessageOptions With {.IsAutoHide = False, .IsTopMost = True, .MessageStyle = MessageStyle.ErrorMessage}
        End Function
        ''' <summary>
        ''' Возвращает стандартные настроки сообщения для информационного сообщения поверх всех окон
        ''' </summary>
        Public Shared Function GetTopMostInfoOptions() As MessageOptions
            Return New MessageOptions With {.IsAutoHide = False, .IsTopMost = True, .MessageStyle = MessageStyle.InfoMessage}
        End Function
#End Region
    End Class
End Namespace