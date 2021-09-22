Imports IRISCalculator.DataClasses
Class OrderPage
    Dim CatalogListSource As CollectionViewSource
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        CatalogListSource = TryFindResource("CatalogSource")
    End Sub

    Private Sub CatalogListBox_MouseDoubleClick(sender As ListBox, e As MouseButtonEventArgs)
        If sender.SelectedIndex > 0 Then
            'Действия по добавлению позиций в расчет
        End If
    End Sub

    Private Sub FindTextBox_TextChanged(sender As TextBox, e As TextChangedEventArgs)
        If sender.Text = "" Then
            RemoveHandler CatalogListSource.Filter, AddressOf FindTextInCatalog
        Else
            AddHandler CatalogListSource.Filter, AddressOf FindTextInCatalog
        End If
    End Sub

    Private Sub FindTextInCatalog(sender As Object, e As FilterEventArgs)
        Dim citem As CatalogItem = CType(e.Item, CatalogItem)
        If Not (citem Is Nothing) Then
            Dim result As Boolean = True
            Dim strmass() As String = FindTextBox.Text.ToLower.Split(" ".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
            For Each x In strmass
                result = result AndAlso citem.Name.ToLower.IndexOf(x) > -1
            Next
            e.Accepted = result
        End If
    End Sub

    Private Sub ClearFindTextButton_Click(sender As Object, e As RoutedEventArgs)
        'FindTextBox.Text = ""
    End Sub
End Class
