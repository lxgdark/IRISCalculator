Imports WPFProjectCore

Namespace DataClasses
    Public Class ThemeClass
        Inherits NotifyProperty_Base(Of ThemeClass)
#Region "Свойства"
#Region "Приватные"
        Private WindowBackgroundColorValue As Color = Colors.LightGray
        Private WindowBackgroundImageOpacityValue As Double = 0.5
        Private BackgroundImageValue As String = Workers.ThemeWorker.BaseImageRelativeString
        Private BackgroundImageVisualModeValue As Workers.ThemeWorker.BackgroundImageVisualModeEnum = Workers.ThemeWorker.BackgroundImageVisualModeEnum.Tile

        Private BorderLightColorValue As Color = Colors.LightGray
        Private BorderStandartColorValue As Color = ColorConverter.ConvertFromString("#e8e8e8")
        Private BorderDarkColorValue As Color = ColorConverter.ConvertFromString("#595554")
        Private BackgroundLightColorValue As Color = Colors.White
        Private BackgroundStandartColorValue As Color = ColorConverter.ConvertFromString("#e8e8e8")
        Private BackgroundDarkColorValue As Color = ColorConverter.ConvertFromString("#595554")
        Private InputElementLightColorValue As Color = Colors.White
        Private InputElementStandartColorValue As Color = ColorConverter.ConvertFromString("#e8e8e8")
        Private InputElementDarkColorValue As Color = ColorConverter.ConvertFromString("#595554")
        Private ButtonLightColorValue As Color = Colors.LightGray
        Private ButtonStandartColorValue As Color = Colors.Gray
        Private ButtonDarkColorValue As Color = ColorConverter.ConvertFromString("#595554")
        Private TextLightColorValue As Color = Colors.White
        Private TextStandartColorValue As Color = ColorConverter.ConvertFromString("#e8e8e8")
        Private TextDarkColorValue As Color = ColorConverter.ConvertFromString("#595554")
        Private MessageBoxInfoColorValue As Color = Colors.DarkGreen 'ColorConverter.ConvertFromString("#FF63B56B")
        Private MessageBoxQuestionColorValue As Color = ColorConverter.ConvertFromString("#FF9EBBFF")
        Private MessageBoxErrorColorValue As Color = ColorConverter.ConvertFromString("#FFBA4949")
        Private ImportantColorValue As Color = Colors.Red
        Private FavoriteColorValue As Color = Colors.Green
        Private InformationColorValue As Color = Colors.Blue

