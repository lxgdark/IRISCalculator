Imports System.Threading.Tasks
Imports WPFProjectCore

Namespace Workers
    Public Class BaseWorker(Of T As {Class, New})
        Inherits NotifyProperty_Base(Of T)
        Private IsInProgressValue As Boolean = False
        ''' <summary>
        ''' Флаг, указывающий идет ли в данный момент процесс выполнения воркера
        ''' </summary>
        ''' <returns></returns>
        Public Property IsInProgress As Boolean
            Get
                Return IsInProgressValue
            End Get
            Set(value As Boolean)
                IsInProgressValue = value
                OnPropertyChanged(NameOf(IsInProgress))
            End Set
        End Property
        Protected Property AppCore As AppCore
        ''' <summary>
        ''' Старотовые действия воркера
        ''' </summary>
        Public Overridable Function StartActions(app As AppCore) As Task(Of Boolean)
            AppCore = app
            Return Task.FromResult(True)
        End Function
        ''' <summary>
        ''' Закрывающие действия воркера
        ''' </summary>
        Public Overridable Sub StopActions()
            SetPropertys(New T)
        End Sub
    End Class
End Namespace