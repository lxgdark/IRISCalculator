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
#End Region
#Region "Главне свойства"
#Region "Приватные"
    Private CurentSelectedPageValue As CurentSelectedPageEnum = CurentSelectedPageEnum.Home
#End Region
    Public Property GlobalPagesList As New ObservableCollection(Of GlobalPage) From {New GlobalPage With {.Header = "Главная"}}
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
        'Определяем тип текущего класса
        Dim meType As Type = [GetType]()
        'Проходим циклом по свойствам текущего класса
        For Each meProperty In meType.GetProperties
            'Если касс унаследован от Базового воркера
            If meProperty.PropertyType.BaseType.ToString.IndexOf("BaseWorker") > -1 Then
                'Если запуск стартовых задач воркера вернул False
                If Not Await meProperty.PropertyType.GetMethod("StartActions").Invoke(meProperty.GetValue(Me), {Me}) Then
                    'То возвращаем False
                    Return False
                End If
            End If
        Next
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
#End Region
End Class