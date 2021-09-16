Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Класс локльных настроек приложения
    ''' </summary>
    Public Class LocalSettings
        Inherits NotifyProperty_Base(Of LocalSettings)
#Region "Свойства"
#Region "Внутренние"
        Private catalogPathValue As String = ""

#End Region
        ''' <summary>
        ''' Путь к файлу каталога типографии
        ''' </summary>
        ''' <returns></returns>
        Public Property CatalogPath As String
            Get
                Return catalogPathValue
            End Get
            Set(value As String)
                catalogPathValue = value
                OnPropertyChanged(NameOf(CatalogPath))
            End Set
        End Property
#End Region
    End Class
End Namespace