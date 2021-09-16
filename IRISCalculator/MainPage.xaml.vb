Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers

Class MainPage
    ''' <summary>
    ''' Загрузка окна
    ''' </summary>
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        'Подписываемся на событие обновления интерфейчас
        AddHandler My.AppCore.UpdateInterface, AddressOf App_UpdateInterface
    End Sub
    ''' <summary>
    ''' Вызыывается событием обновления интерфейса
    ''' </summary>
    Private Sub App_UpdateInterface()
        If NavigationService Is Nothing Then Exit Sub
        RemoveHandler My.AppCore.UpdateInterface, AddressOf App_UpdateInterface
        NavigationService.Refresh()
    End Sub
    ''' <summary>
    ''' Происходит при нажатии правой кнопки мыши по заголовку вкладки
    ''' </summary>
    Private Async Sub GlobalTabItem_MouseRightButtonUp(sender As Border, e As MouseButtonEventArgs)
        'Если это стартовая страница, то выходим из процедуры
        If CType(sender.Tag, GlobalPage).IsStartPage Then Exit Sub
        'Справшиваем уверен ли пользователь, что хочет закрыть вкладку и закрываем, если да
        If Await My.MessageWorker.ShowMessage("Вы уверены. что хотите закрыть расчет?",, MessageWorker.GetStandartYesNoOptions) Then
            My.AppCore.GlobalPagesList.Remove(sender.Tag)
        End If
    End Sub
    ''' <summary>
    ''' Происходит при нажатии кнопки добавления нового расчета
    ''' </summary>
    Private Sub AddOrder_Click(sender As Object, e As RoutedEventArgs)
        'Переходим на главную страницу
        My.AppCore.CurentSelectedPage = AppCore.CurentSelectedPageEnum.Home
        'Добавляем в глобальный список страниц новую
        My.AppCore.GlobalPagesList.Add(New DataClasses.GlobalPage With {.Header = "Новый расчет", .IsStartPage = False})
        'Переходим к последней добавленной странице
        OrderTabControl.SelectedIndex = My.AppCore.GlobalPagesList.Count - 1
    End Sub
End Class
