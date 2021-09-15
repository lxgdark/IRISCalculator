Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers
Imports Microsoft.Win32

Class ThemePage
    ''' <summary>
    ''' Выбор фоновой картинки
    ''' </summary>
    Private Sub OpenBackgroundImageButton_Click(sender As Object, e As RoutedEventArgs)
        'Создаем диалог открытия файла
        Dim ofd As New OpenFileDialog
        'Устанваливаем расширения допустимых файлов
        ofd.Filter = "Файлы изображений (*.JPG;*.PNG)|*.JPG;*.PNG"
        'Заголовок диалога открытия картинки
        ofd.Title = "Выбор фона"
        'Открывать ли предыдущую папку при повторном выборе фона
        ofd.RestoreDirectory = True
        'Если реузльтат выбора файла был положительным...
        If ofd.ShowDialog Then
            '...устанавливаем его в настройки приложения
            My.AppCore.ThemeWorker.CurrentTheme.BackgroundImage = ofd.FileName
        End If
    End Sub
    ''' <summary>
    ''' Удаление пользовательского изображения
    ''' </summary>
    Private Sub ClearBackgroundImageButton_Click(sender As Object, e As RoutedEventArgs)
        My.AppCore.ThemeWorker.CurrentTheme.BackgroundImage = Workers.ThemeWorker.BaseImageRelativeString
    End Sub
    ''' <summary>
    ''' Вызывает событие обновления интерфейса
    ''' </summary>
    Private Sub UpgrateTheme_Click(sender As Object, e As RoutedEventArgs)
        My.AppCore.OnInterfaceUpdate()
    End Sub
    ''' <summary>
    ''' Сброс настроек темы до настроек по умолчанию
    ''' </summary>
    Private Sub SetBaseTheme_Click(sender As Object, e As RoutedEventArgs)
        My.AppCore.ThemeWorker.CurrentTheme.SetPropertys(New DataClasses.ThemeClass)
    End Sub
#Region "Список тем"
    ''' <summary>
    ''' Добавляем текущие настройки в список сохраненных настроек
    ''' </summary>
    Private Sub AddThemeInListButton_Click(sender As Object, e As RoutedEventArgs)
        'Создаем новый класс тем
        Dim addedTheme As New ThemeClass
        'Пененосим в нее текущие настройки
        addedTheme.SetPropertys(My.AppCore.ThemeWorker.CurrentTheme)
        'Добавляем эту тему в список сохраненных
        My.AppCore.ThemeWorker.CustomThemeList.Add(addedTheme)
    End Sub
    ''' <summary>
    ''' Замена настроек темы в списке на текущую
    ''' </summary>
    Private Async Sub SaveThemeInListButton_Click(sender As Object, e As RoutedEventArgs)
        If ThemeList.SelectedIndex > -1 AndAlso Await My.MessageWorker.ShowMessage("Перезаписать выбранную тему?",, MessageWorker.GetStandartYesNoOptions) Then
            'Если в списке выбрана тема и пользователь дал согласие на замену, заменяем ее настройки из текущих
            CType(ThemeList.SelectedItem, ThemeClass).SetPropertys(My.AppCore.ThemeWorker.CurrentTheme)
        End If
    End Sub
    ''' <summary>
    ''' Удаляет выбранную темы
    ''' </summary>
    Private Async Sub KillThemeInListButton_Click(sender As Object, e As RoutedEventArgs)
        If ThemeList.SelectedIndex > -1 AndAlso Await My.MessageWorker.ShowMessage("Удалить выбранную тему?",, MessageWorker.GetStandartYesNoOptions) Then
            'Если в списке выбрана тема и пользователь дал согласие на удаление, удаляем выбранную тему
            My.AppCore.ThemeWorker.CustomThemeList.RemoveAt(ThemeList.SelectedIndex)
        End If
    End Sub
    ''' <summary>
    ''' Установка выбранной темы списка в качестве текущей
    ''' </summary>
    Private Async Sub SetThemeInListButton_Click(sender As Object, e As RoutedEventArgs)
        If ThemeList.SelectedIndex > -1 AndAlso Await My.MessageWorker.ShowMessage("Заменить вашу текущую тему выбранной?",, MessageWorker.GetStandartYesNoOptions) Then
            'Если в списке есть выбранная тема и пользователь дал согластие, переносим настройки темы из списка в текущую
            My.AppCore.ThemeWorker.CurrentTheme.SetPropertys(My.AppCore.ThemeWorker.CustomThemeList(ThemeList.SelectedIndex))
            'Перезапускаем страницу приложения для обновления визульного содержимого
            My.AppCore.OnInterfaceUpdate()
        End If
    End Sub
#End Region
End Class