Imports System.Windows.Controls.Primitives
Imports IRISCalculator.DataClasses
Class CatalogItemSelectionPopupPage
    Dim CatalogListSource As CollectionViewSource
    Private Calculation As [Delegate]
    Dim catalogItem As CatalogItem
#Region "Загрузка окна"
    Private Sub Page_Loaded(sender As Object, e As RoutedEventArgs)
        'Устанавливаем фокус в поле посика
        FindTextBox.Focus()
    End Sub
    ''' <summary>
    ''' Установка стартовых параметров
    ''' </summary>
    ''' <param name="_orderItem"></param>
    ''' <param name="iCategory"></param>
    ''' <param name="calculationSub"></param>
    Public Sub SetParametr(_catalogItem As CatalogItem, iCategory As CatalogItem.ItemCategoryEnum, calculationSub As [Delegate])
        'Определяем ссылку на список каталога, для фильтрации
        CatalogListSource = TryFindResource("CatalogSource")
        'Сохраняем делегат, который нужно вызвать по завершению
        Calculation = calculationSub
        'Определяем категорю позиций отображаемых в каталоге
        ItemCategory = iCategory
        'Созраняем ссылку на текущю составную часть заказа
        catalogItem = _catalogItem
    End Sub
#End Region

#Region "Свойства"
#Region "Внутренние"
    Private ItemCategoryValue As CatalogItem.ItemCategoryEnum = CatalogItem.ItemCategoryEnum.NONE
#End Region
    ''' <summary>
    ''' Категория выбираемой позиции каталога
    ''' </summary>
    ''' <returns></returns>
    Public Property ItemCategory As CatalogItem.ItemCategoryEnum
        Get
            Return ItemCategoryValue
        End Get
        Set(value As CatalogItem.ItemCategoryEnum)
            ItemCategoryValue = value
            AddHandler CatalogListSource.Filter, AddressOf FilterCatalog
        End Set
    End Property
#End Region
#Region "Поиск по каталогу"
    ''' <summary>
    ''' Возникает при изменении текста поиска
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FindTextBox_TextChanged(sender As TextBox, e As TextChangedEventArgs)
        'Добавляем ссылку на событие фильстрации
        AddHandler CatalogListSource.Filter, AddressOf FilterCatalog
    End Sub
    ''' <summary>
    ''' Происходит при нажатии на кнопку очиски текста фильтрации
    ''' </summary>
    Private Sub ClearFindTextButton_Click()
        'Очищаем текст фильтрации
        FindTextBox.Text = ""
    End Sub
    ''' <summary>
    ''' Фильтрует каталог по заданным параметрам
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub FilterCatalog(sender As Object, e As FilterEventArgs)
        'Определяем текущую проверяемую позицию каталога
        Dim citem As CatalogItem = CType(e.Item, CatalogItem)
        'Если она не пустая
        If Not (citem Is Nothing) Then
            'Определяем переменную результата фильтрации (да/нет)
            Dim result As Boolean = True
            'Проверяем соответсвуют ли текущая позиция каталога фильтрации по категории
            result = result AndAlso citem.ItemCategory = ItemCategory Or (ItemCategory = CatalogItem.ItemCategoryEnum.NONE)
            'Разбиваем строку поиска на слова
            Dim strmass() As String = FindTextBox.Text.ToLower.Split(" ".ToCharArray, StringSplitOptions.RemoveEmptyEntries)
            'Проходим по массиву слов и проверяем удовлетворяет ли поиск данным словам
            For Each x In strmass
                'Суммируем резултат
                result = result AndAlso citem.Name.ToLower.IndexOf(x) > -1
            Next
            'Возвращаем результат
            e.Accepted = result
        End If
    End Sub
#End Region
#Region "Выбор позиции в каталоге"
    ''' <summary>
    ''' Возникает при двойном нажатии на сриске
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub CatalogListBox_MouseDoubleClick(sender As ListBox, e As MouseButtonEventArgs)
        SelectedItem()
    End Sub
    ''' <summary>
    ''' Возникает при нажатии кнопки выбора
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub SelectPaperButton_Click(sender As Object, e As RoutedEventArgs)
        SelectedItem()
    End Sub
    ''' <summary>
    ''' Вызывается элементами выбора позиции каталога
    ''' </summary>
    Private Sub SelectedItem()
        'Если в каталоге есть выбранный элемент...
        If CatalogListBox.SelectedIndex > -1 Then
            'Удаляем ссылку на фильтрацию (для будущих открытий окна)
            RemoveHandler CatalogListSource.Filter, AddressOf FilterCatalog
            'Сохраняем выбранную бумагу
            catalogItem.SetPropertys(CatalogListBox.SelectedItem)
            'Очищаем поле поиска
            ClearFindTextButton_Click()
            'Вызываем переданную процедуру завершения
            Windows.Application.Current.Dispatcher.Invoke(Calculation)
        End If
    End Sub
#End Region

End Class