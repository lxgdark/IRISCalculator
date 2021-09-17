Imports Microsoft.Win32

Class SettingPage
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
End Class
