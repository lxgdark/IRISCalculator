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
                'Открываем Exel заданный в настроках в качестве файла каталога
                Dim exelCatalog As New XLWorkbook(app.LocalSettings.CatalogPath)
                'Получаем ссылку на первую страницу
                Dim catalogSheet As IXLWorksheet = exelCatalog.Worksheets.First
                'Циклом проходим по всем заполненным ячейкам в файле
                'Цикл идет построчно слева на право
                For Each cell In catalogSheet.Cells
                    'Если мы сейчас в колонке №1...
                    If cell.WorksheetColumn.ColumnNumber = 1 Then
                        'Если третья колонка в данном ряду не пустая, а так же не имеет значение "Код", значит этот ряд описывает позицию каталога
                        If catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "" And catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value <> "Код" Then
                            Dim catalogItem As New CatalogItem
                            catalogItem.Name = cell.Value
                            catalogItem.Price = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value, 0)
                            catalogItem.Code = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value
                            catalogItem.CostPrice = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value, 0)
                            catalogItem.Unit = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 5).Value
                            catalogItem.GroupCode = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 6).Value
                            catalogItem.ItemCategory = GetItemCategory(catalogItem.GroupCode)
                            If catalogItem.CostPrice = 0 Then catalogItem.CostPrice = catalogItem.Price
                            result.Add(catalogItem)
                        End If
                    End If
                Next
            Catch ex As Exception
                Return New List(Of CatalogItem)
            End Try
            Return result
        End Function

        Private Function GetItemCategory(groupCode As String) As CatalogItem.ItemCategoryEnum
            Dim str As String() = groupCode.ToString.Split("$", StringSplitOptions.RemoveEmptyEntries)
            If str.Length = 3 Then
                Dim result As CatalogItem.ItemCategoryEnum = CatalogItem.ItemCategoryEnum.Paper
                Select Case str(1)
                    Case "PAPER"
                        result = CatalogItem.ItemCategoryEnum.Paper
                    Case "SERVICE"
                        result = CatalogItem.ItemCategoryEnum.Service
                End Select
                Return result
            Else
                Return CatalogItem.ItemCategoryEnum.Service
            End If
        End Function
#End Region
#End Region
    End Class
End Namespace
