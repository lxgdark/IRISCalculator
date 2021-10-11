Imports WPFProjectCore

Namespace DataClasses
    ''' <summary>
    ''' Интерфейс для формул расчета тиража
    ''' </summary>
    Public Interface ICalculationFormula
        Function GetCalculationSumm(printCopyCount As Integer, costPriceForOne As Double) As Double
    End Interface
    ''' <summary>
    ''' Базовая формула для расчета типографских тиражей
    ''' </summary>
    Public Class StandartCalculationFormula
        Implements ICalculationFormula
#Region "Реализация интерфейса"
        ''' <summary>
        ''' Возвращает стоимость тиража
        ''' </summary>
        ''' <param name="printCopyCount"></param>
        ''' <param name="costPriceForOne"></param>
        ''' <returns>
        ''' % наценки = 200 - сумма себестоимости/200 (если результат меньше 40, то ставим 40)
        ''' При себестоимости меньше 1000 р., добавляется 300 р. к сумме на приладку и затраты времени оменеджера
        ''' </returns>
        Public Function GetCalculationSumm(printCopyCount As Integer, costPriceForOne As Double) As Double Implements ICalculationFormula.GetCalculationSumm
            Dim result As Double = 0
            Dim costPriceForAll As Double = printCopyCount * costPriceForOne
            Dim percent As Double = 200 - costPriceForAll / 200
            If percent < 40 Then percent = 40
            result = costPriceForAll + costPriceForAll / 100 * percent
            If result < 1000 Then result = result + 300
            Return result
        End Function
#End Region
    End Class
    ''' <summary>
    ''' Формула для расчета печати сигнальника
    ''' </summary>
    Public Class SignalCalculationFormula
        Implements ICalculationFormula
#Region "Реализация интерфейса"
        ''' <summary>
        ''' Возвращает стоимость тиража
        ''' </summary>
        ''' <param name="printCopyCount"></param>
        ''' <param name="costPriceForOne"></param>
        ''' <returns>
        ''' % наценки = 200 - сумма себестоимости/200 (если результат меньше 40, то ставим 40)
        ''' При себестоимости меньше 1000 р., добавляется 300 р. к сумме на приладку и затраты времени оменеджера
        ''' </returns>
        Public Function GetCalculationSumm(printCopyCount As Integer, costPriceForOne As Double) As Double Implements ICalculationFormula.GetCalculationSumm
            Dim result As Double = 0
            result = printCopyCount * costPriceForOne * 3
            If result < 1000 Then result = result + 300
            Return result
        End Function
#End Region
    End Class
End Namespace