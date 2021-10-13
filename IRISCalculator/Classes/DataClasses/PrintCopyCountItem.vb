Imports Microsoft.VisualBasic
Imports WPFProjectCore

Namespace DataClasses
    Public Class PrintCopyCountItem
        Inherits NotifyProperty_Base(Of PrintCopyCountItem)
#Region "Свойства"
#Region "Внутренние"
        Private printCopyCountValue As Integer = 100
        Private costPriceForOneValue As Double = 0
        Private costPriceForAllValue As Double = 0
        Private salePriceForOneValue As Double = 0
        Private salePriceForAllValue As Double = 0
        Private minPrintCountValue As Integer = 0
#End Region
        ''' <summary>
        ''' Тираж
        ''' </summary>
        ''' <returns></returns>
        Public Property PrintCopyCount As Integer
            Get
                Return printCopyCountValue
            End Get
            Set(value As Integer)
                printCopyCountValue = value
                OnPropertyChanged(NameOf(PrintCopyCount))
                Calculation()
            End Set
        End Property
        ''' <summary>
        ''' Себестоимость за единицу
        ''' </summary>
        ''' <returns></returns>
        Public Property CostPriceForOne As Double
            Get
                Return costPriceForOneValue
            End Get
            Set(value As Double)
                costPriceForOneValue = value
                OnPropertyChanged(NameOf(CostPriceForOne))
                Calculation()
            End Set
        End Property
        ''' <summary>
        ''' Цена продажи за шт.
        ''' </summary>
        ''' <returns></returns>
        Public Property SalePriceForOne As Double
            Get
                Return salePriceForOneValue
            End Get
            Set(value As Double)
                salePriceForOneValue = value
                OnPropertyChanged(NameOf(SalePriceForOne))
            End Set
        End Property
        ''' <summary>
        ''' Цена продажи тиража
        ''' </summary>
        ''' <returns></returns>
        Public Property SalePriceForAll As Double
            Get
                Return salePriceForAllValue
            End Get
            Set(value As Double)
                salePriceForAllValue = value
                OnPropertyChanged(NameOf(SalePriceForAll))
            End Set
        End Property
        Public Property CurrentCalculationFormula As ICalculationFormula
        ''' <summary>
        ''' Минимальный тираж
        ''' </summary>
        ''' <returns></returns>
        Public Property MinPrintCount As Integer
            Get
                Return minPrintCountValue
            End Get
            Set(value As Integer)
                minPrintCountValue = value
                OnPropertyChanged(NameOf(MinPrintCount))
            End Set
        End Property
        ''' <summary>
        ''' Себестоимость за все
        ''' </summary>
        ''' <returns></returns>
        Public Property CostPriceForAll As Double
            Get
                Return costPriceForAllValue
            End Get
            Set(value As Double)
                costPriceForAllValue = value
                OnPropertyChanged(NameOf(CostPriceForAll))
            End Set
        End Property
#End Region
#Region "Процедуры и функции"
#Region "Внутренние"
        Private Sub Calculation()
            SalePriceForAll = CurrentCalculationFormula.GetCalculationSumm(PrintCopyCount, CostPriceForOne)
            SalePriceForOne = SalePriceForAll / PrintCopyCount
            CostPriceForAll = PrintCopyCount * CostPriceForOne
        End Sub
#End Region

#End Region
    End Class
End Namespace
