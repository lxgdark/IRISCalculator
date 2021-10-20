Imports System.Xml.Serialization
Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers
Class OrderPage
    Private MeContext As StandartOrderPage
    Private Delegate Sub CalculationDelegate()
    Dim isPageOpen As Boolean = False
#Region "Страница"
    ''' <summary>
    ''' Загрузка страницы
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        MeContext = Me
        'Задоем контекст данных
        DataContext = MeContext
        'Если страница уже открыта, то выходим из процедуры
        If MeContext.IsPageOpen Then Exit Sub
        MeContext.IsPageOpen = True
        'Подписываемся на изменение коллекции вызывая пересчет заказа
        AddHandler MeContext.OrderItemList.CollectionChanged, AddressOf Calculation
        'Добавляем стартовую составную часть, если это не копия другой страницы
        If Not MeContext.IsCopyPage Then AddStandardOrderItemButton_Click()
        Calculation()
        'Добавляем стартовый расчет тиража
        AddPrintCopyCalculationButton_Click()
    End Sub
    ''' <summary>
    ''' Размер колонок на страницу
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Thumb_DragDelta(sender As Object, e As Controls.Primitives.DragDeltaEventArgs)
        Dim p As Double = e.HorizontalChange * 100 / GlobalGrid.ActualWidth
        Dim w As Double = MeContext.LeftPanelWidth.Value + Math.Round(p, 2)
        If w > 65 Then
            w = 65
        ElseIf w < 40 Then
            w = 40
        End If
        MeContext.LeftPanelWidth = New GridLength(w, GridUnitType.Star)
        MeContext.RightPanelWidth = New GridLength(100 - w, GridUnitType.Star)
    End Sub
#End Region
#Region "Работа с составными частями"
    ''' <summary>
    ''' Добавление стандартной составной части
    ''' </summary>
    Private Sub AddStandardOrderItemButton_Click()
        'Добавляем стандартную составную часть
        Dim soi As New StandartOrderItem
        'Добавляем в стартовую стоставную часть позиции каталога по умолчанию (для сокращения времени работы менеджера)
        For Each item In My.AppCore.CatalogList
            If item.Name = "Печать 4+0" Then soi.PrintItem.SetPropertys(item)
            If item.Name = "Резка в размер" Then soi.CutItem.SetPropertys(item)
        Next
        MeContext.OrderItemList.Add(soi)
        'Вызываем стартовый просчет внутри составной части
        soi.Calculation()
    End Sub
    ''' <summary>
    ''' Происходит при нажатии правой кнопки мыши на состанв
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OrderItem_PreviewMouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        'Открываем контекстое меню работы с составной частью
        OrderItemListContextMenu.Tag = sender.Tag
        OrderItemListContextMenu.IsOpen = True
    End Sub
    ''' <summary>
    ''' Происходит при нажатии пункта меню "Копировать" в списке составных частей
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub OrderItemListContextMenu_ClickItemCopy(sender As Object, e As RoutedEventArgs)
        'Добавляем стандартную составную часть
        Dim boi As BaseOrderItem
        'MsgBox(OrderItemListContextMenu.Tag.GetType.ToString)
        Dim T() As String = OrderItemListContextMenu.Tag.GetType.ToString.Split(".", StringSplitOptions.RemoveEmptyEntries)
        boi = StandartOrderPage.GetOrderItemByType(T(T.Length - 1))
        'Копируем значения из копируемой составной части
        boi.SetPropertys(OrderItemListContextMenu.Tag)
        MeContext.OrderItemList.Add(boi)
        'Вызываем стартовый просчет внутри составной части
        boi.Calculation()
    End Sub
    ''' <summary>
    ''' Происходит при нажатии пункта меню "Удалить" в списке составных частей
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub OrderItemListContextMenu_ClickItemKill(sender As MenuItem, e As RoutedEventArgs)
        If Await My.MessageWorker.ShowMessage("Удалить составную часть?",, MessageWorker.GetStandartYesNoOptions) Then
            MeContext.OrderItemList.Remove(OrderItemListContextMenu.Tag)
        End If
    End Sub
#End Region
#Region "Стандартная составная часть"
    ''' <summary>
    ''' Настройка размера бумаги
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PrintPaperSizeParametrButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New PaperSizeParametrPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PrintPaperSize, False, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Настройка размера изделия
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProductSizeParametrButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New PaperSizeParametrPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).ProductSize, True, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Открытие предпросмотра раскладки
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProductLayoutButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New ProductLayoutPopupPage
        page.SetParametr(sender.Tag)
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Выбор бумаги
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectPaperButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PaperItem, CatalogItem.ItemCategoryEnum.PAPER, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Выбр типа печати
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectPrintButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PrintItem, CatalogItem.ItemCategoryEnum.SERVICEPRINT, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Выбор типа резки
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectCutButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).CutItem, CatalogItem.ItemCategoryEnum.SERVICECUT, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
#Region "Доп. позиции стандартной составной части"
    ''' <summary>
    ''' Добвавление новой доп. позиции
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddDopItemButton_Click(sender As Object, e As RoutedEventArgs)
        Dim osoa As New OtherStandartOrderActionItem
        CType(sender.Tag, StandartOrderItem).OtherOrderActionList.Add(osoa)
        SelectOthetCatalogItemButton_Click(New Button With {.Tag = osoa}, Nothing)
    End Sub
    ''' <summary>
    ''' Выбор доп. позиции в каталоге
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectOthetCatalogItemButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, OtherStandartOrderActionItem).CatalogItem, CatalogItem.ItemCategoryEnum.NONE, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Удаление доп. позиции каталога
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub KillOthetCatalogItemButton_Click(sender As Object, e As RoutedEventArgs)
        If Await My.MessageWorker.ShowMessage("Удалить доп. обработку?",, MessageWorker.GetStandartYesNoOptions) Then
            For Each l In MeContext.OrderItemList
                If TypeOf l Is StandartOrderItem Then
                    CType(l, StandartOrderItem).OtherOrderActionList.Remove(sender.Tag)
                    Calculation()
                End If
            Next
        End If
    End Sub
    ''' <summary>
    ''' Открытие помощника в расчете количества доп. позиции
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub СalculationOthetCatalogItemButton_Click(sender As Object, e As RoutedEventArgs)
        'В разработке
    End Sub
#End Region
#End Region
#Region "Отдельная составная часть"
    Private Sub AddOneCatalogOrderItemButton_Click(sender As Object, e As RoutedEventArgs)
        'Добавляем отдельную составную часть
        Dim ocpoi As New OneCatalogPositionOrderItem
        MeContext.OrderItemList.Add(ocpoi)
        'Вызываем открытие каталога для выбора значения
        SelectOneCatalogItemButton_Click(New Button With {.Tag = ocpoi}, Nothing)
        ''Вызываем стартовый просчет внутри составной части
        ocpoi.Calculation()

    End Sub
    ''' <summary>
    ''' Поисходит приажаааааааакопки выбора позиции из каталога
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectOneCatalogItemButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, OneCatalogPositionOrderItem).CatalogItem, CatalogItem.ItemCategoryEnum.NONE, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    ''' <summary>
    ''' Происходит при нажатии кнопки расчета количества в отдельной позиции
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub СalculationOneCatalogItemButton_Click(sender As Object, e As RoutedEventArgs)
        'В разработке
    End Sub

#End Region
#Region "Расчет тиража"
    ''' <summary>
    ''' Добавление нового расчета для тиража
    ''' </summary>
    Public Sub AddPrintCopyCalculationButton_Click()
        MeContext.PrintCopyCountList.Add(New PrintCopyCountItem With {.CurrentCalculationFormula = New StandartCalculationFormula, .CostPriceForOne = MeContext.ProductCostPrice, .MinPrintCount = MeContext.MinPrintCopy})
    End Sub
    ''' <summary>
    ''' Удаление расчета для тиража
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PrintCopyCountItem_PreviewMouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        MeContext.PrintCopyCountList.Remove(sender.Tag)
    End Sub
