Imports WPFProjectCore
Namespace DataClasses
    Public Class CatalogItem
        Inherits NotifyProperty_Base(Of CatalogItem)
#Region "Свойства"
#Region "Внутренние"
        Private nameValue As String = ""
        Private priceValue As Double = 0
        Private codeValue As String = ""
        Private costPriceValue As Double = 0
        Private unitValue As String = ""
        Private groupCodeValue As String = ""
#End Region
        ''' <summary>
        ''' Наименование наменклатуры
        ''' </summary>
        ''' <returns></returns>
        Public Property Name As String
            Get
                Return nameValue
            End Get
            Set(value As String)
                nameValue = value
                OnPropertyChanged(NameOf(Name))
            End Set
        End Property
        ''' <summary>
        ''' Цена продажи
        ''' </summary>
        ''' <returns></returns>
        Public Property Price As Double
            Get
                Return priceValue
            End Get
            Set(value As Double)
                priceValue = value
                OnPropertyChanged(NameOf(Price))
            End Set
        End Property
        ''' <summary>
        ''' Код в каталоге
        ''' </summary>
        ''' <returns></returns>
        Public Property Code As String
            Get
                Return codeValue
            End Get
            Set(value As String)
                codeValue = value
                OnPropertyChanged(NameOf(Code))
            End Set
        End Property
        ''' <summary>
        ''' Себестоимость
        ''' </summary>
        ''' <returns></returns>
        Public Property CostPrice As Double
            Get
                Return costPriceValue
            End Get
            Set(value As Double)
                costPriceValue = value
                OnPropertyChanged(NameOf(CostPrice))
            End Set
        End Property
        ''' <summary>
        ''' Единица измерения
        ''' </summary>
        ''' <returns></returns>
        Public Property Unit As String
            Get
                Return unitValue
            End Get
            Set(value As String)
                unitValue = value
                OnPropertyChanged(NameOf(Unit))
            End Set
        End Property
        ''' <summary>
        ''' Код раздела каталога в котором находится позиция
        ''' </summary>
        ''' <returns></returns>
        Public Property GroupCode As String
            Get
                Return groupCodeValue
            End Get
            Set(value As String)
                groupCodeValue = value
                OnPropertyChanged(NameOf(GroupCode))
            End Set
        End Property
#End Region
    End Class
End Namespace