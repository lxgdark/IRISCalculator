Imports System.ComponentModel
Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Класс описывающий заказ
    ''' </summary>
    Public Class StandartOrder
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

#End Region

#End Region
    End Class
End Namespace