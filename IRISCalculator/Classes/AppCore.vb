Imports WPFProjectCore
Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers
Imports System.Collections.ObjectModel
Public Class AppCore
    Inherits ApplicationData_Base
#Region "Реализация интерфейса"
    ''' <summary>
    ''' Определяет им папки локальных настроек приложения
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Property AppSettingPath As String = "IRISCalculator"
    ''' <summary>
    ''' Содержит настройки приложения
    ''' </summary>
    ''' <remarks>Загружает настройки из файла при инициализации приложения</remarks>
    Public Property LocalSettings As LocalSettings = LoadSettings(Of LocalSettings)()
    ''' <summary>
    ''' Закрытая функция возвращающая текущий экземпляр IMessageWorker для обращения из базового класса
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Function GetMessageWorker() As IMessageWorker
        Return MessageWorker
    End Function
#End Region
#Region "События"
    ''' <summary>
    ''' Событие обновления интерфейса
    ''' </summary>
    Public Event UpdateInterface()
#End Region
#Region "Воркеры"
    ''' <summary>
    ''' Сообщения приложения
    ''' </summary>
    ''' <returns></returns>
    Public Property MessageWorker As New MessageWorker
    ''' <summary>
    ''' Тема приложения
    ''' </summary>
    ''' <returns></returns>
    Public Property ThemeWorker As New ThemeWorker
    ''' <summary>
    ''' Работа с каталогом типографии
    ''' </summary>
    ''' <returns></returns>
    Public Property CatalogWorker As New CatalogWorker
    ''' <summary>
    ''' Работает с размерами бумаги (сохранение, редактирование и пр.)
    ''' </summary>
    ''' <returns></returns>
    Public Property PaperSizeWorker As New PaperSizeWorker
#End Region
#Region "Главне свойства"
#Region "Приватные"
    Private CurentSelectedPageValue As CurentSelectedPageEnum = CurentSelectedPageEnum.Home
    Private SynchronizationStatusValue As SynchronizationStatusEnum = SynchronizationStatusEnum.Synchronization
#End Region
    ''' <summary>
    ''' Список страниц с расчетами
    ''' </summary>
    ''' <returns></returns>
    Public Property GlobalPagesList As New ObservableCollection(Of GlobalPageWorker) From {New GlobalPageWorker With {.Header = "Главная"}}
    ''' <summary>
    ''' Список позиций каталога
    ''' </summary>
    ''' <returns></returns>
    Public Property CatalogList As New ObservableCollection(Of CatalogItem)
    ''' <summary>
    ''' Текущая страница в главном окне
    ''' </summary>
    ''' <returns></returns>
    Public Property CurentSelectedPage As CurentSelectedPageEnum
        Get
            Return CurentSelectedPageValue
        End Get
        Set(value As CurentSelectedPageEnum)
            CurentSelectedPageValue = value
            OnPropertyChanged("CurentSelectedPage")
        End Set
    End Property
    ''' <summary>
    ''' Статус синхронизации с каталогом
    ''' </summary>
    ''' <returns></returns>
    Public Property SynchronizationStatus As SynchronizationStatusEnum
        Get
            Return SynchronizationStatusValue
        End Get
        Set(value As SynchronizationStatusEnum)
            SynchronizationStatusValue = value
            OnPropertyChanged(NameOf(SynchronizationStatus))
        End Set
    End Property
#End Region
#Region "Процедуры и функции"
#Region "Вызов событий"
    ''' <summary>
    ''' Вызывает событие обновления интерфейса
    ''' </summary>
    Public Sub OnInterfaceUpdate()
        RaiseEvent UpdateInterface()
    End Sub
#End Region
#Region "Синхронизация"
    ''' <summary>
    ''' Синхронизация данных
    ''' </summary>
    Public Async Function Synchronization() As Task(Of Boolean)
        SynchronizationStatus = SynchronizationStatusEnum.Synchronization
        'Определяем тип текущего класса
        Dim meType As Type = [GetType]()
        'Проходим циклом по свойствам текущего класса
        For Each meProperty In meType.GetProperties
            'Если касс унаследован от Базового воркера
            If meProperty.PropertyType.BaseType.ToString.IndexOf("BaseWorker") > -1 Then
                'Если запуск стартовых задач воркера вернул False
                If Not Await meProperty.PropertyType.GetMethod("StartActions").Invoke(meProperty.GetValue(Me), {Me}) Then
                    'Статус синхронизации выставлям в ошибку
                    SynchronizationStatus = SynchronizationStatusEnum.SynchronizationError
                    'То возвращаем False
                    Return False
                End If
            End If
        Next
        'Статус синхронизации выставлям в синхронизировано!
        SynchronizationStatus = SynchronizationStatusEnum.Synchronized
        'Если ранее все прошло хорошо, возвращаем True
        Return True
    End Function
#End Region
#End Region
#Region "Перечеслители и типы"
    ''' <summary>
    ''' Перечеслитель вариантов отображения головной страницы
    ''' </summary>
    Public Enum CurentSelectedPageEnum
        ''' <summary>
        ''' Домашняя страница
        ''' </summary>
        Home = 0
        ''' <summary>
        ''' Страница управления расчетами
        ''' </summary>
        ManageOrders = 1
        ''' <summary>
        ''' Страница управления сохраненными расчетами
        ''' </summary>
        ManageSavedOrders = 2
        'Временная заглушка
        temp = 3
        ''' <summary>
        ''' Страница управления цветовыми настройками приложения
        ''' </summary>
        Theme = 4
        ''' <summary>
        ''' Страница настроек
        ''' </summary>
        Settings = 5
    End Enum
    ''' <summary>
    ''' Перечеслитель статусов синхронизации
    ''' </summary>
    Public Enum SynchronizationStatusEnum
        ''' <summary>
        ''' Идет синхронизация
        ''' </summary>
        Synchronization = 0
        ''' <summary>
        ''' Синхронизирован
        ''' </summary>
        Synchronized = 1
        ''' <summary>
        ''' Ошибка синхронизации
        ''' </summary>
        SynchronizationError = 2
    End Enum
#End Region
End Class