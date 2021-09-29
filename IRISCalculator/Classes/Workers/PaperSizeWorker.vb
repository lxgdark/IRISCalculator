Imports System.Collections.ObjectModel
Imports IRISCalculator.DataClasses
Namespace Workers
    Public Class PaperSizeWorker
        Inherits BaseWorker(Of PaperSizeWorker)
#Region "Свойства"
#Region "Внутренние"
#End Region
        Public Property PaperSizeList As New ObservableCollection(Of PaperSizeItem)
#End Region
#Region "Процедуры и функции"
        Public Overrides Function StartActions(app As AppCore) As Task(Of Boolean)
            SetStandartSize()
            Return MyBase.StartActions(app)
        End Function
#Region "Внутренние"
        Private Sub SetStandartSize()
            Dim sra3 As New PaperSizeItem
            PaperSizeList.Add(sra3)

            Dim a3 As New PaperSizeItem With {
            .Name = "А3",
            .Height = 297,
            .Width = 420,
            .FieldHeight = 3,
            .FieldWidth = 3
            }
            PaperSizeList.Add(a3)

            Dim a4 As New PaperSizeItem With {
                .Name = "А4",
                .Height = 210,
                .Width = 297,
                .FieldHeight = 3,
                .FieldWidth = 3
            }
            PaperSizeList.Add(a4)

            Dim a5 As New PaperSizeItem With {
    .Name = "А5",
    .Height = 148,
    .Width = 210,
    .FieldHeight = 3,
    .FieldWidth = 3
}
            PaperSizeList.Add(a5)

            Dim a6 As New PaperSizeItem With {
    .Name = "А6",
    .Height = 105,
    .Width = 148,
    .FieldHeight = 3,
    .FieldWidth = 3
}
            PaperSizeList.Add(a6)

            Dim personCard As New PaperSizeItem With {
    .Name = "Визитка 90х50",
    .Height = 50,
    .Width = 90,
    .FieldHeight = 3,
    .FieldWidth = 3
}
            PaperSizeList.Add(personCard)

        End Sub
#End Region
#End Region
    End Class
End Namespace
