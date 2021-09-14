Imports WPFProjectCore
Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers
Public Class AppCore
    Inherits ApplicationData_Base
#Region "Реализация интерфейса"
    ''' <summary>
    ''' Определяет им папки локальных настроек приложения
    ''' </summary>
    ''' <returns></returns>
    Protected Overrides Property AppSettingPath As String = "CopycenterManager"
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
#End Region
End Class