#End Region
        ''' <summary>
        ''' Заливка главного окна
        ''' </summary>
        ''' <returns></returns>
        Public Property WindowBackgroundColor As Color
            Get
                Return WindowBackgroundColorValue
            End Get
            Set(value As Color)
                WindowBackgroundColorValue = value
                OnPropertyChanged(NameOf(WindowBackgroundColor))
            End Set
        End Property
        ''' <summary>
        ''' Прозрачность фоновой картинки
        ''' </summary>
        ''' <returns></returns>
        Public Property WindowBackgroundImageOpacity As Double
            Get
                Return WindowBackgroundImageOpacityValue
            End Get
            Set(value As Double)
                WindowBackgroundImageOpacityValue = value
                OnPropertyChanged(NameOf(WindowBackgroundImageOpacity))
            End Set
        End Property
        ''' <summary>
        ''' Фоновая картинка
        ''' </summary>
        ''' <returns></returns>
        Public Property BackgroundImage As String
            Get
                Return BackgroundImageValue
            End Get
            Set(value As String)
                BackgroundImageValue = value
                OnPropertyChanged(NameOf(BackgroundImage))
            End Set
        End Property
        ''' <summary>
        ''' Стиль отображения фоновой картинки
        ''' </summary>
        ''' <returns></returns>
        Public Property BackgroundImageVisualMode As Workers.ThemeWorker.BackgroundImageVisualModeEnum
            Get
                Return BackgroundImageVisualModeValue
            End Get
            Set(value As Workers.ThemeWorker.BackgroundImageVisualModeEnum)
                BackgroundImageVisualModeValue = value
                OnPropertyChanged(NameOf(BackgroundImageVisualMode))
            End Set
        End Property
        ''' <summary>
        ''' Светлый цвет рамки 
        ''' </summary>
        ''' <returns></returns>
        Public Property BorderLightColor As Color
            Get
                Return BorderLightColorValue
            End Get
            Set(value As Color)
                BorderLightColorValue = value
                OnPropertyChanged(NameOf(BorderLightColor))
            End Set
        End Property
        ''' <summary>
        ''' Стандартный цвет рамок
        ''' </summary>
        ''' <returns></returns>
        Public Property BorderStandartColor As Color
            Get
                Return BorderStandartColorValue
            End Get
            Set(value As Color)
                BorderStandartColorValue = value
                OnPropertyChanged(NameOf(BorderStandartColor))
            End Set
        End Property
        ''' <summary>
        ''' Темный цвет рамок
        ''' </summary>
        ''' <returns></returns>
        Public Property BorderDarkColor As Color
            Get
                Return BorderDarkColorValue
            End Get
            Set(value As Color)
                BorderDarkColorValue = value
                OnPropertyChanged(NameOf(BorderDarkColor))
            End Set
        End Property
        ''' <summary>
        ''' Светлый фон
        ''' </summary>
        ''' <returns></returns>
        Public Property BackgroundLightColor As Color
            Get
                Return BackgroundLightColorValue
            End Get
            Set(value As Color)
                BackgroundLightColorValue = value
                OnPropertyChanged(NameOf(BackgroundLightColor))
            End Set
        End Property
        ''' <summary>
        ''' Стандартный фон
        ''' </summary>
        ''' <returns></returns>
        Public Property BackgroundStandartColor As Color
            Get
                Return BackgroundStandartColorValue
            End Get
            Set(value As Color)
                BackgroundStandartColorValue = value
                OnPropertyChanged(NameOf(BackgroundStandartColor))
            End Set
        End Property
        ''' <summary>
        ''' Темный фон
        ''' </summary>
        ''' <returns></returns>
        Public Property BackgroundDarkColor As Color
            Get
                Return BackgroundDarkColorValue
            End Get
            Set(value As Color)
                BackgroundDarkColorValue = value
                OnPropertyChanged(NameOf(BackgroundDarkColor))
            End Set
        End Property
        ''' <summary>
        ''' Светлый цвет элемента ввода
        ''' </summary>
        ''' <returns></returns>
        Public Property InputElementLightColor As Color
            Get
                Return InputElementLightColorValue
            End Get
            Set(value As Color)
                InputElementLightColorValue = value
                OnPropertyChanged(NameOf(InputElementLightColor))
            End Set
        End Property
        ''' <summary>
        ''' Стандартный цвет элемента ввода
        ''' </summary>
        ''' <returns></returns>
        Public Property InputElementStandartColor As Color
            Get
                Return InputElementStandartColorValue
            End Get
            Set(value As Color)
                InputElementStandartColorValue = value
                OnPropertyChanged(NameOf(InputElementStandartColor))
            End Set
        End Property
        ''' <summary>
        ''' Темный цвет элемента ввода
        ''' </summary>
        ''' <returns></returns>
        Public Property InputElementDarkColor As Color
            Get
                Return InputElementDarkColorValue
            End Get
            Set(value As Color)
                InputElementDarkColorValue = value
                OnPropertyChanged(NameOf(InputElementDarkColor))
            End Set
        End Property
        ''' <summary>
        ''' Светлый фон кнопок
        ''' </summary>
        ''' <returns></returns>
        Public Property ButtonLightColor As Color
            Get
                Return ButtonLightColorValue
            End Get
            Set(value As Color)
                ButtonLightColorValue = value
                OnPropertyChanged(NameOf(ButtonLightColor))
            End Set
        End Property
        ''' <summary>
        ''' Стандартный цвет кнопок
        ''' </summary>
        ''' <returns></returns>
        Public Property ButtonStandartColor As Color
            Get
                Return ButtonStandartColorValue
            End Get
            Set(value As Color)
                ButtonStandartColorValue = value
                OnPropertyChanged(NameOf(ButtonStandartColor))
            End Set
        End Property
        ''' <summary>
        ''' Темный цвет кнопок
        ''' </summary>
        ''' <returns></returns>
        Public Property ButtonDarkColor As Color
            Get
                Return ButtonDarkColorValue
            End Get
            Set(value As Color)
                ButtonDarkColorValue = value
                OnPropertyChanged(NameOf(ButtonDarkColor))
            End Set
        End Property
        ''' <summary>
        ''' Светлый цвет текста
        ''' </summary>
        ''' <returns></returns>
        Public Property TextLightColor As Color
            Get
                Return TextLightColorValue
            End Get
            Set(value As Color)
                TextLightColorValue = value
                OnPropertyChanged(NameOf(TextLightColor))
            End Set
        End Property
        ''' <summary>
        ''' Средний цвет текста
        ''' </summary>
        ''' <returns></returns>
        Public Property TextStandartColor As Color
            Get
                Return TextStandartColorValue
            End Get
            Set(value As Color)
                TextStandartColorValue = value
                OnPropertyChanged(NameOf(TextStandartColor))
            End Set
        End Property
        ''' <summary>
        ''' Темный цвет текста
        ''' </summary>
        ''' <returns></returns>
        Public Property TextDarkColor As Color
            Get
                Return TextDarkColorValue
            End Get
            Set(value As Color)
                TextDarkColorValue = value
                OnPropertyChanged(NameOf(TextDarkColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет информационных сообщений
        ''' </summary>
        ''' <returns></returns>
        Public Property MessageBoxInfoColor As Color
            Get
                Return MessageBoxInfoColorValue
            End Get
            Set(value As Color)
                MessageBoxInfoColorValue = value
                OnPropertyChanged(NameOf(MessageBoxInfoColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет сообщений с вовпросом
        ''' </summary>
        ''' <returns></returns>
        Public Property MessageBoxQuestionColor As Color
            Get
                Return MessageBoxQuestionColorValue
            End Get
            Set(value As Color)
                MessageBoxQuestionColorValue = value
                OnPropertyChanged(NameOf(MessageBoxQuestionColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет сообщений об ошибке
        ''' </summary>
        ''' <returns></returns>
        Public Property MessageBoxErrorColor As Color
            Get
                Return MessageBoxErrorColorValue
            End Get
            Set(value As Color)
                MessageBoxErrorColorValue = value
                OnPropertyChanged(NameOf(MessageBoxErrorColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет важного
        ''' </summary>
        ''' <returns></returns>
        Public Property ImportantColor As Color
            Get
                Return ImportantColorValue
            End Get
            Set(value As Color)
                ImportantColorValue = value
                OnPropertyChanged(NameOf(ImportantColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет личного и избранного
        ''' </summary>
        ''' <returns></returns>
        Public Property FavoriteColor As Color
            Get
                Return FavoriteColorValue
            End Get
            Set(value As Color)
                FavoriteColorValue = value
                OnPropertyChanged(NameOf(FavoriteColor))
            End Set
        End Property
        ''' <summary>
        ''' Цвет информационных данных
        ''' </summary>
        ''' <returns></returns>
        Public Property InformationColor As Color
            Get
                Return InformationColorValue
            End Get
            Set(value As Color)
                InformationColorValue = value
                OnPropertyChanged(NameOf(InformationColor))
            End Set
        End Property
#End Region
    End Class
End Namespace