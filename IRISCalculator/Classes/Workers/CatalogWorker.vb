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
                'Если каталог не пустой собщаем, что все прошло гладко
                If app.CatalogList.Count > 0 Then
                    Return True
                Else
                    'В противном случае возвращаем ошибку
                    Return False
                End If
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
                            'Определяем переменную позиции каталога
                            Dim catalogItem As New CatalogItem
                            'Имя позиции
                            catalogItem.Name = cell.Value
                            'Цена позиции (задается если она не пустая и имеет значение типа Double)
                            catalogItem.Price = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 2).Value, 0)
                            'Код позиции
                            catalogItem.Code = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 3).Value
                            'Себестоимость позиции (задается если она не пустая и имеет значение типа Double)
                            catalogItem.CostPrice = IIf(TypeOf catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value Is Double, catalogSheet.Cell(cell.WorksheetRow.RowNumber, 4).Value, 0)
                            'Единица измерения
                            catalogItem.Unit = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 5).Value
                            'Код раздела
                            catalogItem.GroupCode = catalogSheet.Cell(cell.WorksheetRow.RowNumber, 6).Value
                            'Категория позиции (вычесленная на основе кода группы)
                            catalogItem.ItemCategory = CatalogGroupNameWorker.GetItemCategory(catalogItem.GroupCode)
                            'Если себестоимость ровна нулю (так бывает у услуг), то задаем в качестве себестоимость цену продажи (такой лайфхак для установки себестоимости услуг)
                            If catalogItem.CostPrice = 0 Then catalogItem.CostPrice = catalogItem.Price
                            'Добавляем позицию в общий список
                            result.Add(catalogItem)
                        End If
                    End If
                Next
            Catch ex As Exception
                'Если была ошибка, возвращаем пустой каталог
                Return New List(Of CatalogItem)
            End Try
            Return result
        End Function
#End Region
#End Region
    End Class
End Namespace