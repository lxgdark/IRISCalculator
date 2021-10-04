Imports IRISCalculator.DataClasses

Class ProductLayoutPopupPage
#Region "Задание стартовых парметров"
    Public Sub SetParametr(param As StandartOrderItem)
        'Устанавливаем контекст для текущей страницы
        Me.DataContext = param
        'Задаем поля принтера
        PaperWorkSize.Margin = New Thickness(param.PrintPaperSize.FieldWidth, param.PrintPaperSize.FieldHeight, param.PrintPaperSize.FieldWidth, param.PrintPaperSize.FieldHeight)
        'Цикл равный количеству изделий
        For i = 1 To param.ProductCount
            'Создаем рамку описывающую полный размер изделия вместе с вылитами
            Dim b As New Border
            'Цвет линии рамки 
            b.BorderBrush = Brushes.Red
            'Толщена линии
            b.BorderThickness = New Thickness(0.5)
            'Внешний отступ уменьшаем на ширину рамки
            b.Margin = New Thickness(-0.25)
            'В зависимости от того совпадает ли ориентация издели и бумаги задаем ширину и высоту рамки
            If param.IsProductOrientationEqualsPaperOrientation Then
                b.Height = param.ProductSize.Height + param.ProductSize.FieldHeight * 2
                b.Width = param.ProductSize.Width + param.ProductSize.FieldHeight * 2
            Else
                b.Width = param.ProductSize.Height + param.ProductSize.FieldHeight * 2
                b.Height = param.ProductSize.Width + param.ProductSize.FieldHeight * 2
            End If
            'Устанавливаем ориентацию WrapPanel в зависимости от ориентации листа
            If param.PrintPaperSize.Width < param.PrintPaperSize.Height Then
                ProductGrid.Orientation = Orientation.Horizontal
            Else
                ProductGrid.Orientation = Orientation.Vertical
            End If
            'Создаем внутреннюю рамку описывающую изделие без вылетов
            Dim prodb As New Border
            'Цвет линий рамки зеленый
            prodb.BorderBrush = Brushes.Green
            'Ширина внутренней рамки
            prodb.BorderThickness = New Thickness(1)
            'Отступ от внешней рамки 9они же вылеты)
            prodb.Margin = New Thickness(param.ProductSize.FieldWidth, param.ProductSize.FieldHeight, param.ProductSize.FieldWidth, param.ProductSize.FieldHeight)
            'Помещаем внутреннюю рамку во внешнюю
            b.Child = prodb
            'Добавляем внешнюю в раскладку
            ProductGrid.Children.Add(b)
        Next
    End Sub
#End Region
End Class