#End Region
    ''' <summary>
    ''' Основная процедура проводящаая расчет заказа
    ''' </summary>
    Public Sub Calculation()
        'Закрываем всплывающее окно
        OrderItemParameterPopup.IsOpen = False

        'Проходим по составным частям
        For Each item In MeContext.OrderItemList
            'Вызываем в составных частях процедуру просчета
            item.Calculation()
        Next
        'Очищаем свойства расчета
        MeContext.ClearPropertys()
        For Each item In MeContext.OrderItemList
            MeContext.MinPrintCopy = IIf(item.GetProductCount > MeContext.MinPrintCopy, item.GetProductCount, MeContext.MinPrintCopy)
            MeContext.MinCostPrice += item.GetProductCostPrice * item.GetProductCount
            MeContext.ProductCostPrice += item.GetProductCostPrice
        Next
        Dim sinalFormulas As New SignalCalculationFormula
        MeContext.MinPrice = sinalFormulas.GetCalculationSumm(MeContext.MinPrintCopy, MeContext.ProductCostPrice)
        For Each pccl In MeContext.PrintCopyCountList
            pccl.CostPriceForOne = MeContext.ProductCostPrice
            pccl.MinPrintCount = MeContext.MinPrintCopy
        Next
    End Sub


    Private Sub SaveOrderButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub CopyOrderButton_Click(sender As Object, e As RoutedEventArgs)

        Dim newSOP As New StandartOrderPage
        newSOP.SetPropertys(MeContext)
        newSOP.IsCopyPage = True
        'Добавляем в глобальный список страниц новую
        My.AppCore.GlobalPagesList.Add(New GlobalPageWorker With {.Header = "Новый расчет", .IsStartPage = False, .OrderObject = newSOP})
    End Sub
End Class