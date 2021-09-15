Public Class WaitControl
    Public Shared ReadOnly WaitElementVisualProperty As DependencyProperty = DependencyProperty.Register("WaitElementVisual", GetType(Visual), GetType(WaitControl), Nothing)
    ''' <summary>
    ''' Определяет макет для отрисовки в виде перекрытия
    ''' </summary>
    ''' <returns></returns>
    Public Property WaitElementVisual As Visual
        Get
            Return CType(GetValue(WaitElementVisualProperty), Visual)
        End Get
        Set(value As Visual)
            SetValue(WaitElementVisualProperty, value)
        End Set
    End Property
End Class