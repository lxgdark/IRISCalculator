Imports IRISCalculator.DataClasses
Class OrderPage


    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        CatalogItemSelectionPopup.IsOpen = True
        CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).ItemCategory = CatalogItem.ItemCategoryEnum.None
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        CatalogItemSelectionPopup.IsOpen = True
        CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).ItemCategory = CatalogItem.ItemCategoryEnum.Service
    End Sub
End Class
