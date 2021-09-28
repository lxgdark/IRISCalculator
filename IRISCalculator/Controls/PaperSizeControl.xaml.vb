Public Class PaperSizeControl
    Public Shared ReadOnly PaperSizeProperty As DependencyProperty = DependencyProperty.Register(NameOf(PaperSize), GetType(Size), GetType(PaperSizeControl))

    ''' <summary>
    ''' Размер бумаги или изделия
    ''' </summary>
    ''' <returns></returns>
    Public Property PaperSize As Size
        Get
            Return CType(GetValue(PaperSizeProperty), Size)
        End Get
        Set(value As Size)
            SetValue(PaperSizeProperty, value)
        End Set
    End Property

End Class
