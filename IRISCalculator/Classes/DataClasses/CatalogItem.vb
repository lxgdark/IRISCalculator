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
        Private commentValue As String = ""
        Private itemCategoryValue As ItemCategoryEnum = ItemCategoryEnum.Paper
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
        ''' <summary>
        ''' Категория текущей позиции (бумага, услуга, позиция постпечатки...)
        ''' </summary>
        ''' <returns></returns>
        Public Property ItemCategory As ItemCategoryEnum
            Get
                Return itemCategoryValue
            End Get
            Set(value As ItemCategoryEnum)
                itemCategoryValue = value
                OnPropertyChanged(NameOf(ItemCategory))
            End Set
        End Property
        ''' <summary>
        ''' Комментарий к позиции
        ''' </summary>
        ''' <returns></returns>
        Public Property Comment As String
            Get
                Return commentValue
            End Get
            Set(value As String)
                commentValue = value
                OnPropertyChanged(NameOf(Comment))
            End Set
        End Property
#End Region
#Region "Перечеслители и типы"
        Public Enum ItemCategoryEnum
            ''' <summary>
            ''' Категория неопеределена или не задана
            ''' </summary>
            NONE = 0
            ''' <summary>
            ''' Бумага (дизайнерская, синтетика, самоклейка -  то есть все на чем можно печатать)
            ''' </summary>
            PAPER = 1
            ''' <summary>
            ''' Услуга печати
            ''' </summary>
            SERVICEPRINT = 2000
            ''' <summary>
            ''' Услуга резки
            ''' </summary>
            SERVICECUT = 2001
            ''' <summary>
            ''' Услуга постпечатки
            ''' </summary>
            SERVICEPOSTPRINT = 2002
            ''' <summary>
            ''' Gthtpfrfpsdftvst eckeub
            ''' </summary>
            SERVICEOTHER = 2003
        End Enum
#End Region
    End Class
End Namespace