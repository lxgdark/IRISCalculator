Imports IRISCalculator.DataClasses
Imports IRISCalculator.Workers

Class MainPage
#Region "Работа с окном"
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
#End Region
#Region "Добавление новых вкладок"
    ''' <summary>
    ''' Происходит при нажатии кнопки добавления нового расчета
    ''' </summary>
    Private Sub AddOrder_Click(sender As Object, e As RoutedEventArgs)
        'Переходим на главную страницу
        My.AppCore.CurentSelectedPage = AppCore.CurentSelectedPageEnum.Home
        'Добавляем в глобальный список страниц новую
        My.AppCore.GlobalPagesList.Add(New GlobalPageWorker With {.Header = "Новый расчет", .IsStartPage = False})
        'Переходим к последней добавленной странице
        OrderTabControl.SelectedIndex = My.AppCore.GlobalPagesList.Count - 1
    End Sub
    ''' <summary>
    ''' Происходит при нажатии кнопки добавления нового расчета на основе шаблона
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub AddPresetOrder_Click(sender As Object, e As RoutedEventArgs)
        'Переходим на главную страницу
        My.AppCore.CurentSelectedPage = AppCore.CurentSelectedPageEnum.Home
        'Добавляем в глобальный список страниц новую страницу работы с шаблонами расчетов
        My.AppCore.GlobalPagesList.Add(New GlobalPageWorker With {.Header = "Новый расчет по шаблону", .IsStartPage = False, .OrderObject = New OrderPresetPage})
        'Переходим к последней добавленной странице
        OrderTabControl.SelectedIndex = My.AppCore.GlobalPagesList.Count - 1
    End Sub
#End Region
#Region "Работа с вкладками (закрытие, смена заголовка)"
    ''' <summary>
    ''' Происходит при нажатии правой кнопки мыши по заголовку вкладки
    ''' </summary>
    Private Async Sub GlobalTabItem_MouseRightButtonUp(sender As Border, e As MouseButtonEventArgs)
        'Если это стартовая страница, то выходим из процедуры
        If CType(sender.Tag, GlobalPageWorker).IsStartPage Then Exit Sub
        'Справшиваем уверен ли пользователь, что хочет закрыть вкладку и закрываем, если да
        If Await My.MessageWorker.ShowMessage("Вы уверены. что хотите закрыть расчет?",, MessageWorker.GetStandartYesNoOptions) Then
            My.AppCore.GlobalPagesList.Remove(sender.Tag)
            OrderTabControl.SelectedIndex = 0
        End If
    End Sub

    ''' <summary>
    ''' Переход в режим редактирования заголовка
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub GlobalTabItem_MouseLeftButtonDown(sender As Border, e As MouseButtonEventArgs)
        'Если это стартовая страница, то выходим из процедуры
        If CType(sender.Tag, GlobalPageWorker).IsStartPage Then Exit Sub
        'Если было два нажатия левой кнопки мыши
        If e.ClickCount = 2 Then
            'Переводим переменную режима редактирования заголовка в True
            CType(sender.Tag, GlobalPageWorker).IsHeaderEditState = True
        End If
    End Sub
    ''' <summary>
    ''' Возникает при потерефокуса в поле редактирования заголовка
    ''' </summary>
    Private Sub HeaderTextBox_LostFocus()
        'Проходим по всем вкладкам и у всех снемаем режим редактирования заголовка
        For Each tabPage In My.AppCore.GlobalPagesList
            tabPage.IsHeaderEditState = False
        Next
    End Sub
    ''' <summary>
    ''' Происходит при нажатии кнопок в поле ввода заголовка вкладки
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub HeaderTextBox_KeyUp(sender As Object, e As KeyEventArgs)
        'Если нажата Enter, то вызываем процедуру, которая отменяет режим редактирования заголовка
        If e.Key = Key.Enter Then
            HeaderTextBox_LostFocus()
        End If
    End Sub
#End Region
End Class