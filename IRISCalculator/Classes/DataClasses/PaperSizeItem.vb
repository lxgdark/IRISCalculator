Imports WPFProjectCore
Namespace DataClasses
    ''' <summary>
    ''' ОПисывает размеры бумаги (изделия)
    ''' </summary>
    Public Class PaperSizeItem
        Inherits NotifyProperty_Base(Of PaperSizeItem)
#Region "События"
        Public Event SizeChenged()
#End Region
#Region "Свойства"
#Region "Внутренние"
        Private nameValue As String = "SRA3"
        Private heightValue As Double = 320
        Private widthValue As Double = 450
        Private fieldHeightValue As Double = 5
        Private fieldWidthValue As Double = 5
#End Region
        ''' <summary>
        ''' Имя размера
        ''' </summary>
        ''' <returns></returns>
        Public Property Name As String
            Get
                Return nameValue
            End Get
            Set(value As String)
                nameValue = value
                OnPropertyChanged(NameOf(Name))
                RaiseEvent SizeChenged()
            End Set
        End Property
        ''' <summary>
        ''' Высота бумаги (изделия)
        ''' </summary>
        ''' <returns></returns>
        Public Property Height As Double
            Get
                Return heightValue
            End Get
            Set(value As Double)
                heightValue = value
                OnPropertyChanged(NameOf(Height))
                RaiseEvent SizeChenged()
            End Set
        End Property
        ''' <summary>
        ''' Ширина бумаги (изделия)
        ''' </summary>
        ''' <returns></returns>
        Public Property Width As Double
            Get
                Return widthValue
            End Get
            Set(value As Double)
                widthValue = value
                OnPropertyChanged(NameOf(Width))
                RaiseEvent SizeChenged()
            End Set
        End Property
        ''' <summary>
        ''' Отступы от края/вылеты по высоте (верх, низ)
        ''' </summary>
        ''' <returns></returns>
        Public Property FieldHeight As Double
            Get
                Return fieldHeightValue
            End Get
            Set(value As Double)
                fieldHeightValue = value
                OnPropertyChanged(NameOf(FieldHeight))
                RaiseEvent SizeChenged()
            End Set
        End Property
        ''' <summary>
        ''' Отступы от края/вылеты по ширине (лево, право)
        ''' </summary>
        ''' <returns></returns>
        Public Property FieldWidth As Double
            Get
                Return fieldWidthValue
            End Get
            Set(value As Double)
                fieldWidthValue = value
                OnPropertyChanged(NameOf(FieldWidth))
                RaiseEvent SizeChenged()
            End Set
        End Property
#End Region
    End Class
End Namespace