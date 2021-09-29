Imports IRISCalculator.DataClasses
Class PaperSizeParametrPopupPage
    Private Calculation As [Delegate]
    Private paperOrProductSize As New PaperSizeItem
#Region "Задание стартовых парметров"
    Public Sub SetParametr(pSize As PaperSizeItem, isProduct As Boolean, calculationSub As [Delegate])
        'Сохраняем делегат, который нужно вызвать по завершению
        Calculation = calculationSub
        'Сохраняем ссылку на размер
        paperOrProductSize = pSize
        'Выставляем тексты в зависимости для каких настроек было вызвано окно (настройки бумаги или готовго изделия)
        If isProduct Then
            FieldWidthTextBlock.Text = "Вылеты слева/справа:"
            FieldHeightTextBlock.Text = "Вылеты сверху/снизу:"
        Else
            FieldWidthTextBlock.Text = "Отступ от каря листа слева/справа:"
            FieldHeightTextBlock.Text = "Отступ от каря листа сверху/снизу:"
        End If
        'Устанавливаем отступы (вылеты)
        FieldHeightIntegerUpDown.Value = paperOrProductSize.FieldHeight
        FieldWidthIntegerUpDown.Value = paperOrProductSize.FieldWidth
    End Sub
#End Region
#Region "Настройка полей (вылетов)"
    ''' <summary>
    ''' Происходит при установки связывания значений полей (вылетов)
    ''' </summary>
    Private Sub EqualsFieldToggleButton_Checked()
        'Уравнимаем значения полей (вылетов)
        If FieldHeightIntegerUpDown Is Nothing Then Exit Sub
        FieldHeightIntegerUpDown.Value = FieldWidthIntegerUpDown.Value
    End Sub
    ''' <summary>
    ''' Происходит при изменении ширины полей (вылетов)
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FieldWidthIntegerUpDown_ValueChanged(sender As Object, e As RoutedPropertyChangedEventArgs(Of Object))
        'Если задан параметр уравнивать поля (вылеты), то вызываем процидуру, которая делает уравнивание
        If EqualsFieldToggleButton.IsChecked Then
            EqualsFieldToggleButton_Checked()
        End If
    End Sub
#End Region
#Region "Сохранение параметров"
    Private Sub CloseParametrButton_Click()
        'Если в списке стандартных размеров есть выбранный, передаем его значение в настройки бумаги (изделия)
        If PaperSizeListBox.SelectedIndex > -1 Then
            Dim selectedItem As PaperSizeItem = PaperSizeListBox.SelectedItem
            paperOrProductSize.Name = selectedItem.Name
            paperOrProductSize.Width = selectedItem.Width
            paperOrProductSize.Height = selectedItem.Height
        End If
        'Устанавливаем значение полей (вылетов)
        paperOrProductSize.FieldHeight = FieldHeightIntegerUpDown.Value
        paperOrProductSize.FieldWidth = FieldWidthIntegerUpDown.Value
        'Вызываем переданную процедуру завершения
        Windows.Application.Current.Dispatcher.Invoke(Calculation)
    End Sub
    ''' <summary>
    ''' Происходит при двойном нажатии на список размеров
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub PaperSizeListBox_MouseDoubleClick(sender As Object, e As MouseButtonEventArgs)
        If PaperSizeListBox.SelectedIndex > -1 Then
            CloseParametrButton_Click()
        End If
    End Sub
#End Region
End Class