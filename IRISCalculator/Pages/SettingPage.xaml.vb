Imports Microsoft.Win32
Class SettingPage
#Region "Синхронизация с каталогом"
    Private Sub SetCatalogButton_Click(sender As Object, e As RoutedEventArgs)
        'Создаем диалог открытия файла
        Dim ofd As New OpenFileDialog
        'Устанваливаем расширения допустимых файлов
        ofd.Filter = "Файл эксель (*.XLSX)|*.XLSX"
        'Заголовок диалога открытия картинки
        ofd.Title = "Выбор файла каталога"
        'Открывать ли предыдущую папку при повторном выборе фона
        ofd.RestoreDirectory = True
        'Если реузльтат выбора файла был положительным...
        If ofd.ShowDialog Then
            '...устанавливаем его в настройки приложения
            My.AppCore.LocalSettings.CatalogPath = ofd.FileName
        End If
    End Sub
    ''' <summary>
    ''' Происходит при нажатии кнопки принудительной синхронизации
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Async Sub SynchronizationButton_Click(sender As Object, e As RoutedEventArgs)
        'Вызываем синхронизацию
        If Not Await My.AppCore.Synchronization() Then
            'Если синхранизация выдала ошибку, сообщаем пользователю
            Await My.MessageWorker.ShowMessage("При синхронизации каталога произошла ошибка. Возможно вы не верно указали путь к папке с каталогом!",, Workers.MessageWorker.GetTopMostErrorOptions)
        End If
    End Sub
#End Region
End Class
