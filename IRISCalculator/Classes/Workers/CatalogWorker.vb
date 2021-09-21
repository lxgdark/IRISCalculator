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
        ''' <summary>
        ''' Вызывается на старте. Обычно здесь происходят процедуры инициализации и синхронизации.
        ''' Может вызывать во время работы для повторной синхронизации
        ''' </summary>
        Public Overrides Async Function StartActions(app As AppCore) As Task(Of Boolean)
            'Если файл каталога найден...
            If IO.File.Exists(app.LocalSettings.CatalogPath) Then
                'Очищаем каталог типографии
                app.CatalogList.Clear()
                'Запрашиваем данные каталога из файла с помощью асинхронной функции
                'И проходим по полученному списку
                For Each l In Await Task.Run(Function()
                                                 Return ExelParser(app)
                                             End Function)
                    'Добавляем позиции в глобальный каталог в памяти
                    app.CatalogList.Add(l)
                Next
                Return True
            Else
                Return False
            End If
        End Function
#Region "Внутренние"
        ''' <summary>
        ''' Парсит Exel файл и извлекает из него позиции каталога
        ''' </summary>
        ''' <param name="app"></param>
        ''' <returns></returns>
        Private Function ExelParser(app As AppCore) As List(Of CatalogItem)
            Dim result As New List(Of CatalogItem)
            Try
                Dim exelCatalog As New XLWorkbook(app.LocalSettings.CatalogPath)
                Dim catalogSheet As IXLWorksheet = exelCatalog.Worksheets.First
                For Each cell In catalogSheet.Cells
                    If cell.WorksheetColumn.ColumnNumber = 1 Then
                        If catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "" And catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "Код" Then
                            Dim catalogItem As New CatalogItem
                            catalogItem.Name = cell.Value
                            catalogItem.Price = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value, 0)
                            catalogItem.Code = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value
                            catalogItem.CostPrice = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value, 0)
                            catalogItem.Unit = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 5).Value
                            catalogItem.GroupCode = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 6).Value
                            result.Add(catalogItem)
                        End If
                    End If
                Next
            Catch ex As Exception
                Return New List(Of CatalogItem)
            End Try
            Return result
        End Function
#End Region
#End Region
    End Class
End Namespace
