Imports IRISCalculator.DataClasses
Class OrderPage
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        CatalogItemSelectionPopup.IsOpen = True
        CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).ItemCategory = CatalogItem.ItemCategoryEnum.Paper
    End Sub
End Class
