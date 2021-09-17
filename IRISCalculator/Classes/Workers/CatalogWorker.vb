Imports Microsoft.VisualBasic
Imports IRISCalculator.DataClasses
Imports ClosedXML.Excel
Namespace Workers
    Public Class CatalogWorker
        Inherits BaseWorker(Of CatalogWorker)
#Region "Свойства"
#Region "Внутренние"

#End Region

#End Region
#Region "Процедуры и функции"
        Public Overrides Async Function StartActions(app As AppCore) As Task(Of Boolean)
            If IO.File.Exists(app.LocalSettings.CatalogPath) Then
                Return Await Task.Run(Function()
                                          Return ExelParser(app.LocalSettings.CatalogPath)
                                      End Function)
            Else
                Return False
            End If
        End Function
#Region "Внутренние"
        Private Function ExelParser(path As String) As Boolean
            Dim l As New List(Of CatalogItem)
            'Try
            Dim exelCatalog As New XLWorkbook(path)
            Dim catalogSheet As IXLWorksheet = exelCatalog.Worksheets.First
            For Each cell In catalogSheet.Cells
                If cell.WorksheetColumn.ColumnNumber = 1 Then
                    If catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "" And catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "Код" Then
                        Dim catalogItem As New CatalogItem
                        catalogItem.Name = cell.Value
                        CatalogItem.Price = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value, 0)
                        CatalogItem.Code = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value
                        CatalogItem.CostPrice = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value, 0)
                        CatalogItem.Unit = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 5).Value
                        catalogItem.GroupCode = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 6).Value


                    End If
                End If
            Next
            'Catch ex As Exception
            '    Return False
            'End Try
            Return True
        End Function
#End Region
#End Region
    End Class
End Namespace
