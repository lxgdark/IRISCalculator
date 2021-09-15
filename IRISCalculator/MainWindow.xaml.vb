Class MainWindow
#Region "Загрузка, закрытие, сворачивание и перемещение окна"
    ''' <summary>
    ''' Загрузка окна
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)

    End Sub

    ''' <summary>
    ''' Сворачивание окна
    ''' </summary>
    Private Sub MinimazeWindow_Click(sender As Object, e As RoutedEventArgs)
        WindowState = WindowState.Minimized
    End Sub
    ''' <summary>
    ''' Разворачивания окна
    ''' </summary>
    Private Sub MaximumWindow_Click(sender As Object, e As RoutedEventArgs)
        If WindowState = WindowState.Maximized Then
            WindowState = WindowState.Normal
        Else
            WindowState = WindowState.Maximized
        End If
    End Sub
    ''' <summary>
    ''' Перемещение окна
    ''' </summary>
    Private Sub CaptionBar_PreviewMouseDown(sender As Object, e As MouseButtonEventArgs)
        Try
            DragMove()
        Catch ex As Exception
        End Try
    End Sub
    Private Sub CloseWindow_Click(sender As Object, e As RoutedEventArgs)
        'Сохраняем настройки в локальный файл
        My.AppCore.SaveSettings(My.AppCore.LocalSettings)
        'Закрываем приложение
        Close()
    End Sub
#End Region
#Region "МесседжБокс"
    ''' <summary>
    ''' Нажатие на кнопку Нет
    ''' </summary>
    Private Sub NoMessageButton_Click(sender As Object, e As RoutedEventArgs)
        My.MessageWorker.CloseMessage(False)
    End Sub
    ''' <summary>
    ''' Нажатие на кнопку Да/ОК
    ''' </summary>
    Private Sub YesMessageButton_Click(sender As Object, e As RoutedEventArgs)
        My.MessageWorker.CloseMessage(True)
    End Sub
#End Region
End Class