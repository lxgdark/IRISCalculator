Imports IRISCalculator.DataClasses
Class OrderPage


    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        CatalogItemSelectionPopup.IsOpen = True
        CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).ItemCategory = CatalogItem.ItemCategoryEnum.None
        AddHandler CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).CatalogItemSelect, AddressOf Paper_Select
    End Sub

    Private Sub Paper_Select(citem As CatalogItem)
        MsgBox(citem.Name)
        
    End Sub

    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    Private Sub Button_Click_1(sender As Object, e As RoutedEventArgs)
        CatalogItemSelectionPopup.IsOpen = True
        CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).ItemCategory = CatalogItem.ItemCategoryEnum.Service
        AddHandler CType(CatalogItemSelectionFrame.Content, CatalogItemSelectionPopupPage).CatalogItemSelect, AddressOf Service_Select

    End Sub

    Private Sub Service_Select(citem As CatalogItem)
        MsgBox(citem.Name)
    End Sub
End Class
