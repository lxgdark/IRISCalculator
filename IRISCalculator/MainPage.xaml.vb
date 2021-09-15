Class MainPage
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        AddHandler My.AppCore.UpdateInterface, AddressOf App_UpdateInterface
    End Sub

    Private Sub App_UpdateInterface()
        If NavigationService Is Nothing Then Exit Sub
        RemoveHandler My.AppCore.UpdateInterface, AddressOf App_UpdateInterface
        NavigationService.Refresh()
    End Sub
    Private Sub GlobalTabItem_MouseRightButtonUp(sender As Object, e As MouseButtonEventArgs)

    End Sub

    Private Sub AddOrder_Click(sender As Object, e As RoutedEventArgs)
        My.AppCore.CurentSelectedPage = AppCore.CurentSelectedPageEnum.Home
        My.AppCore.GlobalPagesList.Add(New DataClasses.GlobalPage With {.Header = "Новый расчет", .IsStartPage = False})
        OrderTabControl.SelectedIndex = My.AppCore.GlobalPagesList.Count - 1
    End Sub
End Class
