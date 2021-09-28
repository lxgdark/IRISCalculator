Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers

Class OrderPage
    Private MeContext As StandartOrder
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        MeContext = Me
        DataContext = MeContext
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
        Dim p As New CatalogItemSelectionPopupPage
        p.Tag = OrderItemParameterPopup
        OrderItemParameterFrame.Content = p
        OrderItemParameterPopup.IsOpen = True
    End Sub
End Class
