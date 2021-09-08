Imports System.Collections.ObjectModel
Imports IRISCalculator.DataClasses

Namespace Workers
    Public Class ThemeWorker
        Inherits BaseWorker(Of ThemeWorker)
#Region "Свойства"
#Region "Внутренние"
        Private ReadOnly CurrentWorkerValue As New ThemeClass
        Private ReadOnly CustomThemeListValue As New ObservableCollection(Of ThemeClass)
#End Region
        ''' <summary>
        ''' Текущая выбранная тема
        ''' </summary>
        ''' <returns></returns>
        Public Property CurrentTheme As ThemeClass
            Get
                Return CurrentWorkerValue
            End Get
            Set(value As ThemeClass)
                CurrentWorkerValue.SetPropertys(value)
            End Set
        End Property
        ''' <summary>
        ''' Список персональных тем
        ''' </summary>
        Public Property CustomThemeList As ObservableCollection(Of ThemeClass)
            Get
                Return CustomThemeListValue
            End Get
            Set(value As ObservableCollection(Of ThemeClass))
                CustomThemeListValue.Clear()
                For Each l In value
                    CustomThemeListValue.Add(l)
                Next
                OnPropertyChanged(NameOf(CustomThemeList))
            End Set
        End Property
#End Region
#Region "Перечеслители, типы и константы"
        ''' <summary>
        ''' Перечеслитель режима отображения фонового фото
        ''' </summary>
        Public Enum BackgroundImageVisualModeEnum
            ''' <summary>
            ''' Заполнить
            ''' </summary>
            Stretch = 0
            ''' <summary>
            ''' Замостить
            ''' </summary>
            Tile = 1
        End Enum
        ''' <summary>
        ''' Абсолютная ссылка на стандартное изображения фона
        ''' </summary>
        Public Const BaseImageRelativeString As String = "pack://application:,,,/Pict/Logo/Logo.png"
#End Region
    End Class
End Namespace