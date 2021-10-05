Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers
Imports Xceed.Wpf.Toolkit

Class OrderPage
    Private MeContext As StandartOrderPage
    Dim isPageOpen As Boolean = False
    Private Delegate Sub CalculationDelegate()
    Protected Overrides Sub Finalize()
        MyBase.Finalize()
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        If isPageOpen Then Exit Sub
        isPageOpen = True
        MeContext = Me
        DataContext = MeContext
        AddStructureButton_Click()
        For Each item In My.AppCore.CatalogList
            If item.Name = "Печать 4+0" Then CType(MeContext.OrderItemList(0), StandartOrderItem).PrintItem.SetPropertys(item)
            If item.Name = "Резка в размер" Then CType(MeContext.OrderItemList(0), StandartOrderItem).CutItem.SetPropertys(item)
        Next
    End Sub

    Private Sub AddStructureButton_Click()
        Dim soi As New StandartOrderItem
        soi.Calculation()
        MeContext.OrderItemList.Add(soi)
    End Sub

    Private Async Sub OrderItem_PreviewMouseRightButtonDown(sender As Object, e As MouseButtonEventArgs)
        If Await My.MessageWorker.ShowMessage("Удалить составную часть?",, MessageWorker.GetStandartYesNoOptions) Then
            MeContext.OrderItemList.Remove(sender.Tag)
        End If
    End Sub

    Private Sub PrintPaperSizeParametrButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New PaperSizeParametrPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PrintPaperSize, False, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub

    Private Sub ProductSizeParametrButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New PaperSizeParametrPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).ProductSize, True, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub

    Private Sub ProductLayoutButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New ProductLayoutPopupPage
        page.SetParametr(sender.Tag)
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub

    Private Sub SelectPaperButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PaperItem, CatalogItem.ItemCategoryEnum.PAPER, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    Private Sub SelectPrintButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).PrintItem, CatalogItem.ItemCategoryEnum.SERVICEPRINT, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
    Private Sub SelectCutButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New CatalogItemSelectionPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).CutItem, CatalogItem.ItemCategoryEnum.SERVICECUT, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub

    Private Sub AddDopItemButton_Click(sender As Object, e As RoutedEventArgs)

    End Sub
#Region "Процедуры и функции"
    ''' <summary>
    ''' Основная процидура проводящаая расчет заказа
    ''' </summary>
    Private Sub Calculation()
        'Закрываем всплывающее окно
        OrderItemParameterPopup.IsOpen = False

        For Each item In MeContext.OrderItemList
            item.Calculation()

        Next
    End Sub
#End Region
End Class
