Imports System.Collections.ObjectModel
Imports IRISCalculator.DataClasses
Imports ClosedXML.Excel
Namespace Workers
    Public Class CatalogWorker
        Inherits BaseWorker(Of CatalogWorker)
#Region "Свойства"
#Region "Внутренние"

#End Region

#End Region
#Region "Процедуры и функции"
        Public Overrides Function StartActions(app As AppCore) As Task(Of Boolean)
            If IO.File.Exists(app.LocalSettings.CatalogPath) Then
                Return Task.FromResult(True)
            Else
                Return Task.FromResult(False)
            End If
        End Function
#End Region
    End Class
End Namespace
