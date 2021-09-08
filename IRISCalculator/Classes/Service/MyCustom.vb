Imports Microsoft.VisualBasic

Namespace My
    <HideModuleName()>
    Module MyCustomExtensions
        Friend ReadOnly Property AppCore As AppCore
            Get
                Return DirectCast(System.Windows.Application.Current.Resources("AppCore"), AppCore)
            End Get
        End Property
        Friend ReadOnly Property MessageWorker As Workers.MessageWorker
            Get
                Return DirectCast(System.Windows.Application.Current.Resources("AppCore"), AppCore).MessageWorker
            End Get
        End Property
    End Module
End Namespace