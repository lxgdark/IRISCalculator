Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers

Class OrderPage
    Private MeContext As StandartOrder
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
    End Sub

    Private Sub AddStructureButton_Click()
        MeContext.OrderItemList.Add(New StandartOrderItem)
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

    Private Sub Calculation()
        OrderItemParameterPopup.IsOpen = False
    End Sub

    Private Sub ProductSizeParametrButton_Click(sender As Object, e As RoutedEventArgs)
        Dim page As New PaperSizeParametrPopupPage
        page.SetParametr(CType(sender.Tag, StandartOrderItem).ProductSize, True, New CalculationDelegate(AddressOf Calculation))
        OrderItemParameterFrame.Content = page
        OrderItemParameterPopup.IsOpen = True
    End Sub
End Class
