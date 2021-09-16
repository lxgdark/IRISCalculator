Class StartPage
    Private Async Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        If Not Await My.AppCore.Synchronization() Then
            Await My.MessageWorker.ShowMessage("При синхронизации каталога произошла ошибка. Возможно вы не верно указали путь к папке с каталогом!",, Workers.MessageWorker.GetTopMostErrorOptions)
        End If
    End Sub
End Class
